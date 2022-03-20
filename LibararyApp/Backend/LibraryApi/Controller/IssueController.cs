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
  
        public IssueController(IIssue issueService) 
        {
            _issueService = issueService;
        
        }


        #region CrudAPI
        /* contains following APIS  
               a. Get the list of issues
               b. Get issue by Id
               c. Create a new issue
        */


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

        [HttpPost]
        public async Task<IActionResult> Issue(AddIssueDto issue)
        {
            return HandleResult(await _issueService.AddBookIssue(issue));
        }
        #endregion

        #region DriverAPI
        /* contains following APIS  
              a. Get the list of issues filtered by user id
              b. Get the list of issues filtered by book id   
        */

        [HttpGet("GetIssueByUserId/{id}")]
        public async Task<IActionResult> GetIssueByUserId(int id)
        {
            return HandleResult(await _issueService.GetIssueByUserId(id));
        }

        
        [HttpGet("GetIssueByBookId/{id}")]
        public async Task<IActionResult> GetIssueByBookId(int id)
        {
            return HandleResult(await _issueService.GetIssueByBookId(id));
        }
        #endregion 

    }
}
