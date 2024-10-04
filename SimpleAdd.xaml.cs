using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MathGenenrator
{
    /// <summary>
    /// Interaction logic for SimpleAdd.xaml
    /// </summary>
    public partial class SimpleAdd : UserControl, INotifyPropertyChanged
    {
        private int a = 2;
        private string op = "-";
        private int b = 1;
        private string c = "";
        private string d = "";
        private bool showQuotient = false;
        private Brush score; //< yellow for wrong answer, green for write answer

        private void CheckAnswer()
        {
            if (string.IsNullOrEmpty(this.c))
            {
                this.Score = this.Background;
                return;
            }

            string answer;
            string quotient = d.ToString();
            if ("+" == op)
            {
                answer = (a + b).ToString();
            }
            else if ("-" == op)
            {
                answer = (a - b).ToString();
            }
            else if ("*" == op)
            {
                answer = (a * b).ToString();
            }
            else if ("/" == op)
            {
                answer = (a / b).ToString();
                quotient = (a % b).ToString();
            }
            else
            {
                return;
            }
            if ((c != answer) || (showQuotient && d != quotient))
            {
                Score = Brushes.Red;
            }
            else
            {
                Score = Brushes.Green;
            }
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public SimpleAdd()
        {
            this.score = this.Background;
            InitializeComponent();
            ShowQuotient = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool ShowQuotient {
            get { return this.showQuotient; }
            set
            {
                this.showQuotient = value;
                if (value) {
                    this.r.Visibility = Visibility.Visible;
                    this.q.Visibility = Visibility.Visible;
                } else
                {
                    this.r.Visibility = Visibility.Collapsed;
                    this.q.Visibility = Visibility.Collapsed;
                }
            }
        }

        public int A
        {
            get { return a; }
            set
            {
                if (this.a != value)
                {
                    this.a = value;
                    NotifyPropertyChanged("A");
                    CheckAnswer();
                }
            }
        }

        public int B
        {
            get { return b; }
            set
            {
                if (this.b != value)
                {
                    this.b = value;
                    NotifyPropertyChanged("B");
                    CheckAnswer();
                }
            }
        }

        public string Op
        {
            get { return this.op; }
            set
            {
                if (this.op != value)
                {
                    if ("+" == value || "-" == value || "*" == value || "/" == value)
                    {
                        this.op = value;
                        ShowQuotient = "/" == this.op;
                        NotifyPropertyChanged("Op");
                        CheckAnswer();
                    }
                }
            }
        }

        public string C
        {
            get { return c; }
            set
            {
                if (this.c != value)
                {
                    this.c = value;

                    NotifyPropertyChanged("C");
                    CheckAnswer();
                }
            }
        }

        public string D
        {
            get { return d; }
            set
            {
                if (this.d != value)
                {
                    this.d = value;
                    NotifyPropertyChanged("D");
                    CheckAnswer();
                }
            }
        }

        public Brush Score
        {
            get { return this.score; }
            private set
            {
                if (this.score != value)
                {
                    this.score = value;
                    NotifyPropertyChanged("Score");
                }
            }
        }

        private void Answer1Changed(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            this.C = tb.Text;
        }
        private void Answer2Changed(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            this.D = tb.Text;
        }
    }
}
