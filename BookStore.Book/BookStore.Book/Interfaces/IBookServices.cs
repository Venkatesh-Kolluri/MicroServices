using BookStore.Book.Entity;
using BookStore.Book.Models;

namespace BookStore.Book.Interfaces
{
    public interface IBookServices
    {
        BookEntity AddBook(BookModel newBook);
        BookEntity GetBookById(int id);
        IEnumerable<BookEntity> GetAllBooks();
        BookEntity UpdateBook(int id, BookModel updatedBook);
        bool DeleteBook(int id);
    }
}
