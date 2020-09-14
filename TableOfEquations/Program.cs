using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMatrix
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
                { "poi", 789},
                { "hjk", 100},
                { "iuo", 55}
            };

            int amountOfTest = 10;
            int amountOfExpressions = 10000;
            int amountOfOperands = 20;
            IEnumerable<string>[] data = new IEnumerable<string>[amountOfTest] ;

            //EquationParserShow(amountOfExpressions, amountOfOperands, constants);

            for (int i = 0; i < amountOfTest; i++)
            {
                data[i] = TestData.Generate(amountOfExpressions, amountOfOperands, constants.Keys);
            }

            EquationParserTest(data, constants);

            Console.WriteLine();

            mxParserTest(data, constants);

            Console.ReadKey();

        }

        private static void EquationParserShow(int amount, int length, Dictionary<string, double> constants)
        {
            EquationParser ep = new EquationParser();
            ep.SetConstants(constants);

            IEnumerable<string> data = TestData.Generate(10000, 20, constants.Keys);

            foreach (string exp in data)
            {
                string result = ep.Calculate(exp);
                Console.WriteLine($"Expression: {exp} Result: {result}");
            }
        }
        

        private static void EquationParserTest(IEnumerable<string>[] data, Dictionary<string, double> constants)
        {
            EquationParser ep = new EquationParser();
            ep.SetConstants(constants);

            Stopwatch sw = new Stopwatch();

            for (int i = 0; i < data.Length; i++)
            {
                sw.Restart();

                foreach (string exp in data[i])
                    ep.Calculate(exp);                

                sw.Stop();

                Console.WriteLine($"№{i + 1} EquationParser time: {sw.ElapsedMilliseconds} ms");
            }
        }

        private static void mxParserTest(IEnumerable<string>[] data, Dictionary<string, double> constants)
        {
            Expression expr = new Expression();
            foreach (var item in constants)
                expr.defineConstant(item.Key, item.Value);

            Stopwatch sw = new Stopwatch();

            for (int i = 0; i < data.Length; i++)
            {
                sw.Restart();
                foreach (string exp in data[i])
                {
                    expr.setExpressionString(exp);
                    expr.calculate();
                }
                sw.Stop();

                Console.WriteLine($"№{i + 1} mxParser time: {sw.ElapsedMilliseconds} ms");
            }
        }
    }
}
