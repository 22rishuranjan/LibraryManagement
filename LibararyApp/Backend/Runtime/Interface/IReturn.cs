using Application.DTO;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IReturn
    {
        public Task<ApiResponse<List<GetReturnDto>>> AddBookReturn(AddReturnDto issue);
        public Task<ApiResponse<List<GetReturnDto>>> GetBookReturns();
        public Task<ApiResponse<GetReturnDto>> GetReturnById(int id);
    }
}
