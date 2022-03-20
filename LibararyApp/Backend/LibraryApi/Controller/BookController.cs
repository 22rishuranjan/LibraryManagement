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
    public class BookController : BaseApiController
    {

        private readonly IBook _bookService;
     
        public BookController(IBook bookService) 
        {
            _bookService = bookService;
        }

        #region CrudAPI
        /* contains following APIS  
                a. Get the list of books
                b. Get book information by bookid
                c. Create a new Book
                d. Update information on existing book
                e. Delete an existing book
        */

        [HttpGet]
        public async Task<IActionResult> Book()
        {
            return HandleResult(await _bookService.GetBooks());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Book(int id)
        {
            return HandleResult(await _bookService.GetBookById(id));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return HandleResult(await _bookService.DeleteBooks(id));
        }

        [HttpPost]

        public async Task<IActionResult> Book(UpdateBookDto book)
        {
            return HandleResult(await _bookService.AddBook(book));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Book(int id, UpdateBookDto book)
        {
            return HandleResult(await _bookService.UpdateBooks(book, id));
        }
        #endregion

        #region DriverAPI

        /* contains functions for 
             a. Most issued books
             b. Most issued books filter by day
             c. Get other books by user
             d. no of available copy
         */

        [Route("MostIssued")]
        [Route("MostBorrowed")]
        [HttpGet]
        public async Task<IActionResult> MostIssued()
        {
            return HandleResult(await _bookService.GetMostIssuedBooks());
        }

        [Route("MostIssued/{days}")]
        [Route("MostBorrowed/{days}")]
        [Route("MostIssuedByTime/{days}")]
        [HttpGet]
        public async Task<IActionResult> MostIssued(int days)
        {
            DateTime startDate = DateTime.Now.AddDays(-days).ToLocalTime();
            DateTime finishDate = DateTime.Now.ToLocalTime();
            return HandleResult(await _bookService.GetMostIssuedBooksByTime(startDate, finishDate));
        }

        [Route("GetOtherBooks/{bookId}/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetOtherBooks(int bookId, int userId)
        {
            return HandleResult(await _bookService.GetOtherBooks(bookId, userId));
        }

        [Route("Available/{bookId}")]
        [Route("BookAvailable/{bookId}")]
        [HttpGet]
        public async Task<IActionResult> BookAvailable(int bookId)
        {
            return HandleResult(await _bookService.BookAvailable(bookId));
        }
        #endregion
    }
}
