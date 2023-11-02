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
        private readonly IPersonDao PersonDao;

        public UsersController(IUserDao userDao, IPersonDao personDao)
        {
            UserDao = userDao;
            PersonDao = personDao;
        }

        [HttpGet]
        public ActionResult<List<Person>> GetAllUsers()
        {
            try
            {
                return Ok(UserDao.GetUsers());
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
