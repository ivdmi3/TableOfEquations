using MathWorker;
using System;
using System.Collections.Generic;

namespace FunctionListWorker
{
    public class FunctionInfo
    {
        public string Name { get; set; }
        public string Text {
            get { return text;}
            set {
                if (value.StartsWith("="))
                    text = value;
                else
                    text = "=" + value;
            }
        }
        public decimal Result { get;  set; }
        public int RoundTo { get; set; }

        private string text;

        public FunctionInfo() { }

        public FunctionInfo(string Name, string Text)
        {
            this.Name = Name;
            this.Text = Text;
            this.RoundTo = 2;
        }

        public FunctionInfo(string Name, string Text, int RoundTo)
        {
            this.Name = Name;
            this.Text = Text;         
            this.RoundTo = RoundTo;
        }

        public bool TryResolve(Queue<FunctionInfo> YetToResolve, EquationParser Calculator)
        {
            foreach(FunctionInfo f in YetToResolve)
                if(Text.Contains(f.Name))
                    return false;

            Result = Math.Round(decimal.Parse(Calculator.Calculate(Text)), RoundTo);
            return true;
        }

    }
}
