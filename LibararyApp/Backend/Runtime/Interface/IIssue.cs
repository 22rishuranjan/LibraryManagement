using Application.DTO;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IIssue
    {

        public Task<ApiResponse<List<GetIssueDto>>> AddBookIssue(AddIssueDto issue);
        public Task<ApiResponse<List<GetIssueDto>>> GetIssues();
        public Task<ApiResponse<GetIssueDto>> GetIssueById(int id);
        public Task<ApiResponse<GetIssueDto>> DeleteIssues(int id);
        public  Task<ApiResponse<List<GetIssueDto>>> GetIssueByUserId(int id);

        public Task<ApiResponse<List<GetIssueDto>>> GetIssueByBookId(int id);





    }
}
