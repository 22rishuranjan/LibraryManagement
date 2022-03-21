using API.Controller;
using API.Filters;
using Application.DTO;
using Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controller
{
    [TypeFilter(typeof(ApiExceptionFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {

        private readonly ILibUser _userService;
        //private readonly DataContext _context;


        public UserController(ILibUser userService) //, DataContext context   ---- used via method dependency injection
        {
            _userService = userService;
            // _context = context;
        }

        #region CrudApi
        /* contains following APIS  
               a. Get the list of user
               b. Get user information by user id
               c. Create a new user
               d. Update information on existing user
               e. Delete an existing existing
        */

        [HttpGet]
        public async Task<IActionResult> User()
        {
            return HandleResult(await _userService.GetUsers());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> User(int id)
        {
            return HandleResult(await _userService.GetUserById(id));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            return HandleResult(await _userService.DeleteUser(id));
        }


        [HttpPost]

        public async Task<IActionResult> User(UpdateUserDto act)
        {
            return HandleResult(await _userService.AddUser(act));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> User(int id, UpdateUserDto act)
        {
            return HandleResult(await _userService.UpdateUsers(act, id));
        }
        #endregion

        #region DriverAPI
        /* contains following APIS :
             a. Get the list of user with most issued book
             b. Get the list of user with most issued book filtered by time
  
        */

        [Route("MostIssued")]
        [Route("MostBorrowed")]
        [HttpGet]
        public async Task<IActionResult> MostIssued()
        {
            return HandleResult(await _userService.GetUserWithMostIssuedBooks());
        }

        [Route("MostIssued/{days:int}")]
        [Route("MostBorrowed/{days:int}")]
        [Route("MostIssuedByTime/{days:int}")]
        [HttpGet]
        public async Task<IActionResult> MostIssuedByTime(int days)
        {
            DateTime startDate = DateTime.Now.AddDays(-days).ToLocalTime();
            DateTime finishDate = DateTime.Now.ToLocalTime();
            return HandleResult(await _userService.GetUserWithMostIssuedBooksByTime(startDate, finishDate));
        }
        #endregion

    }
}
