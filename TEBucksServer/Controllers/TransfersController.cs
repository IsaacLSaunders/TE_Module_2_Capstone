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

        //TODO get an individual transfer
        [HttpGet("{id}")]
        public ActionResult<Transfer> GetTransfer(int id)
        {
            try
            {

            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        //TODO create a transfer REQUEST OR SEND
        [HttpPost]
        public ActionResult<TransferDto> CreateTransfer(Transfer incoming)
        {

            if(!ValidateTransfer(incoming))
            {
                return BadRequest();
            }

            try
            {

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

            try
            {

            }
            catch (System.Exception)
            {

                return StatusCode(500);
            }
        }


        //TODO VALIDATE A TRANSFER what should this return?
        private bool ValidateTransfer(Transfer incoming)
        {
            //TODO can't send money to yourself

            //TODO can't send 0 or a negative number


            //TODO can't send more than what's in the account
            try
            {

            }
            //TODO not really sure about how/what exception to throw here
            catch (System.Exception e)
            {
               throw new System.Exception(e.Message);
            }


            return true;
        }
    }
}
