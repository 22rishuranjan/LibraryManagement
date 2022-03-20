using Application.DTO;
using Application.Interface;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class BookService :IBook
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;
        public BookService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<ApiResponse<List<BookDto>>> AddBook(BookDto book)
        {
            ApiResponse<List<BookDto>> res = new ApiResponse<List<BookDto>>();
            Book _book = _mapper.Map<Book>(book);
            _context.Add(_book);
            await _context.SaveChangesAsync();
            res.Data = await _context.Books.Select(c => _mapper.Map<BookDto>(c)).ToListAsync();
            res.Message = "Success: Acivtity added!";
            res.Success = true;

            return res;
        }

        public async Task<ApiResponse<BookDto>> DeleteBooks(int id)
        {
            ApiResponse<BookDto> res = new ApiResponse<BookDto>();

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                res.Data = null;
                res.Message = "Error: Book can not be found!";
                res.Success = true;

                return res;

            }

            res.Data = _mapper.Map<BookDto>(book);
            res.Message = "User deleted!!";
            res.Success = true;

            _context.Remove(book);
            await _context.SaveChangesAsync();

            return res;
        }

        //public Task<ApiResponse<BookDto>> DeleteBooks(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ApiResponse<BookDto>> GetBookById(int id)
        {
            ApiResponse<BookDto> res = new ApiResponse<BookDto>();

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                res.Data = null;
                res.Message = "Error: Book can not be found!";
                res.Success = false;

                return res;

            }
            res.Data = _mapper.Map<BookDto>(book);
            res.Message = "Book found!!";
            res.Success = true;

            return res;
        }

        public async Task<ApiResponse<List<BookDto>>> GetBooks()
        {
            ApiResponse<List<BookDto>> res = new ApiResponse<List<BookDto>>();
            res.Data = await _context.Books.Select(c => _mapper.Map<BookDto>(c)).ToListAsync();
            res.Message = "List fectched!!";
            res.Success = true;

            return res;
        }

        public async Task<ApiResponse<BookDto>> UpdateBooks(BookDto book, int id)
        {
            ApiResponse<BookDto> res = new ApiResponse<BookDto>();

            var _book = await _context.Books.FindAsync(id);
            if (_book == null)
            {
                res.Data = null;
                res.Message = "Error: Book can not be found!";
                res.Success = false;

                return res;
            }


            _mapper.Map(book, _book);
            await _context.SaveChangesAsync();
            res.Data = _mapper.Map<BookDto>(await _context.Books.FindAsync(id));
            res.Message = "Success, Book is successfully updated.";
            res.Success = true;
            return res;
        }
    }
}
