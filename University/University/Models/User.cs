namespace University.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;

        public int HumanId { get; set; } 
        public Human Human { get; set; } = null!;
    }
}
