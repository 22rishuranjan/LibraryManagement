using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UpdateReturnDto
    {
       
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int IssueId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
