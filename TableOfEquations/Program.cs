using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Test;

namespace MathWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, double> constants = new Dictionary<string, double>()
            {
                {"NDS", 20},
                {"NALOG", 13},
                {"DEPTH", 100},
                {"adsf", 123},
                {"fedw", 4},
                {"vcxzc", 43},
                {"qwee", 654},
                {"qwe", 8521},
                {"yuio", 41581},
                {"poi", 789},
                {"hjk", 100},
                {"iuo", 55}
            };

            int amountOfTest = 20;
            int amountOfExpressions = 10000;
            int amountOfOperands = 20;
            IEnumerable<string>[] data = new IEnumerable<string>[amountOfTest] ;
#if false
            EquationParserShow(amountOfExpressions, amountOfOperands, constants);
#elif false
            for (int i = 0; i < amountOfTest; i++)
            {
                data[i] = TestData.Generate(amountOfExpressions, amountOfOperands, constants.Keys);
            }

            EquationParserTest(data, constants);

            Console.WriteLine();

            mxParserTest(data, constants);
#elif false
            string equ = "=-12.12+10*89+-13*-7";
            EquationParser eq = new EquationParser();
            Console.WriteLine(eq.Calculate(equ));
#elif true
            EquationParserShow(100, 10, constants);
#endif
            Console.ReadKey();

        }

        private static void EquationParserShow(int amount, int length, Dictionary<string, double> constants)
        {
            EquationParser ep = new EquationParser();
            ep.SetConstants(constants);

            IEnumerable<string> data = TestData.Generate(10000, 20, constants.Keys);

            foreach (string exp in data)
            {
                double result = double.Parse(ep.Calculate(exp));
                Console.WriteLine($"Expression: {exp,-150} Result: {result,-150:F2}");
            }
        }

        private static void EquationParserShow(int amount, int length)
        {
            EquationParser ep = new EquationParser(); 

            IEnumerable<string> data = TestData.Generate(10000, 20);

            foreach (string exp in data)
            {
                double result = double.Parse(ep.Calculate(exp));
                Console.WriteLine($"Expression: {exp,-150} Result: {result,-150:F2}");
            }
        }

        private static void EquationParserTest(IEnumerable<string>[] data, Dictionary<string, double> constants)
        {
            EquationParser ep = new EquationParser();
            ep.SetConstants(constants);

            Stopwatch sw = new Stopwatch();

            List<long> times = new List<long>();

            for (int i = 0; i < data.Length; i++)
            {
                sw.Restart();

                foreach (string exp in data[i])
                    ep.Calculate(exp);                

                sw.Stop();

                times.Add(sw.ElapsedMilliseconds);
                Console.WriteLine($"№{i + 1} EquationParser time: {sw.ElapsedMilliseconds} ms");
            }
            Console.WriteLine($"EquationParser time max: {times.Max()} ms, min: {times.Min()}, average: {times.Average()}");
        }

        private static void mxParserTest(IEnumerable<string>[] data, Dictionary<string, double> constants)
        {
            Expression expr = new Expression();
            foreach (var item in constants)
                expr.defineConstant(item.Key, item.Value);

            Stopwatch sw = new Stopwatch();

            List<long> times = new List<long>();

            for (int i = 0; i < data.Length; i++)
            {
                sw.Restart();
                foreach (string exp in data[i])
                {
                    expr.setExpressionString(exp);
                    expr.calculate();
                }
                sw.Stop();

                times.Add(sw.ElapsedMilliseconds);
                Console.WriteLine($"№{i + 1} mxParser time: {sw.ElapsedMilliseconds} ms");
            }
            Console.WriteLine($"mxParser time max: {times.Max()} ms, min: {times.Min()}, average: {times.Average()}");
        }
    }
}
