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

        public async Task<ApiResponse<List<UserDto>>> AddUser(UserDto user)
        {
            ApiResponse<List<UserDto>> res = new ApiResponse<List<UserDto>>();
            User  _user = _mapper.Map<User>(user);
            _context.Add(_user);
            await _context.SaveChangesAsync();
            res.Data = await _context.Users.Select(c => _mapper.Map<UserDto>(c)).ToListAsync();
            res.Message = "Success: Acivtity added!";
            res.Success = true;

            return res;
        }

        public async Task<ApiResponse<UserDto>> DeleteUser(int id)
        {
            ApiResponse<UserDto> res = new ApiResponse<UserDto>();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                res.Data = null;
                res.Message = "Error: User can not be found!";
                res.Success = true;

                return res;

            }

            res.Data = _mapper.Map<UserDto>(user);
            res.Message = "User deleted!!";
            res.Success = true;

            _context.Remove(user);
            await _context.SaveChangesAsync();

            return res;
        }

        public async Task<ApiResponse<UserDto>> GetUserById(int id)
        {
            ApiResponse<UserDto> res = new ApiResponse<UserDto>();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                res.Data = null;
                res.Message = "Error: User can not be found!";
                res.Success = false;

                return res;

            }
            res.Data = _mapper.Map<UserDto>(user);
            res.Message = "User found!!";
            res.Success = true;

            return res;
        }

        public async Task<ApiResponse<List<UserDto>>> GetUsers()
        {
            ApiResponse<List<UserDto>> res = new ApiResponse<List<UserDto>>();
            res.Data = await _context.Users.Select(c => _mapper.Map<UserDto>(c)).ToListAsync();
            res.Message = "List fectched!!";
            res.Success = true;

            return res;
        }

        public async Task<ApiResponse<UserDto>> UpdateUsers(UserDto user, int id)
        {
            ApiResponse<UserDto> res = new ApiResponse<UserDto>();

            var _user = await _context.Users.FindAsync(id);
            if (_user == null)
            {
                res.Data = null;
                res.Message = "Error: User can not be found!";
                res.Success = false;

                return res;
            }


            _mapper.Map(user, _user);
            await _context.SaveChangesAsync();
            res.Data = _mapper.Map<UserDto>(await _context.Users.FindAsync(id));
            res.Message = "Success, User is successfully updated.";
            res.Success = true;
            return res;
        }
    }
}
