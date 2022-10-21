namespace Exam_Test.Models
{
    public class Questions
    {
        public int Id { get; set; }
        public string Question1 { get; set; } = null!;
        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; } = null!;

        public virtual ICollection<ExamModel> Exams { get; set; }
    }
}
