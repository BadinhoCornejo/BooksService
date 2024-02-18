using BooksService.Models;
using BooksService.Request;

namespace BooksService.Services;

public interface IBookService
{
    Task<List<Book>> FindAllBooks(string? timezone);
    Task<Book?> FindBookByUid(string uid, string? timezone);
    Task<Book?> CreateBook(CreateBookRequest body, string? timezone);
    Task<Book?> UpdateBook(string uid, CreateBookRequest body, string? timezone);
    Task<int> DeleteBook(string uid);
}