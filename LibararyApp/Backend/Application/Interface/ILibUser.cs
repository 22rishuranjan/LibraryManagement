using Application.DTO;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
   public interface ILibUser
    {

        public Task<ApiResponse<List<UserDto>>> GetUsers();

        public Task<ApiResponse<UserDto>> UpdateUsers(UserDto user, int id);

        public Task<ApiResponse<UserDto>> DeleteUser(int id);

        public Task<ApiResponse<UserDto>> GetUserById(int id);

        public Task<ApiResponse<List<UserDto>>> AddUser(UserDto user);
    }
}
