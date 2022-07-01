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
            this.btnResolve = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.PossibleNames = new System.Windows.Forms.ComboBox();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.ViewData = new System.Windows.Forms.DataGridView();
            this.cType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ViewData)).BeginInit();
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
            this.CompositeFormula.Size = new System.Drawing.Size(314, 24);
            this.CompositeFormula.TabIndex = 0;
            this.CompositeFormula.TextChanged += new System.EventHandler(this.CompositeFormula_TextChanged);
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
            // btnResolve
            // 
            this.btnResolve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResolve.Location = new System.Drawing.Point(14, 148);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(75, 23);
            this.btnResolve.TabIndex = 2;
            this.btnResolve.Text = "Проверить";
            this.btnResolve.UseVisualStyleBackColor = true;
            this.btnResolve.Click += new System.EventHandler(this.btnResolve_Click);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox2.Location = new System.Drawing.Point(95, 150);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            // 
            // PossibleNames
            // 
            this.PossibleNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PossibleNames.FormattingEnabled = true;
            this.PossibleNames.Location = new System.Drawing.Point(14, 48);
            this.PossibleNames.Name = "PossibleNames";
            this.PossibleNames.Size = new System.Drawing.Size(121, 21);
            this.PossibleNames.TabIndex = 4;
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(141, 48);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(132, 20);
            this.tbValue.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(279, 46);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
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
            this.ViewData.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.ViewData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ViewData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cType,
            this.cName,
            this.cValue});
            this.ViewData.Location = new System.Drawing.Point(14, 76);
            this.ViewData.Name = "ViewData";
            this.ViewData.RowHeadersVisible = false;
            this.ViewData.Size = new System.Drawing.Size(487, 66);
            this.ViewData.TabIndex = 7;
            // 
            // cType
            // 
            this.cType.HeaderText = "Тип";
            this.cType.Name = "cType";
            // 
            // cName
            // 
            this.cName.HeaderText = "Имя";
            this.cName.Name = "cName";
            // 
            // cValue
            // 
            this.cValue.HeaderText = "Значение";
            this.cValue.Name = "cValue";
            // 
            // FormulaResolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ViewData);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.PossibleNames);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btnResolve);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CompositeFormula);
            this.MinimumSize = new System.Drawing.Size(300, 100);
            this.Name = "FormulaResolver";
            this.Size = new System.Drawing.Size(513, 179);
            ((System.ComponentModel.ISupportInitialize)(this.ViewData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CompositeFormula;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResolve;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox PossibleNames;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView ViewData;
        private System.Windows.Forms.DataGridViewTextBoxColumn cType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cValue;
    }
}
