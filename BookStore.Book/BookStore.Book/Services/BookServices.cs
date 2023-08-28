using BookStore.Book.Entity;
using BookStore.Book.Interfaces;
using BookStore.Book.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Book.Services
{
    public class BookServices : IBookServices
    {
        private readonly IConfiguration _config;
        private readonly BookContext _db;

        public BookServices(IConfiguration config,BookContext db)
        {
            _config = config;
            _db = db;

        }

        public BookEntity AddBook(BookModel newBook)
        {
            try
            {
                BookEntity bookEntity = new BookEntity();
                bookEntity.BookName = newBook.BookName;
                bookEntity.AuthorName = newBook.AuthorName;
                bookEntity.Description = newBook.Description;
                bookEntity.Ratings= newBook.Ratings;
                bookEntity.Reviews= newBook.Reviews;
                bookEntity.DiscountedPrice= newBook.DiscountedPrice;
                bookEntity.OriginalPrice= newBook.OriginalPrice;
                bookEntity.Quantity = newBook.Quantity;
              
                _db.Book.Add(bookEntity);
                _db.SaveChanges();
                if (bookEntity != null)
                {
                    return bookEntity;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<BookEntity> GetAllBooks()
        {
            try
            {
                var books = _db.Book.FirstOrDefault();
                if(books != null)
                {
                    
                    return _db.Book.ToList();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteBook(int id)
        {
            try
            {
                BookEntity book = _db.Book.FirstOrDefault(x => x.BookId == id);
                if(book != null)
                {
                    _db.Book.Remove(book);
                    _db.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        public BookEntity GetBookById(int id)
        {
            try
            {

                BookEntity book = _db.Book.FirstOrDefault(x => x.BookId == id);
                if (book != null)
                {
                    
                    return book;

                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public BookEntity UpdateBook(int id, BookModel updatedBook)
        {
            try
            {
                BookEntity book = _db.Book.FirstOrDefault(x => x.BookId == id);
                //BookEntity bookEntity = new BookEntity();
                book.BookName = updatedBook.BookName;
                book.AuthorName = updatedBook.AuthorName;
                book.Description = updatedBook.Description;
                book.Ratings = updatedBook.Ratings;
                book.Reviews = updatedBook.Reviews;
                book.DiscountedPrice = updatedBook.DiscountedPrice;
                book.OriginalPrice = updatedBook.OriginalPrice;
                book.Quantity = updatedBook.Quantity;

                _db.Book.Update(book);
                _db.SaveChanges();
                if (book != null)
                {
                    return book;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GenerateSecurityToken(int adminId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["JWT-Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                  
                    new Claim(ClaimTypes.Email, email),
                  
                    
                     new Claim("adminId", adminId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
