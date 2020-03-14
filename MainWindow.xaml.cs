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

        public MainWindow()
        {
            InitializeComponent();
            this.textBox1.SizeChanged += (s, e) => { UpdateFontSize(); };
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == this.addButton0)
            {
                this.textBox1.Text = g.GetText(true);
            }
#if false
            else if (sender == this.addButton1 ||
                sender == this.addButton2 ||
                sender == this.addButton3 ||
                sender == this.addButton4 ||
                sender == this.addButton5)
            {
                this.textBox1.Text = g.GenAdd2(1, int.Parse(((Button)sender).Tag.ToString()));
            }
            else if (sender == this.addButton6)
            {
                this.textBox1.Text = g.GenAdd2(10);
            }
#endif
            else if (sender == this.addButton8)
            {
                g.GenDec(10, 99);
                this.textBox1.Text = g.GetText(false);
            }
            else if (sender == this.addButton9)
            {
                g.GenDec(100, 999);
                this.textBox1.Text = g.GetText(false);
            }

            this.textBox1.Background = g.RandomColor(1, 127);
            this.textBox1.Foreground = g.RandomColor(128, 255);

            UpdateFontSize();
        }

        void UpdateFontSize()
        {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text)) return;

            double targetWidth = this.textBox1.ActualWidth;
            double minWidth = targetWidth * .95;

            double targetHeight = this.textBox1.ActualHeight;
            double minHeight = targetHeight * .95;

            double size = this.textBox1.FontSize;
            double step = size;

            for(; ; )
            {
                var f = new FormattedText(
                    this.textBox1.Text,
                    CultureInfo.CurrentCulture,
                    this.textBox1.FlowDirection,
                    new Typeface(this.textBox1.FontFamily, this.textBox1.FontStyle, this.textBox1.FontWeight, this.textBox1.FontStretch),
                    size,
                    this.textBox1.Foreground);

                if (f.Width < minWidth && f.Height < minHeight)
                {
                    if (step < 0)
                    {
                        step = -step / 2;
                    }
                }
                else if (f.Width > targetWidth || f.Height > targetHeight)
                {
                    if (step > 0)
                    {
                        step = -step / 2;
                    }
                }
                else
                {
                    break;
                }

                size += step;

                if (size < 1) size = 1;
            }

            this.textBox1.FontSize = size;
        }
    }

    public class MathGen
    {
        enum Operator
        {
            ADD,
            DEC,
        };

        static string Op2Str(Operator op)
        {
            switch(op)
            {
                case Operator.ADD: return " + ";
                case Operator.DEC: return " - ";
            }
            return "";
        }

        class Question
        {
            public Operator op;
            public int arg1, arg2;
            public int answer;
            public string ToString(bool showAnswer)
            {
                var s = itoa(arg1) + Op2Str(op) + itoa(arg2, true) + " =";
                if (showAnswer) s += " " + itoa(answer);
                return s;
            }
        };

        List<Question> questions = new List<Question>(30);
        Random rand = new Random();

        public Brush RandomColor(int min, int max)
        {
            return new SolidColorBrush(Color.FromRgb(
                (byte)rand.Next(min, max),
                (byte)rand.Next(min, max),
                (byte)rand.Next(min, max)));
        }

        bool Percent(int percent)
        {
            return this.rand.Next(0, 100) < percent;
        }

        static string itoa(int i, bool parentheses = false)
        {
            if (i < 0 && parentheses)
                return string.Format("({0})", i);
            else
                return string.Format("{0}", i);
        }

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
        public void GenDec(int min, int max)
        {
            Func<Random, string> expression = (r) =>
            {
                int a = r.Next(min, max);
                int b = r.Next(min, a);
                return itoa(a) + "-" + itoa(b, true) + " =";
            };

            questions = new List<Question>();
            for (int i = 0; i < 30; ++i)
            {
                var q = new Question();
                q.arg1 = this.rand.Next(min, max);
                q.arg2 = this.rand.Next(min, q.arg1);
                q.op = Operator.DEC;
                q.answer = q.arg1 - q.arg2;
                questions.Add(q);
            }
        }

        public string GetText(bool showAnswer)
        {
            string text = string.Empty;
            for (int i = 0; i < 10; ++i)
            {
                string a = questions[i * 3 + 0].ToString(showAnswer);
                string b = questions[i * 3 + 1].ToString(showAnswer);
                string c = questions[i * 3 + 2].ToString(showAnswer);
                string line = string.Format("{0,-20} {1,-20} {2}\r\n\r\n", a, b, c);
                text += line;
            }
            return text;
        }
    }
}
