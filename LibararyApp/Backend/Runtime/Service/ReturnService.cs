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


        //public Task<ApiResponse<BookDto>> DeleteReturns(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ApiResponse<GetReturnDto>> GetReturnById(int id)
        {
            ApiResponse<GetReturnDto> res = new ApiResponse<GetReturnDto>();

            var issue = await _context.Returns.FindAsync(id);
            if (issue == null)
            {
                res.Data = null;
                res.Message = "Error: Return id can not be found!";
                res.Success = false;
                res.Status = 404;
                return res;

            }
            res.Data = _mapper.Map<GetReturnDto>(issue);
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

        //public async Task<ApiResponse<GetReturnDto>> UpdateReturns(UpdateBookDto issue, int id)
        //{
        //    ApiResponse<GetReturnDto> res = new ApiResponse<GetReturnDto>();

        //    var _issue = await _context.Returns.FindAsync(id);
        //    if (_issue == null)
        //    {
        //        res.Data = null;
        //        res.Message = "Error: Issue can not be found!";
        //        res.Success = false;
        //        res.Status = 404;
        //        return res;
        //    }


        //    _mapper.Map(issue, _issue);
        //    await _context.SaveChangesAsync();
        //    res.Data = _mapper.Map<GetReturnDto>(await _context.Returns.FindAsync(id));
        //    res.Message = "Success, Book is successfully updated.";
        //    res.Success = true;
        //    return res;
        //}
    }
}
