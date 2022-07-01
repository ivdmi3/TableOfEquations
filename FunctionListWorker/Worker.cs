using MathWorker;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionListWorker
{
    public class Worker
    {

        #region ctors

        public Worker()
        {
            this.ListOfSubFunctions = new FunctionList();
            this.Calculator = new EquationParser();
            this.Calculator.SetConstants(new Dictionary<string, decimal>());
            this.MainFunction = null;
        }

        public Worker(FunctionInfo MainFunction) : this(MainFunction, new FunctionList(), new Dictionary<string, decimal>())
        {

        }

        public Worker(FunctionInfo MainFunction, FunctionList ListOfSubFunctions, Dictionary<string, decimal> ListOfVariables)
        {
            this.ListOfSubFunctions = ListOfSubFunctions;
            this.MainFunction = MainFunction;
            this.Calculator = new EquationParser();
            this.Calculator.SetConstants(ListOfVariables);
        }
        #endregion

        #region PublicAPI

        public EquationParser Calculator { get; private set; }

        public void SetMainFunction(string Name, string Text)
        {
            MainFunction = new FunctionInfo(Name, Text);
        }

        public void SetMainFunction(FunctionInfo value)
        {
            MainFunction = value;
        }

        public void AddSubFunction(string Name, string Text)
        {
            ListOfSubFunctions.Add(new FunctionInfo(Name, Text));
        }

        public void AddSubFunction(FunctionInfo value)
        {
            ListOfSubFunctions.Add(value);
        }
        
        public void AddConstantValue(string Name, decimal Value)
        {
            Calculator.AddConstant(Name, Value);
        }

        public decimal Сalculate()
        {
            if (MainFunction == null)
                throw new ArgumentNullException("Не указана итоговая функция!");
            else
                return internalCalculate(MainFunction, new Queue<FunctionInfo>(ListOfSubFunctions.Values));
        }

        public List<FunctionInfo> GetSubResults()
        {
            return ListOfSubFunctions.Values.ToList();
        }

        #endregion

        #region Implementation

        private FunctionList ListOfSubFunctions;
        private FunctionInfo MainFunction;

        private decimal internalCalculate(FunctionInfo InitialFunction, Queue<FunctionInfo> FunctionNames)
        {
            Queue<FunctionInfo> queue = FunctionNames;

            while(queue.Count > 0)
            {
                FunctionInfo function = queue.Dequeue();
                if (InitialFunction.Text.Contains(function.Name))
                    if (function.TryResolve(queue, Calculator))
                        Calculator.AddConstant(function.Name, function.Result); 
                    else
                        queue.Enqueue(function);
            }

            return decimal.Parse(Calculator.Calculate(InitialFunction.Text));
        }

        #endregion

    }
}
