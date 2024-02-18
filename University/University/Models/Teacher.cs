using Microsoft.Windows.Themes;

namespace University.Models 
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Position { get; set; } = null!;
        public string AcademicDegreee { get; set; } = null!;

        public int HumanId { get; set; }
        public Human Human { get; set; } = null!;
    }
}
