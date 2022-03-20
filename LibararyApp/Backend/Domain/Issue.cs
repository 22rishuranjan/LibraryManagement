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

        [Display(Name ="Book")]
        public virtual int BookId { get; set; }

        [Display(Name = "User")]
        public virtual int UserId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
       
    }
}
