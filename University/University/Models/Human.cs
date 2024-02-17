namespace University.Models
{
    public class Human
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
    }
}
