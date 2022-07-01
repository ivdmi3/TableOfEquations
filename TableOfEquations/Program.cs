using FunctionListWorker;
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
            Dictionary<string, decimal> constants = new Dictionary<string, decimal>()
            {
                {"афыва", 20},
                {"вв", 13},
                {"DEPTH", 100},
                {"аа", 123},
                {"fedw", 4},
                {"ыва", 43},
                {"qwee", 654},
                {"qwe", 8521},
                {"yuio", 41581},
                {"poi", 789},
                {"hjk", 100},
                {"iuo", 55}
            };

            int amountOfTest = 20;

            IEnumerable<string>[] data = new IEnumerable<string>[amountOfTest] ;
#if false
            int amountOfExpressions = 10000;
            int amountOfOperands = 20;
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
#elif false
            EquationParserShow(100, 10, constants);
#elif true

            FunctionInfo mainFunction = new FunctionInfo("main", "=Материалы+Услуги+Амортизация+ОЗП+НСХ+ДЗП+СВ+ОПР+ОХР+ТЗР+ВПЗ");
            FunctionList listOfSubFunctions = new FunctionList
            {
                new FunctionInfo("ОЗП", "=трудзатр*стоимость"),
                new FunctionInfo("НСХ", "=ОЗП*КНСХ"),
                new FunctionInfo("ДЗП", "=(ОЗП+НСХ)*КДЗП"),
                new FunctionInfo("СВ", "=(ОЗП+НСХ+ДЗП)*КСВ"),
                new FunctionInfo("ОПР", "=ОЗП*КОПР"),
                new FunctionInfo("ОХР", "=ОЗП*КОХР"),
                new FunctionInfo("ТЗР", "=Материалы*КТЗР"),
                new FunctionInfo("ВПЗ", "=ОЗП*КВПЗ")
            };

            Dictionary<string, decimal> variables = new Dictionary<string, decimal>()
            {
                {"Материалы", 278M },
                {"Услуги", 50M },
                {"Амортизация", 0M },
                {"трудзатр", 1.24M },
                {"стоимость", 430.41M },
                {"КНСХ", 0.0792M },
                {"КДЗП", 0.0949M },                
                {"КСВ", 0.3327M },
                {"КОПР", 0.9124M },
                {"КОХР", 0.4058M },
                {"КТЗР", 0.054M },
                {"КВПЗ", 0 }
            };

            Worker fw = new Worker(mainFunction, listOfSubFunctions, variables);
            Console.WriteLine("RESULT: " + fw.Сalculate());

            foreach(FunctionInfo result in fw.GetSubResults())
            {
                Console.WriteLine(result.Name + ": " + result.Result);
            }

#endif
            Console.ReadKey();

        }

        private static void EquationParserShow(int amount, int length, Dictionary<string, decimal> constants)
        {
            EquationParser ep = new EquationParser();
            ep.SetConstants(constants);

            IEnumerable<string> data = TestData.Generate(10000, 20, constants.Keys);

            foreach (string exp in data)
            {
                decimal result = decimal.Parse(ep.Calculate(exp));
                Console.WriteLine($"Expression: {exp,-150} Result: {result,-150:F2}");
            }
        }

        private static void EquationParserShow(int amount, int length)
        {
            EquationParser ep = new EquationParser(); 

            IEnumerable<string> data = TestData.Generate(10000, 20);

            foreach (string exp in data)
            {
                decimal result = decimal.Parse(ep.Calculate(exp));
                Console.WriteLine($"Expression: {exp,-150} Result: {result,-150:F2}");
            }
        }

        private static void EquationParserTest(IEnumerable<string>[] data, Dictionary<string, decimal> constants)
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

        private static void mxParserTest(IEnumerable<string>[] data, Dictionary<string, decimal> constants)
        {
            for (int i= true ? 0 : 1 ; i<10; i+=2 )
            {

            }

            string s = new string(new List<char>().ToArray());
            
            Expression expr = new Expression();

            foreach (var item in constants)
                expr.defineConstant(item.Key, decimal.ToDouble(item.Value));

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
