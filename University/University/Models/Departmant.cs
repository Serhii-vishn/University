namespace University.Models
{
    public class Departmant//TODO add ModelConfig
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; } = null!;
    }
}
