using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Diagnostics;

namespace RozwiazywanieQuizu.Model
{
    public class QuizModel
    {
        public DataAccess DataAccess { get; set; }
        public Quiz Quiz { get; set; }

        public int counter = 0;
        public int points = 0;

        public int numQuestions;

        public QuizModel()
        {
            Question question = new Question();
            DataAccess = new DataAccess();
            DataAccess.ReadData();
            Quiz = DataAccess.Quiz;
            numQuestions = Quiz.Questions.Count;
            
        }

        public Question nextQuestion()
        {
            try
            {
                int tempCounter = counter;
                counter++;
                return Quiz.Questions[tempCounter];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public void checkAnswers(bool[] odpowiedzi)
        {
            int point = 1;
            bool[] correctAnsewers = { Quiz.Questions[counter - 1].Answers[0].IsCorrect, 
                Quiz.Questions[counter - 1].Answers[1].IsCorrect, 
                Quiz.Questions[counter - 1].Answers[2].IsCorrect, 
                Quiz.Questions[counter - 1].Answers[3].IsCorrect };


            for (int i = 0; i < odpowiedzi.Length; i++)
            {
                if (correctAnsewers[i] != odpowiedzi[i])
                {
                    point = 0;
                }
            }

            points+=point;
        }

        public int[] givePoints()
        {
            int[] grade = { points, numQuestions };
            return grade;
        }
        
    }
}
