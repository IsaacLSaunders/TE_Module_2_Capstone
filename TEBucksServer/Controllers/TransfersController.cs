using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TEBucksServer.DAO;
using TEBucksServer.Models;

namespace TEBucksServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransfersController : Controller
    {
        private readonly IAccountDao AccountDao;
        private readonly IUserDao UserDao;
        private readonly ITransferDao TransferDao;

        public TransfersController(IAccountDao accountDao, IUserDao userDao, ITransferDao transferDao)
        {
            AccountDao = accountDao;
            UserDao = userDao;
            TransferDao = transferDao;
        }


        [HttpGet("{id}")]
        public ActionResult<Transfer> GetTransfer(int id)
        {
            try
            {
                int userId = UserDao.GetUserByUsername(User.Identity.Name).UserId;
                List<Transfer> transfers = TransferDao.GetTransfersByPersonId(userId);
                Transfer output = null;
                foreach(Transfer transfer in transfers)
                {
                    if(transfer.TransferId == id)
                    {
                        output = transfer;
                    }
                }
                return Ok(output);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult<Transfer> CreateTransfer(Transfer incoming)
        {
            try
            {
                incoming.TransferStatus = incoming.TransferType == "Send" ? "Approved" : "Pending";
                Transfer output = TransferDao.CreateTransfer(incoming);
                if (output.TransferStatus == "Approved")
                {
                    //send money to an account
                    AccountDao.IncrementBalance(output);
                }

                return Ok(output);
            }
            catch (System.Exception)
            {

                return StatusCode(500);
            }
        }

        ////TODO approve and reject transfer http put with transfer status update DTO
        //[HttpPut({id}/status)]
        //public ActionResult<Transfer> ApproveOrRejectTransfer()
    }
}
