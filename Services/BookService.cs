using BooksService.Clients;
using BooksService.Models;
using BooksService.Request;

namespace BooksService.Services;

public class BookService : IBookService
{
    private readonly ILogger<BookService> _logger;
    public BookService(ILogger<BookService> logger)
    {
        _logger = logger;
    }

    public async Task<List<Book>> FindAllBooks(string? timezone)
    {
        try
        {
            var client = BookClient.Instance.Client;
            var reply = await client.GetBooksAsync(new GBooksRequest { });

            _logger.Log(LogLevel.Information, reply.Books.ToString());

            List<Book> books = [];

            foreach (var r in reply.Books)
            {
                var item = r.Book;
                var createdAt = DateTime.Parse(item.CreatedAt);
                var updatedAt = DateTime.Parse(item.UpdatedAt);

                if (timezone != null)
                {
                    createdAt = TimeZoneInfo.ConvertTimeFromUtc(createdAt, TimeZoneInfo.FindSystemTimeZoneById(timezone));
                    updatedAt = TimeZoneInfo.ConvertTimeFromUtc(updatedAt, TimeZoneInfo.FindSystemTimeZoneById(timezone));
                }

                Book book = new()
                {
                    Id = (int)item.Id,
                    Uid = item.Uid,
                    Title = item.Title,
                    Author = item.Author,
                    Price = item.Price,
                    CreatedAt = createdAt,
                    UpdatedAt = updatedAt,
                };

                books.Add(book);
            }

            return books;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return [];
        }
    }

    public async Task<Book?> FindBookByUid(string uid, string? timezone)
    {
        try
        {
            var client = BookClient.Instance.Client;
            var reply = await client.GetBookByUidAsync(new GBookByUidRequest { Uid = uid });

            if (reply == null || reply.Book == null)
            {
                return null;
            }

            _logger.Log(LogLevel.Information, reply.Book.ToString());

            var item = reply.Book;
            var createdAt = DateTime.Parse(item.CreatedAt);
            var updatedAt = DateTime.Parse(item.UpdatedAt);

            if (timezone != null)
            {
                createdAt = TimeZoneInfo.ConvertTimeFromUtc(createdAt, TimeZoneInfo.FindSystemTimeZoneById(timezone));
                updatedAt = TimeZoneInfo.ConvertTimeFromUtc(updatedAt, TimeZoneInfo.FindSystemTimeZoneById(timezone));
            }

            Book book = new()
            {
                Id = (int)item.Id,
                Uid = item.Uid,
                Title = item.Title,
                Author = item.Author,
                Price = item.Price,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt,
            };

            return book;

        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<Book?> CreateBook(CreateBookRequest body, string? timezone)
    {
        try
        {
            var client = BookClient.Instance.Client;
            var reply = await client.CreateBookAsync(new GCreateBookRequest
            {
                Body = new GWriteBook
                {
                    Title = body.Title,
                    Author = body.Author,
                    Price = body.Price,
                    CategoryId = body.CategoryId,
                }
            });

            _logger.Log(LogLevel.Information, reply.Book.ToString());

            var item = reply.Book;

            var createdAt = DateTime.Parse(item.CreatedAt);
            var updatedAt = DateTime.Parse(item.UpdatedAt);

            if (timezone != null)
            {
                createdAt = TimeZoneInfo.ConvertTimeFromUtc(createdAt, TimeZoneInfo.FindSystemTimeZoneById(timezone));
                updatedAt = TimeZoneInfo.ConvertTimeFromUtc(updatedAt, TimeZoneInfo.FindSystemTimeZoneById(timezone));
            }

            Book book = new()
            {
                Id = (int)item.Id,
                Uid = item.Uid,
                Title = item.Title,
                Author = item.Author,
                Price = item.Price,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt,
            };

            return book;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<Book?> UpdateBook(string uid, CreateBookRequest body, string? timezone)
    {
        try
        {
            var client = BookClient.Instance.Client;
            var reply = await client.UpdateBookAsync(new GUpdateBookRequest
            {
                Uid = uid,
                Body = new GWriteBook
                {
                    Title = body.Title,
                    Author = body.Author,
                    Price = body.Price,
                    CategoryId = body.CategoryId,
                }
            });

            _logger.Log(LogLevel.Information, reply.Book.ToString());

            var item = reply.Book;

            var createdAt = DateTime.Parse(item.CreatedAt);
            var updatedAt = DateTime.Parse(item.UpdatedAt);

            if (timezone != null)
            {
                createdAt = TimeZoneInfo.ConvertTimeFromUtc(createdAt, TimeZoneInfo.FindSystemTimeZoneById(timezone));
                updatedAt = TimeZoneInfo.ConvertTimeFromUtc(updatedAt, TimeZoneInfo.FindSystemTimeZoneById(timezone));
            }

            Book book = new()
            {
                Id = (int)item.Id,
                Uid = item.Uid,
                Title = item.Title,
                Author = item.Author,
                Price = item.Price,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt,
            };

            return book;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<int> DeleteBook(string uid)
    {
        try
        {
            var client = BookClient.Instance.Client;
            var getBookReply = await client.GetBookByUidAsync(new GBookByUidRequest { Uid = uid });

            if (getBookReply == null || getBookReply.Book == null)
            {
                _logger.LogError("Book not found");
                return 0;
            }

            var reply = await client.DeleteBookAsync(new GBookByIdRequest
            {
                Id = getBookReply.Book.Id
            });

            _logger.Log(LogLevel.Information, "Rows affected: " + reply.Affected.ToString());

            return (int)reply.Affected;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return 0;
        }
    }
}