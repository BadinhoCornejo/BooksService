namespace BooksService.Request;

public class CreateBookRequest
{
    public string Title { get; set; }
    public string Author { get; set; }
    public float Price { get; set; }
    public int CategoryId { get; set; }
};