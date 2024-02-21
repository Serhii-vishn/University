namespace University.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = null!;

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public List<Student> Students { get; set; } = new();
    }
}
