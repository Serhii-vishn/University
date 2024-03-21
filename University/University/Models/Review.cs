namespace University.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string FeedBack { get; set; } = null!;
        public DateTime PostDate { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
    }
}
