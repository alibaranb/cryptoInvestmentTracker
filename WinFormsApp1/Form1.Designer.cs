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
            tabControl1 = new TabControl();
            button1 = new Button();
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
            panel1.Controls.Add(button1);
            panel1.Controls.Add(btnAddTab);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(669, 42);
            panel1.TabIndex = 3;
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
            // button1
            // 
            button1.Dock = DockStyle.Left;
            button1.Location = new Point(75, 0);
            button1.Name = "button1";
            button1.Size = new Size(75, 42);
            button1.TabIndex = 3;
            button1.Text = "Remove Tab";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
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
        private Button button1;
    }
}
