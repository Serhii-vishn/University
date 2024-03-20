namespace University.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int Course {  get; set; }
        public string Speciality { get; set; } = null!;

        public int HumanId { get; set; }
        public Human Human { get; set; } = null!;

        public int? GroupId { get; set; }
        public Groups? Group { get; set; }
    }
}
