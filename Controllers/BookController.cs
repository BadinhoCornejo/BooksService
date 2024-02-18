using BooksService.Models;
using BooksService.Request;
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

    [HttpGet]
    public async Task<List<Book>> Get()
    {
        var timezone = Request.Headers["X-Timezone"].ToString();
        var books = await _bookService.FindAllBooks(timezone);

        return books;
    }

    [HttpGet("{uid}")]
    public async Task<ActionResult<Book>> GetByUid(string uid)
    {
        var timezone = Request.Headers["X-Timezone"].ToString();
        var book = await _bookService.FindBookByUid(uid, timezone);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<ActionResult<Book>> Post(CreateBookRequest body)
    {
        var timezone = Request.Headers["X-Timezone"].ToString();
        var book = await _bookService.CreateBook(body, timezone);

        if (book == null)
        {
            return BadRequest();
        }

        return book;
    }

    [HttpPut("{uid}")]
    public async Task<ActionResult<Book>> Put(string uid, CreateBookRequest body)
    {
        var timezone = Request.Headers["X-Timezone"].ToString();
        var book = await _bookService.UpdateBook(uid, body, timezone);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpDelete("{uid}")]
    public async Task<ActionResult<int>> Delete(string uid)
    {
        var affected = await _bookService.DeleteBook(uid);

        if (affected == 0)
        {
            return NotFound();
        }

        return affected;
    }
}
