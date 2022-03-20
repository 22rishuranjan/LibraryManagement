using Application.DTO;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Runtime.Common
{
    public interface IUtility
    {
        public  bool CheckIfBookExist(int id);
        public bool CheckIfUserExist(int id);
        public bool CheckIfIssueExist(int id);
        public bool CheckIfBookAvailable(int id);
        public bool CheckIfUserIsEligibleToIssue(int userId, int bookId);
        public UpdateBookDto GetUpdatedBookDto(int id);
        public bool CheckIfUserIsEligibleToReturn(int userId, int bookId, int issueId);
        public int GetAvailableCopy(int id);



    }
}
