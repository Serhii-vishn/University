namespace University.Models
{
    public class Groups
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = null!;

        public int CuratorId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        public int CurriculumId { get; set; }
        public Curriculum Curriculum { get; set; } = null!;

        public List<Student> Students { get; set; } = new();
    }
}
