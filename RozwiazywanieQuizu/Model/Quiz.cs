using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozwiazywanieQuizu.Model
{
    public class Quiz
    {
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
        

        public Quiz()
        {
            Questions = new List<Question>();
            Answers = new List<Answer>();
        }

    }
}
