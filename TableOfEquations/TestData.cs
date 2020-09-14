using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMatrix
{
    class TestData
    {
        private static Random rnd = new Random();
        private static char[] operations = { '+', '-', '*', '/' };
                    
        private static int GenerateInt()
        {
            return Math.Abs(rnd.Next(1000)) + 1;
        }

        private static double GenerateDouble()
        {
            return Math.Abs(rnd.NextDouble()*1000) + 1;
        }

        private static string GenerateExp(int expLength, List<string> cNames)
        {
            StringBuilder sb = new StringBuilder("=");

            int operationsCount = rnd.Next(expLength);

            int NamesCount = cNames.Count();
            
            for (int i = 0; i < operationsCount; i++)
            {
                int operationSelector = rnd.Next(100) % 4;
                int variableSelector = rnd.Next(100) % 3;
                string variable = string.Empty;
                switch (variableSelector)
                {
                    case 0:
                        variable = GenerateInt().ToString();
                        break;
                    case 1:
                        variable = GenerateDouble().ToString();
                        break;
                    case 2:
                        int nameSelector = rnd.Next(NamesCount);
                        variable = cNames[nameSelector];
                        break;
                }
                sb.AppendFormat("{0}{1}", variable, operations[operationSelector]);
            }
            sb.Append(GenerateInt());
            return sb.ToString();
        }

        private static string GenerateExp(int expLength)
        {
            StringBuilder sb = new StringBuilder("=");

            int operationsCount = rnd.Next(expLength);

            for (int i = 0; i < operationsCount; i++)
            {
                int operationSelector = rnd.Next(100) % 4;
                int variableSelector = rnd.Next(100) % 2;
                string variable = string.Empty;
                switch (variableSelector)
                {
                    case 0:
                        variable = GenerateInt().ToString();
                        break;
                    case 1:
                        variable = GenerateDouble().ToString();
                        break;
                }
                sb.AppendFormat("{0}{1}", variable, operations[operationSelector]);
            }
            sb.Append(GenerateInt());
            return sb.ToString();
        }

        public static IEnumerable<string> Generate(int amount, int length, IEnumerable<string> cNames)
        {
            List<string> list = new List<string>();

            while(amount!=0)
            {
                list.Add(GenerateExp(length, new List<string>(cNames)));
                amount--;
            }

            return list;
        }

        public static IEnumerable<string> Generate(int amount, int length)
        {
            List<string> list = new List<string>();

            while (amount != 0)
            {
                list.Add(GenerateExp(length));
                amount--;
            }

            return list;
        }
    }
}
