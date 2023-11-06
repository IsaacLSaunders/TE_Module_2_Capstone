using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TEBucksServer.DAO;
using TEBucksServer.DTO;
using TEBucksServer.Models;
using TEBucksServer.NewFolder;
using TEBucksServer.Services;

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
        private readonly ITearsLog TearsLog;

        public TransfersController(IAccountDao accountDao, IUserDao userDao, ITransferDao transferDao, ITearsLog tearsLog)
        {
            AccountDao = accountDao;
            UserDao = userDao;
            TransferDao = transferDao;
            TearsLog = tearsLog;
        }

        //get an individual transfer
        [HttpGet("{id}")]
        public ActionResult<Transfer> GetTransfer(int id)
        {
            try
            {
                return Ok(TransferDao.GetTransferByTransferId(id));
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

            if (!ValidateTransfer(incoming))
            {
                return BadRequest();
            }

            try
            {
                Transfer output = TransferDao.CreateTransfer(incoming);
                return Created($"/api/transfers/{output.TransferId}", output);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        //approve and reject transfer http put with transfer status update DTO
        [HttpPut("{id}/status")]
        public ActionResult<Transfer> ApproveOrRejectTransfer(TransferStatusUpdateDto status, int id)
        {
      
            try
            {
                //check if the user that is logged in can only change the status of transfers they have access to
                User user = UserDao.GetUserByUsername(User.Identity.Name);
                Transfer curTransfer = TransferDao.GetTransferByTransferId(id);
                if(user.UserId != curTransfer.UserFrom.UserId)
                {
                    return BadRequest();
                }

                Transfer output = TransferDao.EditTransferStatus(status, id);
                
                return Ok(output);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }


        private bool ValidateTransfer(NewTransferDto incoming)
        {
            //can't send money to yourself
            if (incoming.userTo == incoming.userFrom)
            {
                return false;
            }

            //can't send 0 or a negative number
            if(incoming.amount <= 0)
            {
                return false;
            }

            //Only run the log once we have validated that the transfer is positive and its going to and from different users
            PrepAndExeTearsLog(incoming);

            //can't send more than what's in the account
            try
            {
                Account acntFrom = AccountDao.GetAccountByUserId(incoming.userFrom);

                if(acntFrom.Balance < incoming.amount)
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

        private void PrepAndExeTearsLog(NewTransferDto incomingTransferDto)
        {
                User from = UserDao.GetUserById(incomingTransferDto.userFrom);
                User to = UserDao.GetUserById(incomingTransferDto.userTo);
                Account acntFrom = AccountDao.GetAccountByUserId(from.UserId);
                    //check if the amount is larger than $1000
                string description = incomingTransferDto.amount >= 1000.00M ? "Transaction Over $1000.00. || " : "";

                TEARSLogModel log = new TEARSLogModel();

                log.amount = incomingTransferDto.amount;
                log.username_from = from.Username;
                log.username_to = to.Username;

                    //check if the balance is less than the requested/sent transfer amount
                if(acntFrom.Balance < incomingTransferDto.amount)
                {
                    log.description = description + $"{incomingTransferDto.transferType} || Overdraft";
                    TearsLog.Log(log);
                }

        }
    }
}
