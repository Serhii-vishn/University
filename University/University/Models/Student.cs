namespace University.Models
{
    public class Student
    {
        public int Id { get; set; }


        public int HumanId { get; set; }
        public Human Human { get; set; } = null!;
    }
}
