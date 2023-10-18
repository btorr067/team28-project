// Wishlist.cs
public class Wishlist
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public List<Book> Books { get; set; }
}
