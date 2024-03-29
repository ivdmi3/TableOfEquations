﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FunctionListWorker;
using System.Linq;


using System.Text.RegularExpressions;

namespace FunctionListWorkerGui
{
    public partial class FormulaResolver: UserControl
    {
        
        static FormulaResolver()
        {
            // ([-+/*]-?\(*(\w+|\d+(\.\d+)?)\)*)*
            CheckFormulaCorrectness = new Regex(@"[A-Za-zА-Яа-я0-9\^\+\-\*\/\(\)\.\,=]", RegexOptions.Compiled);
            NameFinder = new Regex(@"\b(\d*[А-Яа-яA-Za-z]+\d*)+\b", RegexOptions.Compiled);
            ConstantFinder = new Regex(@"^(\-?\d+(?:(?:\.|,)\d*)?|(?:\.|,)\d+)$", RegexOptions.Compiled);
        }

        #region Ctor
        public FormulaResolver(IEnumerable<string> names)
        {
            InitializeComponent();
            ExtraFoundlings = new HashSet<string>();
            AlreadyInUse = new HashSet<string>();
            FormulaWorker = new Worker();
            foreach (string name in names)
                AlreadyInUse.Add(name);
        }

        public FormulaResolver() : this(new List<string>()) { }
        #endregion

        #region Public API
        public decimal Result { get; private set; }

        //public Dictionary<string, decimal> SubResults { get; private set; }
        public Dictionary<string, decimal> Constants { get; private set; }

        public void SetNamesAlreadyInUse(IEnumerable<string> names)
        {
            foreach (string name in names)
                AlreadyInUse.Add(name);
        }

        public void AddConstant(string name, decimal value)
        {
            FormulaWorker.AddConstantValue(name, value);
        }

        public void SetContextMenu(ContextMenuStrip menu)
        {
            ViewData.ContextMenuStrip = menu;
        }

        public RawData ReturnRawData()
        {
               return FormulaWorker.ReturnRawData();
        }

        public void SetFromRawData(RawData data)
        {
            Clear();

            FormulaWorker = new Worker();
            FormulaWorker.SetFromRawData(data);

            foreach(var function in data.ExtraFunctions)
            {
                AlreadyInUse.Add(function.Key);
                int index = ViewData.Rows.Add();
                ViewData.Rows[index].Cells[cName.Index].Value = function.Key;
                ViewData.Rows[index].Cells[cValue.Index].Value = function.Value.Item1;
                ViewData.Rows[index].Cells[cType.Index].Value = FUNCTION_TEXT;
            }                

            foreach (var constant in data.Constants)
            {
                AlreadyInUse.Add(constant.Key);
                int index = ViewData.Rows.Add();
                ViewData.Rows[index].Cells[cName.Index].Value = constant.Key;
                ViewData.Rows[index].Cells[cValue.Index].Value = constant.Value;
                ViewData.Rows[index].Cells[cType.Index].Value = CONSTANT_TEXT;
            }

            CompositeFormula.Text = FormulaWorker.MainFunction.Text;
        }

        public void Run()
        {
            if (CompositeFormula.Text.Trim().Equals(string.Empty)) return;

            string Formula = NeutralizeLocale(CompositeFormula.Text);

            FormulaWorker.SetMainFunction("Result", Formula);

            Result = FormulaWorker.Calculate();

            Constants = FormulaWorker.GetConstants();

            foreach (var subResult in FormulaWorker.GetSubFunctions())
            {
                var tmp = ViewData.
                     Rows.
                     Cast<DataGridViewRow>().
                     Where(c => c.Cells[cName.Index].Value.ToString() == subResult.Key);

                if (tmp.Any())
                {
                   tmp.First().
                   Cells[cResult.Index].Value = subResult.Value.Result;
                }
            }
        }

        public void Clear()
        {
            FormulaWorker = new Worker();
            CompositeFormula.Text = string.Empty;
            tbValue.Text = string.Empty;
            ExtraFoundlings = new HashSet<string>();
            AlreadyInUse = new HashSet<string>();
            ViewData.Rows.Clear();
        }

