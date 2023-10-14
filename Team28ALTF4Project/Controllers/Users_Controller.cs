using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using RESTful_Books.Models;
using System.Data;
using Team28BookDetails.Models;

namespace Team28BookDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        List<User> users = new List<User>
        {
            new User("Username1", "Password1"),
            new User("Username2", "Password2"),
            new User("Username3", "Password3")
        };

        // GET: api/User
        [HttpGet]
        public List<User> Get()
        {
            return users;
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public List<User> Post([FromBody] User user)
        {
            users.Add(user);
            return users;
        }

        // PUT: api/User/5
        [HttpPut("{username}")]
        public List<User> Put(string username, [FromBody] User user)
        {
            User userToUpdate = users.Find(u => u.username == username);
            int index = users.IndexOf(userToUpdate);

            users[index].username = user.username;
            users[index].password = user.password;


            return users;
        }

        // DELETE: api/User/5
        [HttpDelete("{username}")]
        public List<User> Delete(string username)
        {
            User user = users.Find(u => u.username == username);
            users.Remove(user);
            return users;
        }
    }
}