using System;
using System.Reflection;

namespace BInCalcTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly SampleAssembly = Assembly.LoadFrom("BinCalc.dll");
            Type[] types = SampleAssembly.GetTypes();
            Console.WriteLine("Типы библиотеки: ");
            foreach (Type type in types)
            {
                Console.WriteLine(type.Name + "\t" + type.FullName);
            }
            FieldInfo[] ts = types[0].GetFields();
            Console.WriteLine("\nПоля: ");
            foreach (FieldInfo type in ts)
            {
                Console.WriteLine(type.Name + "\t" + type.FieldType);
            }
            MethodInfo[] ms = types[0].GetMethods();
            Console.WriteLine("\nМетоды: ");
            int i = 1;
            foreach (MethodInfo type in ms)
            {
                Console.WriteLine(i + ") " + type.Name + "\t" + type.DeclaringType);
                i++;
            }
            MethodInfo method = types[0].GetMethods()[0];
            ParameterInfo[] par = method.GetParameters();
            Console.WriteLine("\nСигнатура метода: " + method.Name + "\nПараметры: ");
            i = 1;
            foreach (ParameterInfo type in par)
            {
                Console.WriteLine(i + ") " + type.Name + "\t" + type.ParameterType);
                i++;
            }
            Console.WriteLine("\nВозвращаемый тип: " + method.ReturnType);

            object obj = Activator.CreateInstance(types[0]);
            string s = (string)method.Invoke(obj, new object[] { "23|4" });
            Console.WriteLine("\nВызов метода: " + s);
        }
    }
}
