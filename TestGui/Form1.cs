using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private void btnAddData_Click(object sender, EventArgs e)
        //{ 
        //    List<string> usedNames = new List<string>();

        //    usedNames.Add("Материалы");
        //    usedNames.Add("Услуги");

        //    Calculator.SetNamesAlreadyInUse(usedNames);

        //    Calculator.AddConstant("Материалы", decimal.Parse(tbMaterials.Text.Trim()));
        //    Calculator.AddConstant("Услуги", decimal.Parse(tbUslugi.Text.Trim()));
        //}

        private void btnRun_Click(object sender, EventArgs e)
        {
            Calculator.Run();
            tbResult.Text = Calculator.Result.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbResult.Text = string.Empty;
            Calculator.Clear();
        }
    }
}
