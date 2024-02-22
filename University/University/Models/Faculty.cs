namespace University.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public List<Curriculum> Curriculums { get; set; } = new();
    }
}
