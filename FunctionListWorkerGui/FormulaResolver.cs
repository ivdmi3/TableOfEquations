using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FunctionListWorker;
using System.Linq;

using System.Text.RegularExpressions;

namespace FunctionListWorkerGui
{
    public partial class FormulaResolver: UserControl
    {
        private static Regex NameFinder;
        private static Regex ConstantFinder;
        private Worker FormulaWorker;

        private HashSet<string> AlreadyInUse;
        private HashSet<string> ExtraFoundlings;

        static FormulaResolver()
        {
            NameFinder = new Regex(@"\b(\d*[А-Яа-яA-Za-z]+\d*)+\b", RegexOptions.Compiled);
            ConstantFinder = new Regex(@"^(\-?\d+(?:(?:\.|,)\d*)?|(?:\.|,)\d+)$", RegexOptions.Compiled);
        }

        public FormulaResolver()
        {
            InitializeComponent();
            ExtraFoundlings = new HashSet<string>();
            AlreadyInUse = new HashSet<string>();
            FormulaWorker = new Worker();
        }


        private void btnResolve_Click(object sender, EventArgs e)
        {
            string Formula = CompositeFormula.Text.Replace(".", ",");

            if (!Formula.StartsWith("="))
                Formula = Formula.Insert(0, "=");

            FormulaWorker.SetMainFunction("Result", Formula);

            textBox2.Text = FormulaWorker.Сalculate().ToString();

        }

        private void CompositeFormula_TextChanged(object sender, EventArgs e)
        {
            FillPossibleNames();
        }

        private void FillPossibleNames()
        {
            PossibleNames.Items.Clear();

            IEnumerable<string> foundlings = 
                NameFinder.
                Matches(CompositeFormula.Text).
                Cast<Match>().
                Select(x => x.Value).
                Concat(ExtraFoundlings);
            
            PossibleNames.Items.Add(string.Empty);

            foreach (string foundling in foundlings)
                if(!AlreadyInUse.Contains(foundling))
                    PossibleNames.Items.Add(foundling);
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {

            string Name = PossibleNames.Text.Trim();
            string Text = tbValue.Text.Trim().Replace(".", ",");

            bool isFunction = !ConstantFinder.IsMatch(Text);
            string TypeOfItem = isFunction ? "функция" : "константа";

            if (isFunction)
            {
                if (!Text.StartsWith("="))
                    Text = Text.Insert(0, "=");

                FormulaWorker.AddSubFunction(Name, Text);

                MatchCollection foundlings = NameFinder.Matches(Text);
                foreach (Match foundling in foundlings)
                    if (!ExtraFoundlings.Contains(foundling.Value))
                        ExtraFoundlings.Add(foundling.Value);
            }
            else
                FormulaWorker.AddConstantValue(Name, decimal.Parse(Text));

            ViewData.Rows.Add(new object[] { TypeOfItem, Name, Text });

            AlreadyInUse.Add(Name);

            PossibleNames.Text = string.Empty;
            tbValue.Text = string.Empty;

            FillPossibleNames();
        }
    }
}
