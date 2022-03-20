using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UpdateIssueDto
    {
      
        public int IssueId { get; set; }

        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
