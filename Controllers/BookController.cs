using BooksService.Models;
using BooksService.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksService.Controllers;

[ApiController]
[Route("v1/book")]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    private readonly IBookService _bookService;

    public BookController(ILogger<BookController> logger, IBookService bookService)
    {
        _logger = logger;
        _bookService = bookService;
    }

    [HttpGet(Name = "GetBooks")]
    public async Task<List<Book>> Get()
    {
        var timezone = Request.Headers["X-Timezone"].ToString();
        var books = await _bookService.FindAllBooks(timezone);

        return books;
    }
}
