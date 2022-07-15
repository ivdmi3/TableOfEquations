namespace FunctionListWorkerGui
{
    partial class FormulaResolver
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.CompositeFormula = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PossibleNames = new System.Windows.Forms.ComboBox();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.ViewData = new System.Windows.Forms.DataGridView();
            this.cType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoundTo = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ViewData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoundTo)).BeginInit();
            this.SuspendLayout();
            // 
            // CompositeFormula
            // 
            this.CompositeFormula.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CompositeFormula.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CompositeFormula.Location = new System.Drawing.Point(187, 12);
            this.CompositeFormula.Name = "CompositeFormula";
            this.CompositeFormula.Size = new System.Drawing.Size(363, 24);
            this.CompositeFormula.TabIndex = 0;
            this.CompositeFormula.TextChanged += new System.EventHandler(this.CompositeFormula_TextChanged);
            this.CompositeFormula.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CompositeFormula_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Итоговая формула";
            // 
            // PossibleNames
            // 
            this.PossibleNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PossibleNames.FormattingEnabled = true;
            this.PossibleNames.Location = new System.Drawing.Point(14, 54);
            this.PossibleNames.Name = "PossibleNames";
            this.PossibleNames.Size = new System.Drawing.Size(167, 21);
            this.PossibleNames.TabIndex = 4;
            // 
            // tbValue
            // 
            this.tbValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbValue.Location = new System.Drawing.Point(187, 55);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(217, 20);
            this.tbValue.TabIndex = 5;
            this.tbValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbValue_KeyPress);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(483, 52);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(67, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ViewData
            // 
            this.ViewData.AllowUserToAddRows = false;
            this.ViewData.AllowUserToDeleteRows = false;
            this.ViewData.AllowUserToResizeRows = false;
            this.ViewData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.ViewData.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.ViewData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ViewData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cType,
            this.cName,
            this.cValue,
            this.cResult});
            this.ViewData.Location = new System.Drawing.Point(14, 81);
            this.ViewData.Name = "ViewData";
            this.ViewData.Size = new System.Drawing.Size(536, 138);
            this.ViewData.TabIndex = 7;
            this.ViewData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ViewData_CellEndEdit);
            this.ViewData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewData_KeyDown);
            // 
            // cType
            // 
            this.cType.HeaderText = "Тип";
            this.cType.Name = "cType";
            this.cType.ReadOnly = true;
            this.cType.Width = 51;
            // 
            // cName
            // 
            this.cName.HeaderText = "Имя";
            this.cName.Name = "cName";
            this.cName.Width = 54;
            // 
            // cValue
            // 
            this.cValue.HeaderText = "Значение";
            this.cValue.Name = "cValue";
            this.cValue.Width = 80;
            // 
            // cResult
            // 
            this.cResult.HeaderText = "Результат";
            this.cResult.Name = "cResult";
            this.cResult.ReadOnly = true;
            this.cResult.Width = 84;
            // 
            // RoundTo
            // 
            this.RoundTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RoundTo.Location = new System.Drawing.Point(413, 55);
            this.RoundTo.Name = "RoundTo";
            this.RoundTo.Size = new System.Drawing.Size(49, 20);
            this.RoundTo.TabIndex = 8;
            this.RoundTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.RoundTo.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(410, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Округление";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Имя функции или константы";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(184, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Значение";
            // 
            // FormulaResolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RoundTo);
            this.Controls.Add(this.ViewData);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.PossibleNames);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CompositeFormula);
            this.MinimumSize = new System.Drawing.Size(300, 100);
            this.Name = "FormulaResolver";
            this.Size = new System.Drawing.Size(562, 231);
            ((System.ComponentModel.ISupportInitialize)(this.ViewData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoundTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CompositeFormula;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox PossibleNames;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView ViewData;
        private System.Windows.Forms.NumericUpDown RoundTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn cType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn cResult;
    }
}
