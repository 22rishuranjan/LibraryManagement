using Domain;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (!context.Users.Any())
            {

                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Users', RESEED, 0)");
                var users = new List<User>
                {
                    new User
                    {
                        //Id = 101,
                        Name = "Alok Kumar",
                        Email = "alok.kumar@gmail.com",
                        mobile = "+91-882645638",
                        City = "Mumbai",
                        Age = 27,
                        FullAddress = "City Center,Mumbai, 820001 "

                    },
                      new User
                    {
                        //Id = 102,
                        Name = "Aditi Sharma",
                        Email = "aditi.sharma@gmail.com",
                        mobile = "+91-972645638",
                        City = "Bengalore",
                        Age = 34,
                        FullAddress = "221 Street, Bengalore, 220021"

                    },
                        
                     new User
                    {
                        //Id = 102,
                        Name = "John Cena",
                        Email = "ucantseeme@gmail.com",
                        mobile = "+1-72645638",
                        City = "Callifornia",
                        Age = 38,
                        FullAddress = "st. Lois Street, California, USA"

                    },

                     new User
                    {
                        //Id = 102,
                        Name = "S Holmes",
                        Email = "holmes@gmail.com",
                        mobile = "+44-72645638",
                        City = "london",
                        Age = 22,
                        FullAddress = "221 Baker Street, London, UK"

                    },

                      new User
                    {
                        //Id = 102,
                        Name = "J Connor",
                        Email = "connor@outlook.com",
                        mobile = "+44-72645638",
                        City = "london",
                        Age = 45,
                        FullAddress = "BS Garden, Manchester, UK"

                    }

                };

                //await context.Users.AddRangeAsync(users);
                //await context.SaveChangesAsync();
               await context.BulkInsertAsync(users);

            }

            if (!context.Books.Any())
            {

                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Books', RESEED, 0)");
                var books = new List<Book>
                {
                     new Book
                    {
                        //Id = 101,
                        Title = "DAA",
                        Description = "Design and Algorithms",
                        Author = "M Martin",
                        IsAvailable = true,
                        Count = 3,
                        Area = "Computer Science",
                        Page = 200

                    },
                     new Book
                    {
                        //Id = 101,
                        Title = "Rich Dad Poor Dad",
                        Description = "It advocates the importance of financial literacy, financial independence and building wealth through investing in assets, real estate investing, starting and owning businesses, as well as increasing one's financial intelligence.",
                        Author = "Robert T. Kiyosaki",
                        IsAvailable = true,
                        Count = 2,
                        Area = "Investment",
                        Page = 250

                    },
                     new Book
                    {
                        //Id = 101,
                        Title = "Harry Potter and the Philosopher's Stone",
                        Description = "When mysterious letters start arriving on his doorstep, Harry Potter has never heard of Hogwarts School of Witchcraft and Wizardry.",
                        Author = "J. K. Rowling",
                        IsAvailable = true,
                        Count = 10,
                        Area = "Fantasy, Magic",
                        Page = 250

                    },
                     new Book
                    {
                        //Id = 101,
                        Title = "Harry Potter and the Chamber of Secrets",
                        Description = "Throughout the summer holidays after his first year at Hogwarts School of Witchcraft and Wizardry, Harry Potter has been receiving sinister warnings from a..",
                        Author = "J. K. Rowling",
                        IsAvailable = true,
                        Count = 2,
                        Area = "Fantasy, Magic",
                        Page = 400

                    },
                     new Book
                    {
                        //Id = 101,
                        Title = "The Shining",
                        Description = "Before Doctor Sleep, there was The Shining, a classic of modern American horror from the undisputed master, Stephen Kin",
                        Author = " Stephen King",
                        IsAvailable = true,
                        Count = 5,
                        Area = "Horror",
                        Page = 100

                    },
                };

                //await context.Users.AddRangeAsync(users);
                //await context.SaveChangesAsync();
                await context.BulkInsertAsync(books);

            }

            if (!context.Issues.Any())
            {
                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Issues', RESEED,0)");

                var issue = new List<Issue>
                {
                     new Issue
                    { 
                        BookId = 1,
                        UserId = 1,
                        IssueDate = DateTime.Now.AddDays(-35).ToLocalTime(),
                        ExpiryDate=  DateTime.Now.AddDays(-20).ToLocalTime()

                    },

                       new Issue
                    {
                        BookId = 2,
                        UserId = 2,
                        IssueDate = DateTime.Now.AddDays(-135).ToLocalTime(),
                        ExpiryDate=  DateTime.Now.AddDays(-120).ToLocalTime()

                    },
                           new Issue
                    {
                        BookId = 3,
                        UserId = 3,
                        IssueDate = DateTime.Now.AddDays(-2).ToLocalTime(),
                        ExpiryDate=  DateTime.Now.AddDays(13).ToLocalTime()

                    },

                                new Issue
                    {
                        BookId = 1,
                        UserId = 4,
                        IssueDate = DateTime.Now.AddDays(-1).ToLocalTime(),
                        ExpiryDate=  DateTime.Now.AddDays(-14).ToLocalTime()

                    },

                                     new Issue
                    {
                        BookId = 1,
                        UserId = 5,
                        IssueDate = DateTime.Now.AddDays(1).ToLocalTime(),
                        ExpiryDate=  DateTime.Now.AddDays(16).ToLocalTime()

                    }

                };

                //await context.Users.AddRangeAsync(users);
                //await context.SaveChangesAsync();
                await context.BulkInsertAsync(issue);

            }

            if (!context.Returns.Any())
            {

                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Returns', RESEED, 0)");
                var returns = new List<Return>
                {
                     new Return
                    {
                        BookId = 1,
                        UserId = 1,
                        IssueId =1,
                        ReturnDate = DateTime.Now.ToLocalTime()
                    },
                      new Return
                    {
                        BookId = 2,
                        UserId = 2,
                        IssueId =2,
                        ReturnDate = DateTime.Now.ToLocalTime()
                    },
                        new Return
                    {
                        BookId = 3,
                        UserId = 3,
                        IssueId =3,
                        ReturnDate = DateTime.Now.ToLocalTime()
                    },


                };

                //await context.Users.AddRangeAsync(users);
                //await context.SaveChangesAsync();
                await context.BulkInsertAsync(returns);

            }
        }
    }
}
