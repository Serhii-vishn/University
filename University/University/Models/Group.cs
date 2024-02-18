namespace University.Models
{
    public class Group//add ModelConfig
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = null!;

        public int DepartmantId { get; set; }
        public Departmant Departmant { get; set; } = null!;

        public List<Student> Students { get; set; } = new();
    }
}
