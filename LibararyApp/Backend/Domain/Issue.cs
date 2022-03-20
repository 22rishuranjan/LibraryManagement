using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Issue
    {
   
        public int IssueId { get; set; }

       
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Books { get; set; }

      
        public  int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User Users { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
       
    }
}
