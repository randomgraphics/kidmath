using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MathGen g = new MathGen();

        SimpleAdd[] questions;

        public MainWindow()
        {
            InitializeComponent();

            questions = new SimpleAdd[]
            {
                this.q0,
                this.q1,
                this.q2,
                this.q3,
                this.q4,
                this.q5,
                this.q6,
                this.q7,
                this.q8,
                this.q9,
            };
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == this.addButton8)
            {
                g.Gen20(this.questions.Length);
            }
            else if (sender == this.addButton9)
            {
                g.GenDec(this.questions.Length, 10, 99);
            }

            for (int i = 0; i < this.questions.Length; ++i)
            {
                var q = g.questions[i];
                this.questions[i].A = q.a;
                this.questions[i].B = q.b;
                this.questions[i].Op = q.op;
                this.questions[i].C = "";
            }
        }
    }

    public class MathGen
    {
        // a op b = c
        public struct Question
        {
            public string op;
            public int a;
            public int b;
        };

        public List<Question> questions;

#if false
        public string GenAdd2(int maxsum)
        {
            Func<Random, string> expression = (r) =>
            {
                int a = r.Next(1, maxsum);
                int b = r.Next(1, maxsum - a + 1);
                return itoa(a) + " + " + itoa(b, true) + " =";
            };
            string text = string.Empty;
            for (int i = 0; i < 10; ++i)
            {
                string a = expression(this.rand);
                string b = expression(this.rand);
                string c = expression(this.rand);
                string line = string.Format("{0,-20} {1,-20} {2}\r\n\r\n", a, b, c);
                text += line;
            }
            return text;
        }

        public string GenAdd2(int min, int max, int minus_percent = 0)
        {
            Func<Random, string> expression = (r) =>
            {
                int a = r.Next(min, max + 1);
                int b = r.Next(min, max + 1);
                string op = Percent(minus_percent) ? " - " : " + ";
                return itoa(a) + op + itoa(b, true) + " =";
            };
            string text = string.Empty;
            for (int i = 0; i < 10; ++i)
            {
                string a = expression(this.rand);
                string b = expression(this.rand);
                string c = expression(this.rand);
                string line = string.Format("{0,-20} {1,-20} {2}\r\n\r\n", a, b, c);
                text += line;
            }
            return text;
        }

        public string GenAdd3()
        {
            Func<Random, string> expression = (r) =>
            {
                int a = r.Next(-20, 21);
                int b = r.Next(-20, 21);
                int c = r.Next(-20, 21);
                string op1 = Percent(30) ? " + " : " - ";
                string op2 = Percent(30) ? " + " : " - ";
                if (Percent(50))
                {
                    return itoa(a) + op1 + itoa(b, true) + op2 + itoa(c, true) + " =";
                }
                else
                {
                    return itoa(a) + op1 + "(" + itoa(b) + op2 + itoa(c, true) + ") =";
                }
            };
            string text = string.Empty;
            for (int i = 0; i < 4; ++i)
            {
                string a = expression(this.rand);
                string b = expression(this.rand);
                string c = expression(this.rand);
                string line = string.Format("{0,-30} {1,-30} {2}\r\n\r\n", a, b, c);
                text += line;
            }
            return text;
        }
#endif

        /// <summary>
        /// 20以内加减法
        /// </summary>
        /// <param name="count"></param>
        public void Gen20(int count)
        {
            questions = new List<Question>(count);
            for (int i = 0; i < count; ++i)
            {
                var q = new Question();

                if (this.rand.Next(0, 100) > 50)
                {
                    // add
                    q.op = "+";
                    q.a = this.rand.Next(10, 19);
                    q.b = this.rand.Next(10, 19);
                }
                else
                {
                    // dec
                    q.op = "-";
                    q.b = this.rand.Next(3, 12);
                    q.a = this.rand.Next(q.b, 19);
                }

                questions.Add(q);
            }
        }

        public void GenDec(int count, int min, int max)
        {
            questions = new List<Question>(count);
            for (int i = 0; i < count; ++i)
            {
                var q = new Question();
                q.op = "-";
                q.a = this.rand.Next(min, max);
                q.b = this.rand.Next(min, q.a);
                questions.Add(q);
            }
        }

        Random rand = new Random();
    }
}
