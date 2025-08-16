namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnAddTab = new Button();
            panel1 = new Panel();
            btnCalculate = new Button();
            btnRemTab = new Button();
            tabControl1 = new TabControl();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnAddTab
            // 
            btnAddTab.Dock = DockStyle.Left;
            btnAddTab.Location = new Point(0, 0);
            btnAddTab.Name = "btnAddTab";
            btnAddTab.Size = new Size(75, 42);
            btnAddTab.TabIndex = 2;
            btnAddTab.Text = "Add Tab";
            btnAddTab.UseVisualStyleBackColor = true;
            btnAddTab.Click += btnAddTab_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCalculate);
            panel1.Controls.Add(btnRemTab);
            panel1.Controls.Add(btnAddTab);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(669, 42);
            panel1.TabIndex = 3;
            // 
            // btnCalculate
            // 
            btnCalculate.Dock = DockStyle.Left;
            btnCalculate.Location = new Point(150, 0);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(75, 42);
            btnCalculate.TabIndex = 4;
            btnCalculate.Text = "Calculate Profit";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += button2_Click;
            // 
            // btnRemTab
            // 
            btnRemTab.Dock = DockStyle.Left;
            btnRemTab.Location = new Point(75, 0);
            btnRemTab.Name = "btnRemTab";
            btnRemTab.Size = new Size(75, 42);
            btnRemTab.TabIndex = 3;
            btnRemTab.Text = "Remove Tab";
            btnRemTab.UseVisualStyleBackColor = true;
            btnRemTab.Click += button1_Click;
            // 
            // tabControl1
            // 
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 42);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(669, 501);
            tabControl1.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(669, 543);
            Controls.Add(tabControl1);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Crypto Investment Calculator";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button btnAddTab;
        private Panel panel1;
        private TabControl tabControl1;
        private Button btnRemTab;
        private Button btnCalculate;
    }
}
