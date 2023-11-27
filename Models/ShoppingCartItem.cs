namespace MyBookStoreAPI.Models
{
    public class ShoppingCartItem
    {
        public int UserId { get; set; }
        public Books Book { get; set; }
        public int Quantity { get; set; }
    }
}
