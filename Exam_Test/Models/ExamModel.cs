using Newtonsoft.Json;

namespace Exam_Test.Models
{
    public class ExamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Questions>? Questions { get; set; }

    }
}
