using Application.DTO;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
   public interface ILibUser
    {

        public Task<ApiResponse<List<GetUserDto>>> GetUsers();

        public Task<ApiResponse<GetUserDto>> UpdateUsers(UpdateUserDto user, int id);

        public Task<ApiResponse<GetUserDto>> DeleteUser(int id);

        public Task<ApiResponse<GetUserDto>> GetUserById(int id);

        public Task<ApiResponse<List<GetUserDto>>> AddUser(UpdateUserDto user);

        public  Task<ApiResponse<List<GetUserDto>>> GetUserWithMostIssuedBooks();

        public Task<ApiResponse<List<GetUserDto>>> GetUserWithMostIssuedBooksByTime(DateTime sDate, DateTime fDate);
    }
}
