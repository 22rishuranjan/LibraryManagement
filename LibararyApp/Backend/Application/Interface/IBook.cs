using Application.DTO;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
   public interface IBook
    {
        public Task<ApiResponse<List<BookDto>>> GetBooks();

        public Task<ApiResponse<BookDto>> UpdateBooks(BookDto book, int id);

        public Task<ApiResponse<BookDto>> DeleteBooks(int id);

        public Task<ApiResponse<BookDto>> GetBookById(int id);

        public Task<ApiResponse<List<BookDto>>> AddBook(BookDto book);
    }
}
