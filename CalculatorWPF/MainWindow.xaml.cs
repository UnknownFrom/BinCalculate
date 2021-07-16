using System;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;
using CalcLibrary;

namespace CalculatorWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SolidColorBrush errorBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 44));
        SolidColorBrush rightBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        static bool bin,operation = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку ввода числа или операции (+, -, *, /, ^, %) в текстовое поле
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Background = rightBrush;
            
            string text = ((Button)sender).Content.ToString();
            if (textBlock.Text == "0")
            {
                if (Char.IsDigit(text.ToCharArray()[0]))
                {
                    textBlock.Text = text;
                }
                else if(!operation)
                {
                    operation = true;
                    bin = false;
                    textBlock.Text += text;
                }
            }
            else
            {
                if (!Char.IsDigit(text.ToCharArray()[0]) && Char.IsDigit(textBlock.Text.Substring(textBlock.Text.Length - 1).ToCharArray()[0])&&!operation)
                {
                    textBlock.Text += text;
                    operation = true;
                    bin = false;
                }
                if (Char.IsDigit(text.ToCharArray()[0]) /*&& Char.IsDigit(textBlock.Text.Substring(textBlock.Text.Length - 1).ToCharArray()[0])*/)
                {
                    textBlock.Text += text;
                }
            }
            
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку очистки текстового поля
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickClear(object sender, RoutedEventArgs e)
        {
            operation = false;
            textBlock.Background = rightBrush;
            textBlock.Text = "0";
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку решение введённого выражения
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickSolve(object sender, RoutedEventArgs e)
        {
            string res = Calc.DoOperation(textBlock.Text,bin);
            if(res=="error")
            {
                textBlock.Background = errorBrush;
            }
            else
            {
                textBlock.Background = rightBrush;
                textBlock.Text = res;
            }
            operation = false;
            //textBlock.Text=Calc.DoOperation(textBlock.Text);
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку смены знака числа
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickChangeSign(object sender, RoutedEventArgs e)
        {
            if (!operation)
            {
                textBlock.Background = rightBrush;
                textBlock.Text = (double.Parse(textBlock.Text) * (-1)).ToString();
            }
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку вычисления деления 1 на введённое число
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickDivOnX(object sender, RoutedEventArgs e)
        {
            if (!operation)
            {
                bin = false;
                string text = double.Parse(textBlock.Text).ToString();
                if (text != "0")
                {
                    textBlock.Background = rightBrush;
                    textBlock.Text = (1.00 / double.Parse(textBlock.Text)).ToString();
                }
                else
                {
                    textBlock.Background = errorBrush;
                }
                operation = false;
            }
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку вычисления факториала числа
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickFactorial(object sender, RoutedEventArgs e)
        {
            if (!operation)
            {
                int num;
                bin = false;
                if (int.TryParse(textBlock.Text, out num))
                {
                    //int num = int.Parse(textBlock.Text);
                    if (num >= 0)
                    {
                        int result = 1;
                        for (int i = 1; i <= num; i++)
                        {
                            result *= i;
                        }
                        textBlock.Text = result.ToString();
                        textBlock.Background = rightBrush;
                    }
                    else
                    {
                        textBlock.Background = errorBrush;
                    }
                }
                operation = false;
            }
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку вычисления квадратного корня числа
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickSqrt(object sender, RoutedEventArgs e)
        {
            if (!operation)
            {
                bin = false;
                string text = double.Parse(textBlock.Text).ToString();
                if (double.Parse(text) >= 0)
                {
                    textBlock.Text = (Math.Sqrt(double.Parse(textBlock.Text))).ToString();
                    textBlock.Background = rightBrush;
                }
                else
                {
                    textBlock.Background = errorBrush;
                }
                operation = false;
            }
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку возведения во вторую степень
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickPow2(object sender, RoutedEventArgs e)
        {
            if (!operation)
            {
                bin = false;
                textBlock.Text += "^2";
                textBlock.Text = Calc.DoOperation(textBlock.Text, bin);
                textBlock.Background = rightBrush;
                operation = false;
            }
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку вывода числа Эйлера "е"
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickEXP(object sender, RoutedEventArgs e)
        {
            bin = false;
            textBlock.Text = Math.Exp(1).ToString();
            textBlock.Background = rightBrush;
            operation = false;
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку вычисления е^x
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickEXPX(object sender, RoutedEventArgs e)
        {
            if (!operation)
            {
                bin = false;
                textBlock.Text = Math.Exp(double.Parse(textBlock.Text)).ToString();
                textBlock.Background = rightBrush;
                operation = false;
            }
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку вывода числа ПИ
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickPI(object sender, RoutedEventArgs e)
        {
            bin = false;
            textBlock.Text = Math.PI.ToString();
            textBlock.Background = rightBrush;
            operation = false;
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку вычисления тангенса от введённого числа
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickTAN(object sender, RoutedEventArgs e)
        {
            if (!operation)
            {
                bin = false;
                textBlock.Text = Math.Tan(double.Parse(textBlock.Text)).ToString();
                textBlock.Background = rightBrush;
                operation = false;
            }
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку вычисления синуса от введённого числа
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickSIN(object sender, RoutedEventArgs e)
        {
            if (!operation)
            {
                bin = false;
                textBlock.Text = Math.Sin(double.Parse(textBlock.Text)).ToString();
                textBlock.Background = rightBrush;
                operation = false;
            }
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку вычисления косинуса от введённого числа
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickCOS(object sender, RoutedEventArgs e)
        {
            if (!operation)
            {
                bin = false;
                textBlock.Text = Math.Cos(double.Parse(textBlock.Text)).ToString();
                textBlock.Background = rightBrush;
                operation = false;
            }
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку ввода операции |
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickOR(object sender, RoutedEventArgs e)
        {
            if (!operation)
            {
                bin = true;
                textBlock.Text += "|";
                operation = true;
            }
            //string res = Calc.DoOperation(textBlock.Text,bin);
            //if (res == "error")
            //{
            //    textBlock.Background = errorBrush;
            //}
            //else
            //{
            //    textBlock.Background = rightBrush;
            //    textBlock.Text = res;
            //}
        }
        /// <summary>
        /// Ввод операции &
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickAND(object sender, RoutedEventArgs e)
        {
            if (!operation)
            {
                bin = true;
                textBlock.Text += "&";
                operation = true;
            }
            //string res = Calc.DoOperation(textBlock.Text, bin);
            //if (res == "error")
            //{
            //    textBlock.Background = errorBrush;
            //}
            //else
            //{
            //    textBlock.Background = rightBrush;
            //    textBlock.Text = res;
            //}
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку операции XOR
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickXOR(object sender, RoutedEventArgs e)
        {
            if (!operation)
            {
                bin = true;
                textBlock.Text += "^";
                operation = true;
            }
        }

        int wholePart;
        int mantisa;
        string binWholePart;
        string binMantisa;
        string binOrder;
        int countZero;
        int order;
        bool sign;
        /// <summary>
        /// Обработчик события нажатия на кнопку вычисления ввёденого числа в двоичной системе исчисления
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickBIN(object sender, RoutedEventArgs e)
        {
            if (!operation)
            {
                string number = textBlock.Text;
                char[] sep = new char[3] { ',', ' ', '.' };
                string[] parts = number.Split(sep);
                wholePart = int.Parse(parts[0]);
                //countZero = parts[1].Length;
                //mantisa = int.Parse(parts[1]);
                //countZero = countZero - mantisa.ToString().Length;
                if (wholePart > 0)
                {
                    sign = true;/*+*/
                }
                else
                {
                    sign = false;/*-*/
                    wholePart *= -1;
                }
                InBin();
                textBlock.Text = binToString();
                operation = false;
            }
        }
        /// <summary>
        /// Перевод в двоичную систему исчисления
        /// </summary>
        /// <example>
        /// <code>
        /// int wholePart = 3;
        /// int binWholePart = 0;
        /// InBin();
        /// Console.WriteLine(binWholePart) // binWholePart = 11
        /// </code>
        /// </example>
        private void InBin()
        {
            /*string */
            binWholePart = "";
            int copyWhole = Math.Abs(wholePart);
            if (copyWhole == 0)
            {
                binWholePart = "0";
            }
            while (copyWhole != 0)
            {
                if (copyWhole % 2 == 1)
                {
                    binWholePart = "1" + binWholePart;
                    copyWhole = copyWhole / 2;
                }
                else
                {
                    binWholePart = "0" + binWholePart;
                    copyWhole = copyWhole / 2;
                }
            }
            //int copyMantisa = Math.Abs(mantisa);
            /*string */
            //binMantisa = "";
            //int sizeMantisa = copyMantisa.ToString().Length + countZero;
            //int count = 0;
            //while (copyMantisa != 0 && count != 10)
            //{
            //    copyMantisa *= 2;
            //    if (copyMantisa.ToString().Length > sizeMantisa)
            //    {
            //        binMantisa = binMantisa + "1";
            //        copyMantisa = (int)(copyMantisa % Math.Pow(10, copyMantisa.ToString().Length - 1));
            //        count++;
            //    }
            //    else
            //    {
            //        binMantisa = binMantisa + "0";
            //        count++;
            //    }
            //}

        }
        /// <summary>
        /// Возвращение двоичного числа в виде строки
        /// </summary>
        /// <returns>Двоичное число</returns>
        public string binToString() => (sign == false ? "1 " : "") + binWholePart;//+ "," + binMantisa + (order != 0 ? "*10^" + (order < 0 ? "-" : "") + binOrder : "");

        /// <summary>
        /// Обработчик события нажатия на кнопку вычисления ввёденого числа в десятичной системе исчисления
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickDEC(object sender, RoutedEventArgs e)
        {
            if (!textBlock.Text.Contains("2") && !textBlock.Text.Contains("3") && !textBlock.Text.Contains("4") && !textBlock.Text.Contains("5") && !textBlock.Text.Contains("6") && !textBlock.Text.Contains("7") && !textBlock.Text.Contains("8") && !textBlock.Text.Contains("9"))
            {
                if (!operation)
                {
                    string number = textBlock.Text;
                    int result = 0;
                    for (int i = textBlock.Text.Length - 1, k = 0; i >= 0; i--, k++)
                    {
                        result += int.Parse(number[i].ToString()) * (int)Math.Pow(2, k);
                    }
                    textBlock.Text = result.ToString();
                }
            }
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку операции для нахождения целой части от деления
        /// </summary>
        /// <param name="sender">тип object</param>
        /// <param name="e">тип RoutedEventArgs</param>
        private void Button_ClickDIV(object sender, RoutedEventArgs e)
        {
            if (!operation)
            {
                textBlock.Text += ":";
                operation = true;
            }
        }

        //public string decimalToString() => (sign == false ? "-" : "") + wholePart + "," + mantisa + (order != 0 ? "*10^" + order : "");
    }
}
