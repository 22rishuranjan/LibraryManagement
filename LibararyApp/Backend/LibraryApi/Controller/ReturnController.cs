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


        #region CrudAPI
        /* contains following APIS  
              a. Get the list of returns of books
              b. Get retuns of books by Id
              c. Create a new return of books
       */

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
   
        [HttpPost]

        public async Task<IActionResult> Return(AddReturnDto ret)
        {
            return HandleResult(await _returnService.AddBookReturn(ret));
        }
        #endregion

    }
}
