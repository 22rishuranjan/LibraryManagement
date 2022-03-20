using AutoMapper.Configuration.Conventions;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UpdateBookDto
    {
     
      
        public string BookTitle { get; set; }
        public string Description { get; set; }
        public bool isAvailable { get; set; }
        public int Count { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
    }
}