        #endregion

        #region Implementation

       
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
                Distinct().
                Concat(ExtraFoundlings);
            
            PossibleNames.Items.Add(string.Empty);

            foreach (string foundling in foundlings)
                if(!AlreadyInUse.Contains(foundling))
                    PossibleNames.Items.Add(foundling);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (PossibleNames.Text.Trim().Equals(string.Empty) || tbValue.Text.Trim().Equals(string.Empty)) return;

            string Name = PossibleNames.Text.Trim();
            string Text = NeutralizeLocale(tbValue.Text.Trim());

            bool isFunction = !ConstantFinder.IsMatch(Text);
            string TypeOfItem = isFunction ? FUNCTION_TEXT : CONSTANT_TEXT;

            if (isFunction)
            {
                FormulaWorker.AddSubFunction(Name, Text, (int) RoundTo.Value);
                FillExtraFoundlings(Text);
            }
            else
                FormulaWorker.AddConstantValue(Name, decimal.Parse(Text));

            ViewData.Rows.Add(new object[] { TypeOfItem, Name, Text });

            AlreadyInUse.Add(Name);

            PossibleNames.Text = string.Empty;
            tbValue.Text = string.Empty;

            FillPossibleNames();
        }

        private void CompositeFormula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(CheckFormulaCorrectness.IsMatch(e.KeyChar.ToString()) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(CheckFormulaCorrectness.IsMatch(e.KeyChar.ToString()) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void ViewData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = ViewData.Rows[e.RowIndex];

            if(row.Cells[cName.Index].Value == null || row.Cells[cValue.Index].Value == null)
                return;

            string Name = row.Cells[cName.Index].Value.ToString();
            string Text = NeutralizeLocale(row.Cells[cValue.Index].Value.ToString());

            switch (row.Cells[cType.Index].Value.ToString())
            {
                case FUNCTION_TEXT:
                    FormulaWorker.EditSubFunction(Name, Text);
                    FillExtraFoundlings(Text);
                    FillPossibleNames();
                    break;
                case CONSTANT_TEXT:
                    FormulaWorker.
                        Calculator.
                        EditConstant(Name, decimal.Parse(Text));
                    break;
            }
        }

        private void ViewData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && ViewData.SelectedRows.Count == 1)
            {
                DataGridViewRow row = ViewData.Rows[ViewData.SelectedRows[0].Index];
                string Name = row.Cells[cName.Index].Value.ToString();
                switch (row.Cells[cType.Index].Value.ToString())
                {
                    case FUNCTION_TEXT:
                        FormulaWorker.DeleteSubFunction(Name);                        
                        break;
                    case CONSTANT_TEXT:
                        break;
                }
                AlreadyInUse.Remove(Name);
                FormulaWorker.Calculator.DeleteConstant(Name);
                ViewData.Rows.Remove(row);
                FillPossibleNames();
            }

        }

        private void FillExtraFoundlings(string Text)
        {
            MatchCollection foundlings = NameFinder.Matches(Text);
            foreach (Match foundling in foundlings)
                if (!ExtraFoundlings.Contains(foundling.Value))
                    ExtraFoundlings.Add(foundling.Value);
        }

        private string NeutralizeLocale(string Text)
        {
            return Text.Replace(".", ",");
        }


        private const string FUNCTION_TEXT = "функция"; //FUNCTION_TEXT CONSTANT_TEXT
        private const string CONSTANT_TEXT = "константа";
        private const string PARAMETER_TEXT = "параметр";

        private static Regex NameFinder;
        private static Regex CheckFormulaCorrectness;
        private static Regex ConstantFinder;

        private HashSet<string> AlreadyInUse;
        private HashSet<string> ExtraFoundlings;

        private Worker FormulaWorker
        {
            get { return _fw; }
            set
            {
                if (_fw != null)
                    _fw.Dispose();
                _fw = value;
            }
        }
        private Worker _fw;

        #endregion
        
       
    }
}
