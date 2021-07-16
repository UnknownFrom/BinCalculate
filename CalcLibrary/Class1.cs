using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CalcLibrary
{
    /// <summary>
    /// Класс <c>Calc<c> 
    /// Содержит методы для выполнения операций над числами в двоичной и десятичной системе исчисления
    /// </summary>
    static public class Calc
    {
        static Assembly assembly = Assembly.LoadFrom(@"BinCalc.dll");
        static Type[] types = assembly.GetTypes();
        static MethodInfo BinOperation = types[0].GetMethods()[0];
        /// <summary>
        /// Выполнение математической операции
        /// </summary>
        /// <param name="s">Выражение (string)</param>
        /// <param name="bin">В какой системе исчисления выполняются операции (bool)</param>
        /// <returns>Результат вычисления</returns>
        public static string DoOperation(string s,bool bin)
        {
            
            string[] operands = GetOperands(s);//получить операнды
            if(operands[0] == "0" && operands[1]=="0"||operands[1]=="")
            {
                return "error";
            }
            string op = GetOperation(s);//получить операцию
            if (bin)
            {
                return DoBinOperation(s);
            }
            else
            {
                return DoubleOperation[op](double.Parse(operands[0]), double.Parse(operands[1])).ToString();//вычислить и получить строку;
            }
        }

        /// <summary>
        /// Выполнение операции в двоичной системе исчисления
        /// </summary>
        /// <example>
        /// <code>
        /// string s = DoBinOperation("23^4");// s = "19"
        /// </code>
        /// </example>
        /// <param name="s">Выражение (string)</param>
        /// <returns>Результат вычисления</returns>
        public static string DoBinOperation(string s)
        {
            object obj = Activator.CreateInstance(types[0]);
            string[] num = s.Split(new char[] { '|', '&', '^' }, StringSplitOptions.RemoveEmptyEntries);
            if (num[0].IndexOf(',') == -1 && num[1].IndexOf(',') == -1)
            {
                string res = (string)BinOperation.Invoke(obj, new object[] { s });
                return res;
            }
            else
            {
                return "error";
            }

        }

        /// <summary>
        /// Нахождение операндов
        /// </summary>
        /// <example>
        /// <code>
        /// string[] oper = GetOperands("10+3"); //res[0]=10, res[1]=3
        /// </code>
        /// </example>
        /// <param name="s">Выражение (string)</param>
        /// <returns>Список операндов</returns>
        public static string[] GetOperands(string s)
        {
            string pattern = @"[^\d,\.]";
            Regex rgx = new Regex(pattern);
            MatchCollection mc = rgx.Matches(s,1);
            List<string> lm = new List<string>(); /* все знаки, кроме чисел и точки/запятой */
            foreach (Match m in mc)
            {
                lm.Add(m.Value);
            }
            string[] res = new string[2];
            if (lm.Count == 1)
            {
                res[0] = s.Substring(0, s.IndexOf(lm[0], 1));
                res[1] = s.Substring(s.IndexOf(lm[0], 1) + 1);
            }
            else
            {
                res[0] = "0";
                res[1] = "0";
            }
            return res;
        }
        public delegate T OperationDelegate<T>(T x, T y);
        /// <summary>
        /// Набор операций
        /// </summary>
        public static Dictionary<string, OperationDelegate<double>> DoubleOperation = new Dictionary<string, OperationDelegate<double>>
        {
            { "+", /*delegate(double x, double y){ return x + y; }*/(x,y)=>x+y },
            { "-", /*delegate(double x, double y){ return x - y; }*/(x,y)=>x-y },
            { "*", /*delegate(double x, double y){ return x * y; }*/(x,y)=>x*y },
            { "/", /*delegate(double x, double y){ return x / y; }*/(x,y)=>x/y },
            { "%", /*delegate(double x, double y){ return x / y; }*/(x,y)=>x%y },
            { "^", /*delegate(double x, double y){ return x / y; }*/(x,y)=>Math.Pow(x,y)},
            { ":", /*delegate(double x, double y){ return x / y; }*/(x,y)=>(int)(x/y)},
        };

        /// <summary>
        /// Определение операции в выражении
        /// </summary>
        /// <example>
        /// <code>
        /// string res = GetOperation("10+3");
        /// Console.WriteLine(res); //"+"
        /// </code>
        /// </example>
        /// <param name="s">Выражение (string)</param>
        /// <returns>Операция</returns>
        public static string GetOperation(string s)
        {
            string pattern = @"[^\d,\.]";
            Regex rgx = new Regex(pattern);
            MatchCollection mc = rgx.Matches(s, 1);
            return mc[0].Value;
        }
    }
}
