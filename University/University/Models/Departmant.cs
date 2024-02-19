namespace University.Models
{
    public class Departmant
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; } = null!;

        public List<Group> Groups { get; set; } = new();
    }
}
