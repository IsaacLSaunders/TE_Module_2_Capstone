using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TEBucksServer.DAO;
using TEBucksServer.DTO;
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

        //get an individual transfer
        [HttpGet("{id}")]
        public ActionResult<Transfer> GetTransfer(int id)
        {
            Transfer output = null;
            try
            {
                output = TransferDao.GetTransferByTransferId(id);
                return Ok(output);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        //create a transfer REQUEST OR SEND
        [HttpPost]
        public ActionResult<Transfer> CreateTransfer(NewTransferDto incoming)
        {
            Transfer output = null;

            if (!ValidateTransfer(incoming))
            {
                return BadRequest();
            }

            try
            {
                output = TransferDao.CreateTransfer(incoming);
                return Created($"/api/transfers/{output.TransferId}", output);
            }
            catch (Exception)
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
                //check if the user that is logged in can only change the status of transfers they have access to
                User user = UserDao.GetUserByUsername(User.Identity.Name);
                if(user.UserId != id)
                {
                    return BadRequest();
                }

                output = TransferDao.EditTransferStatus(status, id);
                return Ok(output);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }


        //VALIDATE A TRANSFER what should this return?
                //TODO THIS COULD BE WHERE THE LOGGER GOES
        private bool ValidateTransfer(NewTransferDto incoming)
        {
            //can't send money to yourself
            if(incoming.userTo == incoming.userFrom)
            {
                return false;
            }

            //can't send 0 or a negative number
            if(incoming.amount <= 0)
            {
                return false;
            }


            //can't send more than what's in the account
            try
            {
                Account from = AccountDao.GetAccountByUserId(incoming.userFrom);

                if(from.Balance < incoming.amount)
                {
                    return false;
                }
            }
            //TODO not really sure about how/what exception to throw here
            catch (Exception e)
            {
               throw new Exception("Could not get the necissary account information to validate the transfer.", e);
            }


            return true;
        }
    }
}
