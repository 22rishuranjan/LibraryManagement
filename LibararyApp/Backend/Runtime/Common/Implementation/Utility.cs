using Application.DTO;
using Persistence;
using System.Linq;

namespace Runtime.Common
{
    public class Utility : IUtility
    {

        private readonly DataContext _context;
        //private readonly IBook _bookService;

        public Utility(DataContext context)
        {
            _context = context;
            //_bookService = bookService;

        }

        //*** :To check if user is Eligible to Issue Books
        //*** :if user already has a book issued, then he can't issue another book

        public bool CheckIfUserIsEligibleToIssue(int userId, int bookId)
        {
            var count = _context.Issues.Where(i => i.UserId == userId)
                                       .Select(i => i.IssueId)
                                       .Except(_context.Returns.Select(r => r.IssueId))
                                       .Count();
            if (count > 0)
            {

                return false;
                //var record = (from iss
                //              in _context.Issues
                //              join
                //              ret in _context.Returns
                //              on new { iss.IssueId, iss.UserId, iss.BookId } equals new { ret.IssueId, ret.UserId,ret.BookId } into t
                //              from result in t.DefaultIfEmpty()
                //              where iss.UserId == userId && iss.BookId == bookId && result.ReturnId != null
                //              select new
                //               {
                //                   userId = iss.UserId,
                //                   issueId = iss.IssueId,
                //                   bookId = iss.BookId,
                //                   issueDate = iss.IssueDate,
                //                   returnDate = result.ReturnDate,
                //                   returnId = result.ReturnId 
                //                }) ;

                //if (record.ToList().Count ==0) return false;

            }
            return true;
        }

        //*** :To check if user is Eligible to Return Books
        //*** :if user already has a book issued, then he can return the book
        public bool CheckIfUserIsEligibleToReturn(int userId, int bookId, int issueId)
        {
            //check if book was issued to user
            var countIfBookIssued = _context.Issues.Where(i => i.UserId == userId && i.BookId == bookId && i.IssueId == issueId).Count();

            //check if book was returned
            var countIfBookReturned = _context.Returns.Where(r => r.UserId == userId && r.BookId == bookId && r.IssueId == issueId).Count();
            if (countIfBookIssued > countIfBookReturned)
            {

                return true;

            }
            return false; ;
        }
        //*** :To check if searched user exists 
        public bool CheckIfUserExist(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        //*** :To check if searched book exists 
        public bool CheckIfBookExist(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return false;
            }
            return true;
        }

        //*** :To check if searched issue exists 
        public bool CheckIfIssueExist(int id)
        {
            var user = _context.Issues.Find(id);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        //*** :To check if searched book is available to be issued
        public bool CheckIfBookAvailable(int id)
        {
            var selectBook = _context.Books
                                    .Where(i => i.BookId == id)
                                    .Select(p => new { BookId = p.BookId, IsAvailable = p.IsAvailable }).ToList();
            foreach (var book in selectBook)
            {
                if (book.IsAvailable == false)
                {
                    return false;
                }


            }
            return true;
        }

        //*** :To update status, if book reaches max no to be issued
        public UpdateBookDto GetUpdatedBookDto(int id)
        {
            //*** :IF book Exist
            var book = _context.Books.Find(id);

            //var books = _context.Books.Where(i => i.BookId ==  id).ToList();

            var bookCount = book.Count;
            //var issueRecords = _context.Issues.Where(i => i.BookId == id);


            var issueCount = _context.Issues.Where(i => i.BookId == id)
                                       .Select(i => i.IssueId)
                                       .Except(_context.Returns.Select(r => r.IssueId))
                                       .Count();



            if (!(bookCount > issueCount))
            {

                UpdateBookDto updateDto = new UpdateBookDto
                {
                    BookTitle = book.Title,
                    isAvailable = false,
                    Count = book.Count,
                    Author = book.Author,
                    Description = book.Description,
                    Genre = book.Area
                };

                //_bookService.UpdateBooks(updateDto, id);

                return updateDto;
            }
            else
            {
                UpdateBookDto updateDto = new UpdateBookDto
                {
                    BookTitle = book.Title,
                    isAvailable = true,
                    Count = book.Count,
                    Author = book.Author,
                    Description = book.Description,
                    Genre = book.Area
                };

                // _bookService.UpdateBooks(updateDto, id);

                return updateDto;
            }


            return new UpdateBookDto();



            /*
            var bookFromIssue = (from b in _context.Books
                                 join
                                 i in _context.Issues on b.BookId equals i.BookId into booksIssues
                                 from result in booksIssues.DefaultIfEmpty()
                                 where b.BookId == id && result.IssueId == 0
                                 select new
                                 {
                                     bookId = b.BookId,
                                     issueId = result.IssueId,
                                     isAvailable = b.IsAvailable,
                                     count = b.Count
                                 }) ;

            var bookFromIssueCount = bookFromIssue.Count();

            if (bookFromIssueCount > 0)
            {

            }
            else
            {
                var bookFromReturn = (from b in _context.Books
                                      join
                                      r in _context.Returns on b.BookId equals r.BookId into booksReturns
                                      from result in booksReturns.DefaultIfEmpty()
                                      where b.BookId == id && result.ReturnId == 0
                                      select new
                                      {
                                          bookId = b.BookId,
                                          issueId = result.IssueId,
                                          returnId = result.ReturnId,
                                          isAvailable = b.IsAvailable,
                                          count = b.Count
                                      });


                var bookFromReturnCount = bookFromReturn.Count();

                if (bookFromReturnCount > 0)
                {

                }

               
            }
            */



        }

        //*** :To check how many available copy of books are present
        public int GetAvailableCopy(int id)
        {
            //*** :IF book Exist
            var book = _context.Books.Find(id);
            if (book != null)
            {

                //*** :total copy available
                var bookCount = book.Count;


                //*** :total copy issued
                var issueCount = _context.Issues.Where(i => i.BookId == id)
                                           .Select(i => i.IssueId)
                                           .Except(_context.Returns.Select(r => r.IssueId))
                                           .Count();

                return bookCount - issueCount;
            }
            return 0;
        }


    }
}


