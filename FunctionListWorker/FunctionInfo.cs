using MathWorker;
using System.Collections.Generic;

namespace FunctionListWorker
{
    public class FunctionInfo
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public decimal Result { get;  set; }

        public FunctionInfo() { }

        public FunctionInfo(string Name, string Text)
        {
            this.Name = Name;
            this.Text = Text;
        }

        public bool TryResolve(Queue<FunctionInfo> functionsYetToResolve, EquationParser Calculator)
        {
            foreach(FunctionInfo f in functionsYetToResolve)
                if(Text.Contains(f.Name))
                    return false;

            Result = decimal.Parse(Calculator.Calculate(Text));
            return true;
        }

    }
}
