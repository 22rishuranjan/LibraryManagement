using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public int Count { get; set; }
        public string Author { get; set; }
        public string Area { get; set; }
        public int Page { get; set; }
    }
}
