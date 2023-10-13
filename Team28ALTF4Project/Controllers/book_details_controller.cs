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

        [HttpPost("CheckifBook_DetailsTableExists")]
        public JsonResult Post()
        {

            

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                string tableName = "book_details";
                string query = $"SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{mycon.Database}' AND table_name = '{tableName}'";


                mycon.Open();
                try {
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        int count = Convert.ToInt32(myCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            return new JsonResult("Table Exists");
                        }

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                } 
                catch {
                    string newQuery = $"CREATE TABLE '{tableName}' (    " +
                        $"ISBN int NOT NULL," +
                        $"BookName varchar(200)," +
                        $"BookDescription varchar(1000)," +
                        $"Price decimal(4,2)," +
                        $"Author varchar(150)," +
                        $"Genre varchar(100)," +
                        $"Publisher varchar(150)," +
                        $"YearPublished int(255)," +
                        $"CopiesSold int(255)," +
                        $"PRIMARY KEY (ISBN));'";

                    using (MySqlCommand myNewCommand = new MySqlCommand(newQuery, mycon))
                    {
                        myNewCommand.ExecuteNonQuery();

                        myReader = myNewCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();

                        return new JsonResult("Table Doesn't Exist, it has been created.");
                    }
                }

                return new JsonResult("Connection was never established");
            }
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
