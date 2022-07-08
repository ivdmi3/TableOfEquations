using FunctionListWorker;
using System;
using System.Collections.Generic;

namespace FunctionListWorkerGui
{
    public class RawData
    {

        public ValueTuple<string, string> Mainfunction { get; set; }
        public Dictionary<string, Tuple<string, int>> ExtraFunctions { get; set; }
        public Dictionary<string, decimal> Constants { get; set; }

    }
}
