using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TEBucksServer.DAO;
using TEBucksServer.Models;

namespace TEBucksServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountDao AccountDao;
        private readonly IUserDao UserDao;
        private readonly ITransferDao TransferDao;

        public AccountController(IAccountDao accountDao, IUserDao userDao, ITransferDao transferDao)
        {
            AccountDao = accountDao;
            UserDao = userDao;
            TransferDao = transferDao;
        }

        [HttpGet("balance")]
        public ActionResult<Account> GetAccount()
        {
            try
            {
                int userId = UserDao.GetUserByUsername(User.Identity.Name).UserId;
                return Ok(AccountDao.GetAccountById(userId));
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("transfers")]
        public ActionResult<List<Transfer>> GetAllTransfers()
        {
            try
            {
                int userId = UserDao.GetUserByUsername(User.Identity.Name).UserId;
                return Ok(TransferDao.GetTransfersByPersonId(userId));
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
