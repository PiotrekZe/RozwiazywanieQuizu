using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace RozwiazywanieQuizu.Model
{
    public class DataAccess
    {
        public Quiz Quiz { get; set; }
        
        static SQLiteConnection conn = new SQLiteConnection(@"Data Source=E:\University\Semestr 4\Programowanie\QuizBaza\Quiz.db;Version=3");

        public void ReadData(SQLiteConnection conn)
        {
            
            Quiz = new Quiz();

            List<Question> questionList = new List<Question>();
            List<Answer> answerList = new List<Answer>();

            SQLiteDataReader reader;
            SQLiteCommand command;

            command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Questions";
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Question question = new Question();
                long id = (long)reader["id"];
                string que = (string)reader["question"];
                question.Id = id;
                question.Text = que;
                Quiz.Questions.Add(question);
            }

            command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Answers";
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Answer answer = new Answer();
                long id = (long)reader["id"];
                string ans = (string)reader["answer"];
                bool corr = Convert.ToBoolean((long)reader["isCorrect"]);
                long questionId = (long)reader["questionId"];
                answer.Id = id;
                answer.Text = ans;
                answer.IsCorrect = corr;
                answer.QuestionId = questionId;
                answerList.Add(answer);
            }

            foreach (Answer temp_answer in answerList)
            {
                long tempId = temp_answer.QuestionId;
                Quiz.Questions[(int)tempId - 1].Answers.Add(temp_answer);
            }

        }

        public void ReadData()
        {
            try
            {
                conn.Open();
                ReadData(conn);
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
