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
    }
}
