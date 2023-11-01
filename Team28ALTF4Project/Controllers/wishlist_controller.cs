// WishlistController.cs
[ApiController]
[Route("api/wishlist")]
public class WishlistController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public WishlistController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWishlist(Wishlist wishlist)
    {
        _context.Wishlists.Add(wishlist);
        await _context.SaveChangesAsync();
        return Ok(wishlist);
    }

    [HttpGet("{userId}")]
    public IActionResult GetWishlists(string userId)
    {
        var wishlists = _context.Wishlists.Where(w => w.UserId == userId).ToList();
        return Ok(wishlists);
    }

    [HttpPost("{wishlistId}/addBook")]
    public async Task<IActionResult> AddBookToWishlist(int wishlistId, Book book)
    {
        var wishlist = _context.Wishlists.Include(w => w.Books).FirstOrDefault(w => w.Id == wishlistId);
        if (wishlist == null)
        {
            return NotFound();
        }

        wishlist.Books.Add(book);
        await _context.SaveChangesAsync();
        return Ok(wishlist);
    }
}
