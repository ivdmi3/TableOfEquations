using System.Collections.Generic;

namespace FunctionListWorker
{
    public class FunctionList : Dictionary<string, FunctionInfo>
    {
        private Dictionary<string, FunctionInfo> dictionary;

        public FunctionList() { }

        public FunctionList(Dictionary<string, FunctionInfo> dictionary)
        {
            foreach(var item in dictionary)
            {
                this.Add(item.Value);
            }
        }

        public FunctionList(IEnumerable<FunctionInfo> list)
        {
            foreach (var item in list)
            {
                this.Add(item);
            }
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
