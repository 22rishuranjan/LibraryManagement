using Application.DTO;
using Application.Interface;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Runtime.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class BookService :IBook
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;

        private readonly IUtility _utility;

    
      
        public BookService(DataContext context, IMapper mapper, IUtility utility)
        {
            _context = context;
            _mapper = mapper;
            _utility = utility;

        }

        public async Task<ApiResponse<List<GetBookDto>>> AddBook(GetBookDto book)
        {
            ApiResponse<List<GetBookDto>> res = new ApiResponse<List<GetBookDto>>();
            Book _book = _mapper.Map<Book>(book);
            _context.Add(_book);
            await _context.SaveChangesAsync();
            res.Data = await _context.Books.Select(c => _mapper.Map<GetBookDto>(c)).ToListAsync();
            res.Message = "Success: Book added!";
            res.Success = true;
            res.Status = 201;

            return res;
        }

        public async Task<ApiResponse<GetBookDto>> DeleteBooks(int id)
        {
            ApiResponse<GetBookDto> res = new ApiResponse<GetBookDto>();

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                res.Data = null;
                res.Message = "Error: Book can not be found!";
                res.Success = true;
                res.Status = 404;
                return res;

            }

            res.Data = _mapper.Map<GetBookDto>(book);
            res.Message = "Book deleted!!";
            res.Success = true;
            res.Status = 200;

            _context.Remove(book);
            await _context.SaveChangesAsync();

            return res;
        }

        //public Task<ApiResponse<BookDto>> DeleteBooks(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ApiResponse<GetBookDto>> GetBookById(int id)
        {
            ApiResponse<GetBookDto> res = new ApiResponse<GetBookDto>();

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                res.Data = null;
                res.Message = "Error: Book can not be found!";
                res.Success = false;
                res.Status = 404;
                return res;

            }
            res.Data = _mapper.Map<GetBookDto>(book);
            res.Message = "Book found!!";
            res.Success = true;
            res.Status = 200;

            return res;
        }


        public async Task<ApiResponse<List<GetBookDto>>> GetBooks()
        {
            ApiResponse<List<GetBookDto>> res = new ApiResponse<List<GetBookDto>>();
            res.Data = await _context.Books.Select(c => _mapper.Map<GetBookDto>(c)).ToListAsync();
            res.Message = "List fectched!!";
            res.Success = true;
            res.Status = 200;

            return res;
        }

        public async Task<ApiResponse<GetBookDto>> UpdateBooks(UpdateBookDto book, int id)
        {
            ApiResponse<GetBookDto> res = new ApiResponse<GetBookDto>();

            var _book = await _context.Books.FindAsync(id);
            if (_book == null)
            {
                res.Data = null;
                res.Message = "Error: Book can not be found!";
                res.Success = false;
                res.Status = 404;
                return res;
            }


            _mapper.Map(book, _book);
            await _context.SaveChangesAsync();
            res.Data = _mapper.Map<GetBookDto>(await _context.Books.FindAsync(id));
            res.Message = "Success, Book is successfully updated.";
            res.Success = true;
            return res;
        }

        public async Task<ApiResponse<List<GetBookDto>>> GetMostIssuedBooks()
        {

            ApiResponse<List<GetBookDto>> res = new ApiResponse<List<GetBookDto>>();
            var issues = _context.Issues.GroupBy(i => i.BookId)
                                             .Select(group => new
                                             {
                                                 Key = group.Key,
                                                 Count = group.Count()
                                             }).OrderByDescending(x => x.Count).ToList();


            List<GetBookDto> listBookDto = new List<GetBookDto>();
            var highestCount = 0;
            foreach (var issue in issues)
            {
                if (highestCount <= issue.Count) highestCount = issue.Count;
                    if (issue.Count >= highestCount)
                {
                    GetBookDto bookDto = new GetBookDto();
                    var book = await _context.Books.FindAsync(issue.Key);
                    bookDto = _mapper.Map<GetBookDto>(book);
                    listBookDto.Add(bookDto);
                }
            }
            
            


            if (listBookDto.Count > 0)
            {
                res.Data = listBookDto;
                res.Message = $"Most Borrowed book(s) with count {highestCount}";
                res.Success = true;
                res.Status = 200;
            }
            else
            {
                res.Data = null;
                res.Message = "record not found!!";
                res.Success = true;
                res.Status = 200;
            }


            return res;
        }

        public async Task<ApiResponse<List<GetBookDto>>> GetOtherBooks(int bookId, int userId)
        {

            ApiResponse<List<GetBookDto>> res = new ApiResponse<List<GetBookDto>>();

            var issues = _context.Issues.Where(i => i.UserId == userId)
                                        .Select(i => i.BookId)
                                        .Except(_context.Books.Where(b => b.BookId == bookId).Select(r => r.BookId)).ToList();

            List<GetBookDto> listBookDto = new List<GetBookDto>();
            foreach (var issue in issues)
            {
                GetBookDto bookDto = new GetBookDto();
                var book = await _context.Books.FindAsync(issue);
                bookDto = _mapper.Map<GetBookDto>(book);
                listBookDto.Add(bookDto);

            }
         
            if (listBookDto.Count > 0)
            {
                res.Data = listBookDto;
                res.Message = "List fectched!!";
                res.Success = true;
                res.Status = 200;
            }
            else
            {
                res.Data = null;
                res.Message = "record not found!!";
                res.Success = true;
                res.Status = 200;
            }
            

            return res;
        }


        public async Task<ApiResponse<List<GetBookDto>>> GetMostIssuedBooksByTime(DateTime sDate, DateTime fDate)
        {

            ApiResponse<List<GetBookDto>> res = new ApiResponse<List<GetBookDto>>();
            var issues = _context.Issues.Where(i => i.IssueDate >= sDate && i.IssueDate <= fDate)
                                             .GroupBy(i => i.BookId)
                                             .Select(group => new
                                             {
                                                 Key = group.Key,
                                                 Count = group.Count()
                                             })
                                             .OrderByDescending(x => x.Count).ToList();


            List<GetBookDto> listBookDto = new List<GetBookDto>();
            var highestCount = 0;
            foreach (var issue in issues)
            {
                if (highestCount <= issue.Count) highestCount = issue.Count;
                if (issue.Count >= highestCount)
                {
                    GetBookDto bookDto = new GetBookDto();
                    var book = await _context.Books.FindAsync(issue.Key);
                    bookDto = _mapper.Map<GetBookDto>(book);
                    listBookDto.Add(bookDto);
                }
            }

            if (listBookDto.Count > 0)
            {
                res.Data = listBookDto;            
                res.Message = $"Most Borrowed book(s) in given period start date : {sDate}, finish date: {fDate} with count {highestCount}";
                res.Success = true;
                res.Status = 200;
            }
            else
            {
                res.Data = null;
                res.Message = "record not found!!";
                res.Success = true;
                res.Status = 200;
            }


            return res;
        }

        public async Task<ApiResponse<GetBookDto>> BookAvailable(int id)
        {
            ApiResponse<GetBookDto> res = new ApiResponse<GetBookDto>();
           
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                res.Data = null;
                res.Message = "Error: Book can not be found!";
                res.Success = false;
                res.Status = 404;
                return res;

            }

            //*** :Checks if the book is available to be issued 
            if (!_utility.CheckIfBookAvailable(id))
            {
                res.Message = "Error: Book is unavailable right now. It will be available once user returns it.";
                res.Success = false;
                res.Status = 200;
                return res;
            }
            else
            {
                int count = _utility.GetAvailableCopy(id);
                res.Data = _mapper.Map<GetBookDto>(book);
                res.Message = $"Succes: Book found. No of copy available: {count}";
                res.Success = true;
                res.Status = 200;
                return res;
            }
      
          
        }
    }
}
