namespace Team28BookDetails.Models
{
    public class Book_Details
    {
        public string ISBN { get; set; }

        public string BookName { get; set; }

        public string BookDescription { get; set; }

        public decimal Price { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public string Publisher { get; set; }

        public int YearPublished { get; set; }

        public float CopiesSold { get; set; }

    }
}
