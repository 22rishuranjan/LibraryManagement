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
    public class ReturnController : BaseApiController
    {

        private readonly IReturn _returnService;
       


        public ReturnController(IReturn returnService) 
        {
            _returnService = returnService;
    
        }

        [HttpGet]
        public async Task<IActionResult> Return()
        {
            return HandleResult(await _returnService.GetBookReturns());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Return(int id)
        {
            return HandleResult(await _returnService.GetReturnById(id));
        }


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteIssue(int id)
        //{
        //    return HandleResult(await _issueService.DeleteBooks(id));
        //}

        [HttpPost]

        public async Task<IActionResult> Return(AddReturnDto ret)
        {
            return HandleResult(await _returnService.AddBookReturn(ret));
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Issue(int id, UpdateIssueDto act)
        //{
        //    return HandleResult(await _bookService.UpdateBooks(act, id));
        //}

    }
}
