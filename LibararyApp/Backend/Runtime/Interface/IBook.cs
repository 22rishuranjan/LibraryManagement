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
        public Task<ApiResponse<List<GetBookDto>>> GetBooks();

        public Task<ApiResponse<GetBookDto>> UpdateBooks(UpdateBookDto book, int id);

        public Task<ApiResponse<GetBookDto>> DeleteBooks(int id);

        public Task<ApiResponse<GetBookDto>> GetBookById(int id);

        public Task<ApiResponse<List<GetBookDto>>> AddBook(GetBookDto book);

        public Task<ApiResponse<List<GetBookDto>>> GetMostIssuedBooks();
        public Task<ApiResponse<List<GetBookDto>>> GetOtherBooks(int bookId, int userId);

        public Task<ApiResponse<List<GetBookDto>>> GetMostIssuedBooksByTime(DateTime sDate, DateTime fDate);

        public  Task<ApiResponse<GetBookDto>> BookAvailable(int id);
    }
}
