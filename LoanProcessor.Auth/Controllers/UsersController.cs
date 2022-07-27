using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoanProcessor.Auth.Data;
using LoanProcessor.Auth.Model;
using LoanProcessor.Auth.Repositories;
using LoanProcessor.Auth.ViewModels;

namespace LoanProcessor.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("authenticate")]
        //public async Task<ActionResult<User>> Authenticate([FromBody] LoginViewModel loginViewModel)
        public ActionResult<User> Authenticate([FromBody] LoginViewModel loginViewModel)
        {
            //_context.User.Add(user);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetUser", new { id = user.Id }, user);

            var user =  _userRepository.Authenticate(loginViewModel.UserName, loginViewModel.Password);

            if (user != null)
            {
                return Ok(user);

            }
            else
            {
                return BadRequest(new { message = "UserName or Password is incorrect" });

            }
        }

       
    }
}
