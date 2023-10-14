namespace Team28BookDetails.Models

{
    public class User
    {
        public string username { get; set; }

        public string password { get; set; }

        public User()
        {
        }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}