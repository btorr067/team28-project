using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using Team28BookDetails.Models;

namespace Team28BookDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class book_details_controller : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public book_details_controller(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetBookByISBN")]
        public JsonResult Get(String iSBN)
        {
            string query = @"
                        select ISBN, BookName, BookDescription, Price, Author, Genre, Publisher, YearPublished, CopiesSold
                        from book_details
                        where ISBN = @ISBN;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ISBN", iSBN);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            if (table.Rows.Count == 0)
            {
                return new JsonResult("ISBN Does not Exist!");
            }

            return new JsonResult(table);
        }

        [HttpGet("GetBooksByAuthorName")]
        public JsonResult Get(String authorFirstName, String authorLastName)
        {
            string query = @"
                        select ISBN, BookName, BookDescription, Price, Author, Genre, Publisher, YearPublished, CopiesSold
                        from book_details
                        where Author = @Author;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            var authorName = authorFirstName + " " + authorLastName;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Author", authorName);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            if (table.Rows.Count == 0)
            {
                return new JsonResult("Author doesn't have any books...");
            }

            return new JsonResult(table);
        }

        [HttpPost("PostBookDetails")]
        public JsonResult Post(Book_Details bookDetails)
        {
            string query = @"
                        insert into book_details(ISBN, BookName, BookDescription, Price, Author, Genre, Publisher, YearPublished, CopiesSold)
                        values (@ISBN, @BookName, @BookDescription, @Price, @Author, @Genre, @Publisher, @YearPublished, @CopiesSold);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ISBN", bookDetails.ISBN);
                    myCommand.Parameters.AddWithValue("@BookName", bookDetails.BookName);
                    myCommand.Parameters.AddWithValue("@BookDescription", bookDetails.BookDescription);
                    myCommand.Parameters.AddWithValue("@Price", bookDetails.Price);
                    myCommand.Parameters.AddWithValue("@Author", bookDetails.Author);
                    myCommand.Parameters.AddWithValue("@Genre", bookDetails.Genre);
                    myCommand.Parameters.AddWithValue("@Publisher", bookDetails.Publisher);
                    myCommand.Parameters.AddWithValue("@YearPublished", bookDetails.YearPublished);
                    myCommand.Parameters.AddWithValue("@CopiesSold", bookDetails.CopiesSold);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully!");
        }

        [HttpPost("PostAuthorDetails")]
        public JsonResult Post(Author_Details authorDetails)
        {
            string query = @"
                        INSERT INTO author_details (FirstName, LastName, Biography, Publisher)
                        VALUES (@FirstName, @LastName, @Biography, @Publisher);

            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;

            try
            {
                using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                {
                    mycon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myCommand.Parameters.AddWithValue("@FirstName", authorDetails.FirstName);
                        myCommand.Parameters.AddWithValue("@LastName", authorDetails.LastName);
                        myCommand.Parameters.AddWithValue("@Biography", authorDetails.Biography);
                        myCommand.Parameters.AddWithValue("@Publisher", authorDetails.Publisher);

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                }
            } catch
            {
                return new JsonResult("This failed due to Author First Name already existing in database.");
            }
            

            return new JsonResult("Added Successfully!");
        }
    }
}
