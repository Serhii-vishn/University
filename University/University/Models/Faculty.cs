namespace University.Models
{
    public class Faculty//TODO add ModelConfig
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public List<Departmant> Departmants { get; set; } = new();
    }
}
