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
    public class UsersController : Controller
    {

        private readonly IUserDao UserDao;

        public UsersController(IUserDao userDao)
        {
            UserDao = userDao;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            List<User> output = null;
            try
            {
                output = UserDao.GetUsers();

                return Ok(output);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
