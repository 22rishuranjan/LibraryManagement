using Domain;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (!context.Users.Any())
            {


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


                var books = new List<Book>
                {
                    new Book
                    {
                        //Id = 101,
                        Title = "DAA",
                        Description = "Design and Algorithms",
                        Author = "M Martin",
                        IsAvailable = true,
                        Count = 1,
                        Area = "Computer Science"

                    },

                    new Book
                    {
                        //Id = 101,
                        Title = "Rich Dad Poor Dad",
                        Description = "It advocates the importance of financial literacy, financial independence and building wealth through investing in assets, real estate investing, starting and owning businesses, as well as increasing one's financial intelligence.",
                        Author = "Robert T. Kiyosaki",
                        IsAvailable = true,
                        Count = 2,
                        Area = "Investment"

                    },
                     new Book
                    {
                        //Id = 101,
                        Title = "Harry Potter and the Philosopher's Stone",
                        Description = "When mysterious letters start arriving on his doorstep, Harry Potter has never heard of Hogwarts School of Witchcraft and Wizardry.",
                        Author = "J. K. Rowling",
                        IsAvailable = true,
                        Count = 1,
                        Area = "Fantasy, Magic"

                    },
                      new Book
                    {
                        //Id = 101,
                        Title = "Harry Potter and the Chamber of Secrets",
                        Description = "Throughout the summer holidays after his first year at Hogwarts School of Witchcraft and Wizardry, Harry Potter has been receiving sinister warnings from a..",
                        Author = "J. K. Rowling",
                        IsAvailable = true,
                        Count = 1,
                        Area = "Fantasy, Magic"

                    },

                         new Book
                    {
                        //Id = 101,
                        Title = "The Shining",
                        Description = "Before Doctor Sleep, there was The Shining, a classic of modern American horror from the undisputed master, Stephen Kin",
                        Author = " Stephen King",
                        IsAvailable = true,
                        Count = 5,
                        Area = "Horror"

                    },



                };

                //await context.Users.AddRangeAsync(users);
                //await context.SaveChangesAsync();
                await context.BulkInsertAsync(books);

            }
        }
    }
}
