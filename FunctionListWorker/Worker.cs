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
            ListOfSubFunctions.Add(new FunctionInfo(Name, Text, RoundTo));
        }

        public void AddSubFunction(FunctionInfo value)
        {
            ListOfSubFunctions.Add(value);
        }

        public void EditSubFunction(string Name, string Text)
        {
            ListOfSubFunctions[Name].Text = Text;
        }

        public void DeleteSubFunction(string Name)
        {
            ListOfSubFunctions.Remove(Name);
            if(Calculator.HasConstant(Name)) ;
        }

        public void AddConstantValue(string Name, decimal Value)
        {
            Calculator.AddConstant(Name, Value);
        }

        
        public decimal Сalculate()
        {
            if (mainFunction == null)
                throw new ArgumentNullException("Не указана итоговая функция!");
            else
                return internalCalculate(mainFunction, new Queue<FunctionInfo>(ListOfSubFunctions.Values));
        }

        public List<FunctionInfo> GetSubResults()
        {
            return ListOfSubFunctions.Values.ToList();
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

        private decimal internalCalculate2(FunctionInfo InitialFunction, Queue<FunctionInfo> FunctionNames)
        {
            while(FunctionNames.Count > 0)
            {
                FunctionInfo function = FunctionNames.Dequeue();
                if ( InitialFunction.Text.Contains(function.Name))
                    if (function.TryResolve(FunctionNames, Calculator))
                        Calculator.AddConstant(function.Name, function.Result); 
                    else
                        FunctionNames.Enqueue(function);
            }

            return decimal.Parse(Calculator.Calculate(InitialFunction.Text));
        }

        private decimal internalCalculate(FunctionInfo InitialFunction, Queue<FunctionInfo> FunctionNames)
        {
            while (FunctionNames.Count > 0)
            {
                FunctionInfo function = FunctionNames.Dequeue();

                if (!patterns.ContainsKey(function.Name))
                    patterns.Add(function.Name, new Regex(string.Format(FunctionNamePattern, function.Name)));

                if (patterns[function.Name].IsMatch(InitialFunction.Text))
                    if (function.TryResolve(FunctionNames, Calculator))
                        if(Calculator.HasConstant(function.Name))
                            Calculator.EditConstant(function.Name, function.Result);
                        else
                            Calculator.AddConstant(function.Name, function.Result);
                    else
                        FunctionNames.Enqueue(function);
            }

            return decimal.Parse(Calculator.Calculate(InitialFunction.Text));
        }

        #endregion

    }
}
