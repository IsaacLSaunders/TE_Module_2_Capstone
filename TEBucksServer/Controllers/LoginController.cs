﻿using Microsoft.AspNetCore.Mvc;
using TEBucksServer.DAO;
using TEBucksServer.Exceptions;
using TEBucksServer.Models;
using TEBucksServer.Security;

namespace TEBucksServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserDao userDao;
        private readonly IAccountDao accountDao;

        public LoginController(ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher, IUserDao userDao, IAccountDao accountDao)
        {
            this.tokenGenerator = tokenGenerator;
            this.passwordHasher = passwordHasher;
            this.userDao = userDao;
            this.accountDao = accountDao;
        }

        [HttpPost]
        public IActionResult Authenticate(LoginUser userParam)
        {
            // Default to bad username/password message
            IActionResult result = Unauthorized(new { message = "Username or password is incorrect." });

            User user;
            // Get the user by username
            try
            {
                user = userDao.GetUserByUsername(userParam.Username);
            }
            catch (DaoException)
            {
                // return default Unauthorized message instead of indicating a specific error
                return result;
            }

            // If we found a user and the password hash matches
            if (user != null && passwordHasher.VerifyHashMatch(user.PasswordHash, userParam.Password, user.Salt))
            {
                // Create an authentication token
                string token = tokenGenerator.GenerateToken(user.UserId, user.Username);

                // Create a ReturnUser object to return to the client
                ReturnUser retUser = new ReturnUser() { User = user, Token = token };

                // Switch to 200 OK
                return Ok(retUser);
            }

            return result;
        }

        [HttpPost("/register")]
        public IActionResult Register(LoginUser userParam)
        {
            // Default generic error message
            const string ErrorMessage = "An error occurred and user was not created.";

            IActionResult result = BadRequest(new { message = ErrorMessage });

            // is username already taken?
            try
            {
                User existingUser = userDao.GetUserByUsername(userParam.Username);
                if (existingUser != null)
                {
                    return Conflict(new { message = "Username already taken. Please choose a different username." });
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, ErrorMessage);
            }

            // create new user
            User newUser;
            Account newAccount;
            try
            {
                //create a user
                newUser = userDao.CreateUser(userParam.Username, userParam.Password, userParam.FirstName, userParam.LastName);
                //create account for new user
                newAccount = accountDao.CreateAccount(newUser.UserId);
            }
            catch (DaoException)
            {
                return StatusCode(500, ErrorMessage);
            }

            if (newUser != null)
            {
                // Create a ReturnUser object to return to the client
                ReturnUser returnUser = new ReturnUser() { User = newUser};

                result = Created("/login", returnUser);
            }

            return result;
        }
    }
}
