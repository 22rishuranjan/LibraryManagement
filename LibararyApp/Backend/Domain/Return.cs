using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Return
    {
        public int ReturnId { get; set; }

      
        public  int BookId { get; set; }
        public  int UserId { get; set; }
        public  int IssueId { get; set; }

        [ForeignKey("IssueId")]
        public virtual Issue Issues { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
