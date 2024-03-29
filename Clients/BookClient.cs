using Grpc.Net.Client;

namespace BooksService.Clients;

public sealed class BookClient
{
    private static readonly Lazy<BookClient> lazy = new(() => new BookClient());

    public static BookClient Instance { get { return lazy.Value; } }
    public GBookService.GBookServiceClient Client { get; }

    private BookClient()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9000");
        var client = new GBookService.GBookServiceClient(channel);
        Client = client;
    }
}