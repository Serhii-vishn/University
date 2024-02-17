namespace University.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Access { get; set; } = null!;
    }
}
