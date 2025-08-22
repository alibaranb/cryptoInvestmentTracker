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
            btnAddTab.Margin = new Padding(3, 4, 3, 4);
            btnAddTab.Name = "btnAddTab";
            btnAddTab.Size = new Size(86, 56);
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
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1041, 56);
            panel1.TabIndex = 3;
            // 
            // btnCalculate
            // 
            btnCalculate.Dock = DockStyle.Left;
            btnCalculate.Location = new Point(172, 0);
            btnCalculate.Margin = new Padding(3, 4, 3, 4);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(86, 56);
            btnCalculate.TabIndex = 4;
            btnCalculate.Text = "Calculate Profit";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += button2_Click;
            // 
            // btnRemTab
            // 
            btnRemTab.Dock = DockStyle.Left;
            btnRemTab.Location = new Point(86, 0);
            btnRemTab.Margin = new Padding(3, 4, 3, 4);
            btnRemTab.Name = "btnRemTab";
            btnRemTab.Size = new Size(86, 56);
            btnRemTab.TabIndex = 3;
            btnRemTab.Text = "Remove Tab";
            btnRemTab.UseVisualStyleBackColor = true;
            btnRemTab.Click += button1_Click;
            // 
            // tabControl1
            // 
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 56);
            tabControl1.Margin = new Padding(3, 4, 3, 4);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1041, 668);
            tabControl1.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1041, 724);
            Controls.Add(tabControl1);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
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
