using Application.DTO;
using Application.Interface;
using AutoMapper;
using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Runtime.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runtime.Service
{
    public class ReturnService : IReturn
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;

        private readonly IUtility _utility;
        public ReturnService(DataContext context, IMapper mapper, IUtility utility)
        {
            _context = context;
            _mapper = mapper;
            _utility = utility;


        }

        #region Functions for Crud Api
        public async Task<ApiResponse<List<GetReturnDto>>> AddBookReturn(AddReturnDto bookReturn)
        {

            ApiResponse<List<GetReturnDto>> res = new ApiResponse<List<GetReturnDto>>();

            //*** :Checks if the book exists
            if (!_utility.CheckIfBookExist(bookReturn.BookId))
            {
                res.Data = null;
                res.Message = "Error: Book not found";
                res.Success = false;
                res.Status = 200;
                return res;
            }

            //*** :Checks if the user exists
            if (!_utility.CheckIfUserExist(bookReturn.UserId))
            {
                res.Data = null;
                res.Message = "Error: User not found";
                res.Success = false;
                res.Status = 200;
                return res;
            }

            //*** :Checks if the issue exists
            if (!_utility.CheckIfIssueExist(bookReturn.IssueId))
            {
                res.Data = null;
                res.Message = "Error: Can't return, this book is either returned or not issued to the user!";
                res.Success = false;
                res.Status = 200;
                return res;
            }

            //*** :map the addDto with the issue class
            Return _return= _mapper.Map<Return>(bookReturn);

            //*** :Checks if user has any book to return
            if (_utility.CheckIfUserIsEligibleToReturn(bookReturn.UserId, bookReturn.BookId,bookReturn.IssueId))
            {
                _return.ReturnDate = DateTime.Now.ToLocalTime();
               
                _context.Add(_return);
                await _context.SaveChangesAsync();

                //*** :update book availability status if it becomes is unavailable
                UpdateBookDto bookDto = _utility.GetUpdatedBookDto(bookReturn.BookId);
                var _book = await _context.Books.FindAsync(bookReturn.BookId);
                _mapper.Map(bookDto, _book);
                await _context.SaveChangesAsync();

                res.Data = await _context.Returns.Select(c => _mapper.Map<GetReturnDto>(c)).ToListAsync();
                res.Message = "Success: Book returned!";
                res.Success = true;
                res.Status = 201;
            }
            else
            {
                res.Data = null;
                res.Message = "Error: This book is not issued to the user, can't return!";
                res.Success = false;
                res.Status = 200;
            }
            return res;
        }
        public async Task<ApiResponse<GetReturnDto>> GetReturnById(int id)
        {
            ApiResponse<GetReturnDto> res = new ApiResponse<GetReturnDto>();

            var returnRecord = await _context.Returns.FindAsync(id);
            if (returnRecord == null)
            {
                res.Data = null;
                res.Message = "Error: Return id can not be found!";
                res.Success = false;
                res.Status = 404;
                return res;

            }
            res.Data = _mapper.Map<GetReturnDto>(returnRecord);
            res.Message = "Return found!!";
            res.Success = true;
            res.Status = 200;

            return res;
        }
        public async Task<ApiResponse<List<GetReturnDto>>> GetBookReturns()
        {
            ApiResponse<List<GetReturnDto>> res = new ApiResponse<List<GetReturnDto>>();
            res.Data = await _context.Returns.Select(c => _mapper.Map<GetReturnDto>(c)).ToListAsync();
            res.Message = "List fectched!!";
            res.Success = true;
            res.Status = 200;

            return res;
        }
        #endregion

        #region Functions for Driver Api
   
        public async Task<ApiResponse<GetBookDto>> GetReadRate(int id)
        {
            ApiResponse<GetBookDto> res = new ApiResponse<GetBookDto>();

            var returnRecord = await _context.Returns.FindAsync(id);
            if (returnRecord == null)
            {
                res.Data = null;
                res.Message = "Error: Return id can not be found!";
                res.Success = false;
                res.Status = 404;
                return res;
            }



            var returns = await _context.Returns.Where(r => r.ReturnId == id).FirstOrDefaultAsync();
            var issue = await _context.Issues.Where(r => r.IssueId == returns.IssueId).FirstOrDefaultAsync();
            var book = await _context.Books.Where(r => r.BookId == returns.BookId).FirstOrDefaultAsync();

         


            //*** :Checks if the book exists
            if (!_utility.CheckIfBookExist(book.BookId))
            {
                res.Data = null;
                res.Message = "Error: Book not found";
                res.Success = false;
                res.Status = 200;
                return res;
            }

            DateTime issueDate = issue.IssueDate;
            DateTime returnDate = returns.ReturnDate;


            TimeSpan ts = returnDate - issueDate;
            var noOfDays = Math.Round(ts.TotalDays) == 0 ? 1 : ts.TotalDays;

            var rate = Math.Round( book.Page / Math.Round(noOfDays),1);

            //var rate =  _context.Databse.ExecuteRawSql(@"select b.bookid,b.page/IIF(dateDiff(day,i.IssueDate,r.ReturnDate)=0,1,dateDiff(day,i.IssueDate,r.ReturnDate)) from issues i 
            //                inner join returns r on i.issueid = r.returnid
            //                inner join books b on b.bookid = r.BookId
            //                where returnid = @id", new SqlParameter("@id", id));


    
            res.Data = _mapper.Map<GetBookDto>(book);

            res.Message = $"Read rate for book- {book.Title} is : {rate} pages per day";
            res.Success = true;
            res.Status = 200;

            return res;
        }
        #endregion

    }
}
