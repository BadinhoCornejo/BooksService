using BooksService.Clients;
using BooksService.Models;
using Grpc.Net.Client;

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
        var client = BookClient.Instance.Client;
        var reply = await client.GetBooksAsync(new BooksRequest { });

        _logger.Log(LogLevel.Information, reply.Books.ToString());

        List<Book> books = [];

        foreach (var item in reply.Books)
        {
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

}