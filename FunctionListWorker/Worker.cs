using MathWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FunctionListWorker
{
    public class Worker : IDisposable
    {

        #region ctors

        public Worker()
        {
            this.ListOfSubFunctions = new FunctionList();
            this.Calculator = new EquationParser();
            this.Calculator.SetConstants(new Dictionary<string, decimal>());
            this.mainFunction = null;
            patterns = new Dictionary<string, Regex>();
        }

        public Worker(FunctionInfo MainFunction) : this(MainFunction, new FunctionList(), new Dictionary<string, decimal>())
        {

        }

        public Worker(FunctionInfo MainFunction, FunctionList ListOfSubFunctions, Dictionary<string, decimal> ListOfVariables)
        {
            patterns = new Dictionary<string, Regex>();

            foreach (var function in ListOfSubFunctions)
                patterns.Add(function.Key, new Regex(string.Format(FunctionNamePattern, function.Value.Name)));

            this.ListOfSubFunctions = ListOfSubFunctions;
            this.mainFunction = MainFunction;
            this.Calculator = new EquationParser();
            this.Calculator.SetConstants(ListOfVariables);
        }

        public void Dispose()
        {
            Calculator = null;
            ListOfSubFunctions = null;
        }

        #endregion

        #region PublicAPI

        public EquationParser Calculator { get; private set; }

        public void SetMainFunction(string Name, string Text)
        {
            mainFunction = new FunctionInfo(Name, Text);
        }

        public void SetMainFunction(FunctionInfo value)
        {
            mainFunction = value;
        } 

        public void AddSubFunction(string Name, string Text, int RoundTo)
        {
            if (!patterns.ContainsKey(Name))
                patterns.Add(Name, new Regex(string.Format(FunctionNamePattern, Name)));
            ListOfSubFunctions.Add(new FunctionInfo(Name, Text, RoundTo));
        }

        public void AddSubFunction(FunctionInfo value)
        {
            if (!patterns.ContainsKey(value.Name))
                patterns.Add(value.Name, new Regex(string.Format(FunctionNamePattern, value.Name)));
            ListOfSubFunctions.Add(value);
        }

        public void EditSubFunction(string Name, string Text)
        {
            if (ListOfSubFunctions.ContainsKey(Name))
                ListOfSubFunctions[Name].Text = Text;
        }

        public void DeleteSubFunction(string Name)
        {
            if (ListOfSubFunctions.ContainsKey(Name))
                ListOfSubFunctions.Remove(Name);
            if(Calculator.HasConstant(Name))
                Calculator.DeleteConstant(Name);
        }

        public void AddConstantValue(string Name, decimal Value)
        {
            Calculator.AddConstant(Name, Value);
        }

        public void EditConstantValue(string Name, decimal Value)
        {
            if (Calculator.HasConstant(Name))
                Calculator.EditConstant(Name, Value);
        }


        public decimal Сalculate()
        {
            if (mainFunction == null)
                throw new ArgumentNullException("Не указана итоговая функция!");
            else
                return internalCalculate(ListOfSubFunctions.Values);
        }

        public FunctionList GetSubFunctions()
        {
            return ListOfSubFunctions;
        }

        public Dictionary<string, decimal> GetConstants()
        {
            return Calculator.GetConstants();
        }

        public FunctionInfo MainFunction {
            get {
                return mainFunction;
            }
            private set {
                mainFunction = value;
            }
        }

        #endregion

        #region Implementation

        private FunctionList ListOfSubFunctions;
        private FunctionInfo mainFunction;
        private string FunctionNamePattern = @"[+-/*]?\(*{0}\)*";

        private Dictionary<string,Regex> patterns;

        private decimal internalCalculate(IEnumerable<FunctionInfo> SubFunctions)
        {
            Queue<FunctionInfo> FunctionsYetToBeResolved = new Queue<FunctionInfo>(SubFunctions);

            while (FunctionsYetToBeResolved.Count > 0)
            {
                FunctionInfo function = FunctionsYetToBeResolved.Dequeue();

                if (function.TryResolve(FunctionsYetToBeResolved, Calculator))
                {
                    if (Calculator.HasConstant(function.Name))
                        Calculator.EditConstant(function.Name, function.Result);
                    else
                        Calculator.AddConstant(function.Name, function.Result);
                }
                else
                    FunctionsYetToBeResolved.Enqueue(function);
            }

            string result = Calculator.Calculate(mainFunction.Text);

            return decimal.Parse(result);
        }

        private decimal internalCalculate2(FunctionInfo InitialFunction, Queue<FunctionInfo> FunctionNames)
        {
            while (FunctionNames.Count > 0)
            {
                FunctionInfo function = FunctionNames.Dequeue();
                if (InitialFunction.Text.Contains(function.Name))
                    if (function.TryResolve(FunctionNames, Calculator))
                        Calculator.AddConstant(function.Name, function.Result);
                    else
                        FunctionNames.Enqueue(function);
            }

            return decimal.Parse(Calculator.Calculate(InitialFunction.Text));
        }

        #endregion

    }
}
