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
                List<TransferDto> transfers = TransferDao.GetTransfersByPersonId(userId);
                TransferDto output = null;
                foreach(TransferDto transfer in transfers)
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
        public ActionResult<TransferDto> CreateTransfer(Transfer incoming)
        {

            if(!ValidateTransfer(incoming))
            {
                return BadRequest();
            }

            try
            {
                incoming.TransferStatus = incoming.TransferType == "Send" ? "Approved" : "Pending";
                TransferDto output = TransferDao.CreateTransfer(incoming);
                Transfer temp = new Transfer();
                temp.TransferId = output.TransferId;
                temp.TransferStatus = output.TransferStatus;
                temp.TransferType = output.TransferType;
                temp.Amount = output.Amount;
                temp.UserFrom = output.userFrom.UserId;
                temp.UserTo = output.userTo.UserId;

                if (output.TransferStatus == "Approved")
                {
                    //send money to an account
                    AccountDao.IncrementBalance(temp);
                }

                return Ok(output);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        //TODO approve and reject transfer http put with transfer status update DTO
        [HttpPut("{id}/status")]
        public ActionResult<Transfer> ApproveOrRejectTransfer(TransferStatusUpdateDto status, int id)
        {

            Transfer output = null;
            try
            {
                output = TransferDao.EditTransferStatus(status,id);
                if (output.TransferStatus == "Approved")
                {
                    AccountDao.DecrementBalance(output);
                }
            }
            catch (System.Exception)
            {

                return StatusCode(500);
            }
            return Ok(output);
        }


        private bool ValidateTransfer(Transfer incoming)
        {
            //can't send money to yourself
            if (incoming.UserFrom == incoming.UserTo)
            {
                return false;
            }

            //can't send 0 or a negative number
            if (incoming.Amount <= 0)
            {
                return false;
            }

            //TODO can't send more than what's in the account
            try
            {
                Account fromAccount = AccountDao.GetAccountByPersonId(incoming.UserFrom);
                Account toAccount = AccountDao.GetAccountByPersonId(incoming.UserTo);
                if (incoming.TransferType == "Send" && fromAccount.Balance < incoming.Amount)
                {
                    return false;
                }
                else if (incoming.TransferType != "Send" && toAccount.Balance < incoming.Amount)
                {
                    return false;
                }
            }
            //TODO not really sure about how/what exception to throw here
            catch (System.Exception e)
            {
               throw new System.Exception(e.Message);
            }


            //not sure about returning null here...
            return true;
        }
    }
}
