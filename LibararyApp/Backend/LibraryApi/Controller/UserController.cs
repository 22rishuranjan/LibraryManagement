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

        public async Task<IActionResult> User(GetUserDto act)
        {
            return HandleResult(await _userService.AddUser(act));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> User(int id, UpdateUserDto act)
        {
            return HandleResult(await _userService.UpdateUsers(act, id));
        }

        [Route("MostIssued")]
        [Route("MostBorrowed")]
        [HttpGet]
        public async Task<IActionResult> MostIssued()
        {
            return HandleResult(await _userService.GetUserWithMostIssuedBooks());
        }

        [Route("MostIssued/{days}")]
        [Route("MostBorrowed/{days}")]
        [Route("MostIssuedByTime/{days}")]
        [HttpGet]
        public async Task<IActionResult> MostIssuedByTime(int days)
        {
            DateTime startDate = DateTime.Now.AddDays(-days).ToLocalTime();
            DateTime finishDate = DateTime.Now.ToLocalTime();
            return HandleResult(await _userService.GetUserWithMostIssuedBooksByTime(startDate, finishDate));
        }


    }
}
