using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Diagnostics;


namespace RozwiazywanieQuizu.ViewModel
{
    public class QuizViewModel : INotifyPropertyChanged
    {
        private Stopwatch _stopwatch;
        public Model.QuizModel QuizModel { get; set; }
        public Model.Question Question { get; set; }
        public Model.Answer Answer { get; set; }

        
        private bool canExecute = true;
        public QuizViewModel()
        {
            QuizModel = new Model.QuizModel();

            NextQuestionContent = "Następne pytanie";

            NextQuestionEnableCommand = false;
            QuizEnableCommand = true;

            NextQuestionCommand = new RelayCommand(nextQuestion, param => this.canExecute);
            QuizCommand = new RelayCommand(firstQuestion, param => this.canExecute);

        }
        public event PropertyChangedEventHandler PropertyChanged;

        //Pytanie
        private string currentQuestion;
        public string CurrentQuestion
        {
            get { return currentQuestion; }
            set
            {
                if (value != currentQuestion)
                {
                    currentQuestion = value;
                    OnPropertyChanged("CurrentQuestion");
                }
            }
        }

        //Odpowiedzi
        private string answer1;
        public string Answer1
        {
            get { return answer1; }
            set
            {
                if (value != answer1)
                {
                    answer1 = value;
                    OnPropertyChanged("Answer1");
                }
            }
        }

        private string answer2;
        public string Answer2
        {
            get { return answer2; }
            set
            {
                if (value != answer2)
                {
                    answer2 = value;
                    OnPropertyChanged("Answer2");
                }
            }
        }

        private string answer3;
        public string Answer3
        {
            get { return answer3; }
            set
            {
                if (value != answer3)
                {
                    answer3 = value;
                    OnPropertyChanged("Answer3");
                }
            }
        }

        private string answer4;
        public string Answer4
        {
            get { return answer4; }
            set
            {
                if (value != answer4)
                {
                    answer4 = value;
                    OnPropertyChanged("Answer4");
                }
            }
        }

        //Licznik czasu
        private string elapsedTime;
        public string ElapsedTime
        {
            get { return elapsedTime; }
            set
            {
                elapsedTime = value;
                OnPropertyChanged("ElapsedTime"); //OnPropertyChanged(nameof(ElapsedTime)); nie wiem jaką rożnicę to robi, ale spotkałem się z tym też
            }
        }

        //Punkty
        private string showPoints;
        public string ShowPoints
        {
            get { return showPoints; }
            set
            {
                if (value != showPoints)
                {
                    showPoints = value;
                    OnPropertyChanged("showPoints");
                }
            }
        }

        //Button tekst
        private string nextQuestionContent;
        public string NextQuestionContent
        {
            get { return nextQuestionContent; }
            set
            {
                if (value != nextQuestionContent)
                {
                    nextQuestionContent = value;
                    OnPropertyChanged("nextQuestionContent");
                }
            }
        }

        //Checkboxy odpowiedzi

        private bool isAnswer1Selected;
        public bool IsAnswer1Selected
        {
            get { return isAnswer1Selected; }
            set 
            {
                if (value != isAnswer1Selected)
                {
                    isAnswer1Selected = value;
                    OnPropertyChanged("isAnswer1Selected");
                }
            }
        }

        private bool isAnswer2Selected;
        public bool IsAnswer2Selected
        {
            get { return isAnswer2Selected; }
            set
            {
                if (value != isAnswer2Selected)
                {
                    isAnswer2Selected = value;
                    OnPropertyChanged("isAnswer2Selected");
                }
            }
        }

        private bool isAnswer3Selected;
        public bool IsAnswer3Selected
        {
            get { return isAnswer3Selected; }
            set
            {
                if (value != isAnswer3Selected)
                {
                    isAnswer3Selected = value;
                    OnPropertyChanged("isAnswer3Selected");
                }
            }
        }

        private bool isAnswer4Selected;
        public bool IsAnswer4Selected
        {
            get { return isAnswer4Selected; }
            set
            {
                if (value != isAnswer4Selected)
                {
                    isAnswer4Selected = value;
                    OnPropertyChanged("isAnswer4Selected");
                }
            }
        }

        //Buttony enable
        private bool quizEnableCommand;
        public bool QuizEnableCommand
        {
            get { return quizEnableCommand; }
            set
            {
                if (value != quizEnableCommand)
                {
                    quizEnableCommand = value;
                    OnPropertyChanged("quizEnableCommand");
                }
            }
        }

        private bool nextQuestionEnableCommand;
        public bool NextQuestionEnableCommand
        {
            get { return nextQuestionEnableCommand; }
            set
            {
                if (value != nextQuestionEnableCommand)
                {
                    nextQuestionEnableCommand = value;
                    OnPropertyChanged("nextQuestionEnableCommand");
                }
            }
        }

        //Commands
        private ICommand nextQuestionCommand { get; set; }
        public ICommand NextQuestionCommand
        {
            get
            {
                return nextQuestionCommand;  
            }
            set { nextQuestionCommand = value; }
        }

        private ICommand quizCommand { get; set; }
        public ICommand QuizCommand
        {
            get
            {
                return quizCommand;
            }
            set { quizCommand = value; }
        }
        
        public void displayQueston() // wyświetlamy pytanie i odpowiedzi
        {
            Question = new Model.Question();
            Question = QuizModel.nextQuestion();

            //tu nadajemy nowe wartości
            if (Question != null)
            {
                CurrentQuestion = Question.Text;
                Answer1 = Question.Answers[0].Text;
                Answer2 = Question.Answers[1].Text;
                Answer3 = Question.Answers[2].Text;
                Answer4 = Question.Answers[3].Text;
            }
            else //null, bo koniec pytań
            {
                StopTimer();
                var points = QuizModel.givePoints();
                NextQuestionEnableCommand = false;
                ShowPoints = "Zdobyte punkty: " + points[0].ToString() + "/" + points[1].ToString();
            }
        }


        public void nextQuestion(object obj) 
        {
            bool[] checkedAnsers = { IsAnswer1Selected, IsAnswer2Selected, IsAnswer3Selected, IsAnswer4Selected }; 
            QuizModel.checkAnswers(checkedAnsers);

            IsAnswer1Selected = false;
            IsAnswer2Selected = false;
            IsAnswer3Selected = false;
            IsAnswer4Selected = false;

            displayQueston();
        }

        public void firstQuestion(object obj) //wczytujemy pierwszy zestaw i włączamy czas (potem trzeba dezaktywować klik w tym)
        {
            StartTimer(); //włączamy czas po kliknięciu

            displayQueston();

            QuizEnableCommand = false;
            NextQuestionEnableCommand = true;
        }
        private void StopTimer()
        {
            _stopwatch.Stop();
        }

        private void StartTimer()
        {
            _stopwatch = Stopwatch.StartNew();
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += UpdateElapsedTime;
            timer.Start();
        }

        private void UpdateElapsedTime(object sender, System.Timers.ElapsedEventArgs e)
        {
            ElapsedTime = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
        }


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
