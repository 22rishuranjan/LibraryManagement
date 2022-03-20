using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ReturnDTO
    {
        public int ReturnId { get; set; }

       
        public int BookId { get; set; }
     
        public int UserId { get; set; }

      
        public int IssueId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
