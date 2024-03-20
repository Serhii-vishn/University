namespace University.Models
{
    public class Curriculum
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; } = null!;

        public List<Teacher> Teachers { get; set; } = new();

        public List<Groups> Groups { get; set; } = new();
    }
}
