// WishlistController.cs
[ApiController]
[Route("api/wishlist")]
public class WishlistController : ControllerBase
{
    // Inject your data context here
    private readonly DataContext _context;

    public WishlistController(DataContext context)
    {
        _context = context;
    }

    // Create a new wishlist for a user
    [HttpPost]
    public IActionResult CreateWishlist([FromBody] Wishlist wishlist)
    {
        _context.Wishlists.Add(wishlist);
        _context.SaveChanges();
        return Ok();
    }

    // Add a book to a user's wishlist
    [HttpPost("{wishlistId}/addBook")]
    public IActionResult AddBookToWishlist(int wishlistId, [FromBody] int bookId)
    {
        var wishlist = _context.Wishlists.Include(w => w.Books).FirstOrDefault(w => w.WishlistId == wishlistId);
        if (wishlist == null)
        {
            return NotFound("Wishlist not found");
        }

        var book = _context.Books.Find(bookId);
        if (book == null)
        {
            return NotFound("Book not found");
        }

        wishlist.Books.Add(book);
        _context.SaveChanges();
        return Ok();
    }

    // Remove a book from a user's wishlist
    [HttpDelete("{wishlistId}/removeBook")]
    public IActionResult RemoveBookFromWishlist(int wishlistId, [FromBody] int bookId)
    {
        var wishlist = _context.Wishlists.Include(w => w.Books).FirstOrDefault(w => w.WishlistId == wishlistId);
        if (wishlist == null)
        {
            return NotFound("Wishlist not found");
        }

        var book = wishlist.Books.FirstOrDefault(b => b.BookId == bookId);
        if (book != null)
        {
            wishlist.Books.Remove(book);
            _context.SaveChanges();
        }

        return Ok();
    }

    // List the books in a user's wishlist
    [HttpGet("{wishlistId}/books")]
    public IActionResult GetWishlistBooks(int wishlistId)
    {
        var wishlist = _context.Wishlists.Include(w => w.Books).FirstOrDefault(w => w.WishlistId == wishlistId);
        if (wishlist == null)
        {
            return NotFound("Wishlist not found");
        }

        return Ok(wishlist.Books);
    }
}
