namespace BooksService.Models;

public class Book
{
    public int Id { get; set; }
    public string Uid { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public float Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}