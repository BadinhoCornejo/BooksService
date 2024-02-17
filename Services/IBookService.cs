using BooksService.Models;

namespace BooksService.Services;

public interface IBookService
{
    Task<List<Book>> FindAllBooks(string? timezone);
}