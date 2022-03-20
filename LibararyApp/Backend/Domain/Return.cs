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

        [Display(Name = "Book")]
        public virtual int BookId { get; set; }
        [Display(Name = "User")]
        public virtual int UserId { get; set; }

        [Display(Name = "Issue")]
        public virtual int IssueId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
