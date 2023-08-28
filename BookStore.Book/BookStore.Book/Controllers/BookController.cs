using BookStore.Book.Entity;
using BookStore.Book.Interfaces;
using BookStore.Book.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookServices _book;
        private readonly IConfiguration _config;
        private readonly BookContext _db;
        private readonly ResponseEntity response;

        public BookController(IConfiguration config, BookContext db, IBookServices book)
        {
            _book = book;
            _config = config;
            _db = db;
            response = new ResponseEntity();

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("addbook")]
        public ResponseEntity AddBook(BookModel bookModel)
        {
            try
            {
                var result = _book.AddBook(bookModel);

                if (result != null)
                {
                    response.Data = result;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("getall")]
        public ResponseEntity GetAll()
        {
            try
            {
                IEnumerable<BookEntity> result = _book.GetAllBooks();

                if (result != null)
                {
                    response.Data = result;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
      
        [HttpGet]
        [Route("getbyId")]
        public ResponseEntity GetById(int id)
        {
            try
            {
                var result = _book.GetBookById(id);

                if (result != null)
                {
                    response.Data = result;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize(Roles ="Admin")]
        [HttpPut]
        [Route("edit")]
        public ResponseEntity Edit(int id, BookModel updatedBook)
        {
            try
            {
                var result = _book.UpdateBook(id,updatedBook);

                if (result != null)
                {
                    response.Data = result;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete]
        [Route("delete")]
        public ResponseEntity Delete(int id)
        {
            try
            {
                var result = _book.DeleteBook(id);

                if (result != null)
                {
                    response.Data = result;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}


