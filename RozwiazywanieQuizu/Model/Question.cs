using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozwiazywanieQuizu.Model
{
    public class Question
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
        }
    }
}
