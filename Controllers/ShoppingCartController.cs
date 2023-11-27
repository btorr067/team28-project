namespace MyBookStoreAPI.Controllers;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyBookStoreAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    // This is some mock data store for available books will implement Brandons Soon
    private static List<Books> Books = new List<Books>
        {
            new Books { Id = 1, Title = "Sample Book 1", Price = 20.0 },
            new Books { Id = 2, Title = "Sample Book 2", Price = 30.0 },
            
        };

    
    private static List<ShoppingCartItem> CartItems = new List<ShoppingCartItem>();

    [HttpGet("subtotal/{userId}")]
    public ActionResult<double> GetSubtotal(int userId)
    {
        double subtotal = CartItems.Where(item => item.UserId == userId)
                                   .Sum(item => item.Book.Price * item.Quantity);

        return Ok(subtotal);
    }
    [HttpGet("list/{userId}")]
    public ActionResult<IEnumerable<Books>> GetCartItems(int userId)
    {
        var cartItems = CartItems.Where(ci => ci.UserId == userId).ToList();

        if (!cartItems.Any())
        {
            return NotFound("No items found in the shopping cart for the user.");
        }

        var books = cartItems.Select(ci => ci.Book).ToList();

        return Ok(books);
    }
    [HttpPost("add-to-cart")] 
    public ActionResult AddBookToCart(int userId, int bookId)
    {
        var book = Books.FirstOrDefault(b => b.Id == bookId); 

        if (book == null)
        {
            return NotFound("Book not found.");
        }

        var cartItem = CartItems.FirstOrDefault(ci => ci.UserId == userId && ci.Book.Id == bookId);

        if (cartItem != null)
        {
            // If the book is already in the cart just increase the quantity 
            cartItem.Quantity++;
        }
        else
        {
            // If the book is not in the cart add it 
            CartItems.Add(new ShoppingCartItem { UserId = userId, Book = book, Quantity = 1 });
        }

        return Ok("Book added to cart successfully.");
    }
    [HttpGet("books/{userId}")]
    public ActionResult<List<Books>> GetBooksInCart(int userId)
    {
        var booksInCart = CartItems.Where(item => item.UserId == userId)
                                   .Select(item => item.Book)
                                   .ToList();

        if (!booksInCart.Any())
        {
            return NotFound("No books found in the cart for this user.");
        }

        return Ok(booksInCart);// Depending on what UserID is inputted it will return the list of books in the Users Cart 
        // Still missing to add if they have multiple of the same book
    }
    [HttpDelete("{userId}/{bookId}")]
    public IActionResult RemoveBookFromCart(int userId, int bookId)
    {
        var cartItem = CartItems.FirstOrDefault(ci => ci.UserId == userId && ci.Book.Id == bookId);

        if (cartItem == null)
        {
            return NotFound("Book not found in the shopping cart.");
        }

        CartItems.Remove(cartItem);

        return NoContent(); // Standard response for a successful DELETE request that returns no body.
    }

}


