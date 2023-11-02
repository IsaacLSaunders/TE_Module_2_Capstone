using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                return Ok(TransferDao.GetTransferById(userId));
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
                incoming.TransferId = 0;
                return Ok(TransferDao.CreateTransfer(incoming));
            }
            catch (System.Exception)
            {

                return StatusCode(500);
            }
        }
    }
}
