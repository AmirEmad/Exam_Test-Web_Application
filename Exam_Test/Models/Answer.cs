namespace Exam_Test.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Answer1 { get; set; } = null!;

        public virtual ICollection<Questions> Questions { get; set; }
    }
}
