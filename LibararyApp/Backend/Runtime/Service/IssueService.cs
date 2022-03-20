using Application.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Runtime.Common;
using Persistence;
using AutoMapper;
using Domain;
using System.Threading.Tasks;
using Application.DTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Runtime.Service
{
    public class IssueService :IIssue
    {

        private readonly DataContext _context;

        private readonly IMapper _mapper;

        private readonly IUtility _utility;
        public IssueService(DataContext context, IMapper mapper,IUtility utility)
        {
            _context = context;
            _mapper = mapper;
            _utility = utility;
        }
        public async Task<ApiResponse<List<GetIssueDto>>> AddBookIssue(AddIssueDto issue)
        {

            ApiResponse<List<GetIssueDto>> res = new ApiResponse<List<GetIssueDto>>();

            //*** :Checks if the book exists
            if (!_utility.CheckIfBookExist(issue.BookId))
            {
                res.Data = null;
                res.Message = "Error: Book not found";
                res.Success = false;
                res.Status = 200;
                return res;
            }

            //*** :Checks if the user exists
            if (!_utility.CheckIfUserExist(issue.UserId))
            {
                res.Data = null;
                res.Message = "Error: User not found";
                res.Success = false;
                res.Status = 200;
                return res;
            }

            //*** :Checks if the book is available to be issued 
            if (!_utility.CheckIfBookAvailable(issue.BookId))
            {
                res.Message = "Error: Book is unavailable right now. It will be available once user returns it.";
                res.Success = false;
                res.Status = 200;
                return res;
            }

            //*** :map the addDto with the issue class
            Issue _issue = _mapper.Map<Issue>(issue);

            //*** :Checks if user has any book to return
            if (_utility.CheckIfUserIsEligibleToIssue(issue.UserId,issue.BookId))
            {
                _issue.IssueDate = DateTime.Now.ToLocalTime();
                _issue.ExpiryDate = DateTime.Now.ToLocalTime().AddDays(15);
                _context.Add(_issue);
                await _context.SaveChangesAsync();

                //*** :update book availability status if it becomes is unavailable
                UpdateBookDto bookDto =  _utility.GetUpdatedBookDto(issue.BookId);
                var _book = await _context.Books.FindAsync(issue.BookId);
                _mapper.Map(bookDto, _book);
                await _context.SaveChangesAsync();


                res.Data = await _context.Issues.Select(c => _mapper.Map<GetIssueDto>(c)).ToListAsync();
                res.Message = "Success: Book Issue added!";
                res.Success = true;
                res.Status = 201;
            }
            else
            {
                res.Data = null;
                res.Message = "Error: User has already a book issued, no return found";
                res.Success = false;
                res.Status = 200;
            }
            return res;
        }
        public async Task<ApiResponse<GetIssueDto>> DeleteIssues(int id)
        {
            ApiResponse<GetIssueDto> res = new ApiResponse<GetIssueDto>();

            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                res.Data = null;
                res.Message = "Error: Issue can not be found!";
                res.Success = true;
                res.Status = 404;
                return res;

            }

            res.Data = _mapper.Map<GetIssueDto>(issue);
            res.Message = "Issue deleted!!";
            res.Success = true;
            res.Status = 200;

            _context.Remove(issue);
            await _context.SaveChangesAsync();

            return res;
        }
        public async Task<ApiResponse<GetIssueDto>> GetIssueById(int id)
        {
            ApiResponse<GetIssueDto> res = new ApiResponse<GetIssueDto>();

            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                res.Data = null;
                res.Message = "Error: Issue can not be found!";
                res.Success = false;
                res.Status = 404;
                return res;

            }
            res.Data = _mapper.Map<GetIssueDto>(issue);
            res.Message = "Issue found!!";
            res.Success = true;
            res.Status = 200;

            return res;
        }
        public async Task<ApiResponse<List<GetIssueDto>>> GetIssueByBookId(int id)
        {
         
            ApiResponse<List<GetIssueDto>> res = new ApiResponse<List<GetIssueDto>>();
            //var issue = await _context.Issues.Where(i => i.BookId == id).ToListAsync();

            //*** :Checks if the book exists
            if (!_utility.CheckIfBookExist(id))
            {
                res.Data = null;
                res.Message = "Error: Book not found";
                res.Success = false;
                res.Status = 200;
                return res;
            }

            res.Data = await _context.Issues.Where(i => i.BookId == id).Select(c => _mapper.Map<GetIssueDto>(c)).ToListAsync();
            res.Message = "Issue List for selected book fectched!!";
            res.Success = true;
            res.Status = 200;

            return res;
        }
        public async Task<ApiResponse<List<GetIssueDto>>> GetIssueByUserId(int id)
        {
            ApiResponse<List<GetIssueDto>> res = new ApiResponse<List<GetIssueDto>>();

            //*** :Checks if the user exists
            if (!_utility.CheckIfUserExist(id))
            {
                res.Data = null;
                res.Message = "Error: User not found";
                res.Success = false;
                res.Status = 200;
                return res;
            }

            //var issue = await _context.Issues.Where(i => i.BookId == id).ToListAsync();
            res.Data = await _context.Issues.Where(i => i.UserId == id).Select(c => _mapper.Map<GetIssueDto>(c)).ToListAsync();
            res.Message = "Issue List for selected user fectched!!";
            res.Success = true;
            res.Status = 200;

            return res;
        }
        public async Task<ApiResponse<List<GetIssueDto>>> GetIssues()
        {
            ApiResponse<List<GetIssueDto>> res = new ApiResponse<List<GetIssueDto>>();
            res.Data = await _context.Issues.Select(c => _mapper.Map<GetIssueDto>(c)).ToListAsync();
            res.Message = "List fectched!!";
            res.Success = true;
            res.Status = 200;

            return res;
        }
    }
}
