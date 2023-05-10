using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozwiazywanieQuizu.Model
{
    public class Answer
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public long QuestionId { get; set; }
    }
}
