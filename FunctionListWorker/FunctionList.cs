using System.Collections.Generic;

namespace FunctionListWorker
{
    public class FunctionList : Dictionary<string, FunctionInfo>
    {

        public FunctionList() {

        }

        public void Add(FunctionInfo item)
        {
            Add(item.Name, item);
        }

        private new void Add(string itemName, FunctionInfo item)
        {
            base.Add(itemName, item);
        }
    }
}
