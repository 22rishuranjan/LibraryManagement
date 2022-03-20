using API.Filters;
using Application.DTO;
using Application.Interface;
using LibraryApi.Controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controller
{
    [TypeFilter(typeof(ApiExceptionFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : BaseApiController
    {

        private readonly IIssue _issueService;
        //private readonly DataContext _context;


        public IssueController(IIssue issueService) //, DataContext context   ---- used via method dependency injection
        {
            _issueService = issueService;
            // _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Issue()
        {
            return HandleResult(await _issueService.GetIssues());
        }

       


        [HttpGet("{id}")]
        public async Task<IActionResult> Issue(int id)
        {
            return HandleResult(await _issueService.GetIssueById(id));
        }


        //[Route("MostIssued")]
        //[Route("MostBorrowed")]
        [HttpGet("GetIssueByUserId/{id}")]
        public async Task<IActionResult> GetIssueByUserId(int id)
        {
            return HandleResult(await _issueService.GetIssueByUserId(id));
        }

        //[Route("MostIssued")]
        //[Route("MostBorrowed")]
        [HttpGet("GetIssueByBookId/{id}")]
        public async Task<IActionResult> GetIssueByBookId(int id)
        {
            return HandleResult(await _issueService.GetIssueByBookId(id));
        }


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteIssue(int id)
        //{
        //    return HandleResult(await _issueService.DeleteBooks(id));
        //}

        [HttpPost]

        public async Task<IActionResult> Issue(AddIssueDto issue)
        {
            return HandleResult(await _issueService.AddBookIssue(issue));
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Issue(int id, UpdateIssueDto act)
        //{
        //    return HandleResult(await _bookService.UpdateBooks(act, id));
        //}

    }
}
