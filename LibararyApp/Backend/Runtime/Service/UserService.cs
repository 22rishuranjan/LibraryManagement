using Application.DTO;
using Application.Interface;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class UserService : ILibUser
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;
        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<ApiResponse<List<GetUserDto>>> AddUser(UpdateUserDto user)
        {
            ApiResponse<List<GetUserDto>> res = new ApiResponse<List<GetUserDto>>();
            User  _user = _mapper.Map<User>(user);
            _context.Add(_user);
            await _context.SaveChangesAsync();
            res.Data = await _context.Users.Select(c => _mapper.Map<GetUserDto>(c)).ToListAsync();
            res.Message = "Success: User added!";
            res.Success = true;
            res.Status = 201;
            return res;
        }
        public async Task<ApiResponse<GetUserDto>> DeleteUser(int id)
        {
            ApiResponse<GetUserDto> res = new ApiResponse<GetUserDto>();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                res.Data = null;
                res.Message = "Error: User can not be found!";
                res.Success = true;
                res.Status = 404;
                return res;

            }

            res.Data = _mapper.Map<GetUserDto>(user);
            res.Message = "User deleted!!";
            res.Success = true;
            res.Status = 200;
            _context.Remove(user);
            await _context.SaveChangesAsync();

            return res;
        }
        public async Task<ApiResponse<GetUserDto>> GetUserById(int id)
        {
            ApiResponse<GetUserDto> res = new ApiResponse<GetUserDto>();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                res.Data = null;
                res.Message = "Error: User can not be found!";
                res.Success = false;
                res.Status = 404;
                return res;

            }
            res.Data = _mapper.Map<GetUserDto>(user);
            res.Message = "User found!!";
            res.Success = true;
            res.Status = 200;
            return res;
        }
        public async Task<ApiResponse<List<GetUserDto>>> GetUsers()
        {
            ApiResponse<List<GetUserDto>> res = new ApiResponse<List<GetUserDto>>();
            res.Data = await _context.Users.Select(c => _mapper.Map<GetUserDto>(c)).ToListAsync();
            res.Message = "List fectched!!";
            res.Success = true;
            res.Status = 200;
            return res;
        }
        public async Task<ApiResponse<GetUserDto>> UpdateUsers(UpdateUserDto user, int id)
        {
            ApiResponse<GetUserDto> res = new ApiResponse<GetUserDto>();

            var _user = await _context.Users.FindAsync(id);
            if (_user == null)
            {
                res.Data = null;
                res.Message = "Error: User can not be found!";
                res.Success = false;
                res.Status = 404;
                return res;
            }


            _mapper.Map(user, _user);
            await _context.SaveChangesAsync();
            res.Data = _mapper.Map<GetUserDto>(await _context.Users.FindAsync(id));
            res.Message = "Success, User is successfully updated.";
            res.Success = true;
            res.Status = 200;
            return res;
        }
        public async Task<ApiResponse<List<GetUserDto>>> GetUserWithMostIssuedBooks()
        {

            ApiResponse<List<GetUserDto>> res = new ApiResponse<List<GetUserDto>>();
            var issues = _context.Issues.GroupBy(i => i.UserId)
                                             .Select(group => new
                                             {
                                                 Key = group.Key,
                                                 Count = group.Count()
                                             }).OrderByDescending(x => x.Count).ToList();


            List<GetUserDto> listUserDto = new List<GetUserDto>();
            var highestCount = 0;
            foreach (var issue in issues)
            {
                if (highestCount <= issue.Count) highestCount = issue.Count;
                if (issue.Count >= highestCount)
                {
                    GetUserDto userDto = new GetUserDto();
                    var user = await _context.Users.FindAsync(issue.Key);
                    userDto = _mapper.Map<GetUserDto>(user);
                    listUserDto.Add(userDto);
                }
            }

      

            if (listUserDto.Count > 0)
            {
                res.Data = listUserDto;
                res.Message = $"List of users that borrowed most book(s) with count {highestCount}";
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
        public async Task<ApiResponse<List<GetUserDto>>> GetUserWithMostIssuedBooksByTime(DateTime sDate, DateTime fDate)
        {

            ApiResponse<List<GetUserDto>> res = new ApiResponse<List<GetUserDto>>();
            var issues = _context.Issues.Where(i=>i.IssueDate >= sDate && i.IssueDate<=fDate).GroupBy(i => i.UserId)
                                             .Select(group => new
                                             {
                                                 Key = group.Key,
                                                 Count = group.Count()
                                             }).OrderByDescending(x => x.Count).ToList();


            List<GetUserDto> listUserDto = new List<GetUserDto>();
            var highestCount = 0;
            foreach (var issue in issues)
            {
                if (highestCount <= issue.Count) highestCount = issue.Count;
                if (issue.Count >= highestCount)
                {
                    GetUserDto userDto = new GetUserDto();
                    var user = await _context.Users.FindAsync(issue.Key);
                    userDto = _mapper.Map<GetUserDto>(user);
                    listUserDto.Add(userDto);
                }
            }

          

            if (listUserDto.Count > 0)
            {
                res.Data = listUserDto;
                res.Message = $"User that Borrowed book(s) in given period start date : {sDate}, finish date: {fDate} with count {highestCount}";
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
    }
}
