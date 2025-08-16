using System.Data;
using System.Net.WebSockets;
using System.Text.Json;
using Websocket.Client;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        // Store each tab's data
        private Dictionary<string, DataTable> tabData = new Dictionary<string, DataTable>();
        private WebsocketClient? _client;


        public Form1()
        {
            InitializeComponent();
        }

        private void btnAddTab_Click(object sender, EventArgs e)
        {
            // Ask user for a tab name
            string tabName = Prompt.ShowDialog("Enter tab name (e.g., August 2025):", "New Investment Tab");

            if (string.IsNullOrWhiteSpace(tabName)) return;
            if (tabData.ContainsKey(tabName))
            {
                MessageBox.Show("Tab with this name already exists.");
                return;
            }

            // Create DataTable for this tab
            DataTable table = new DataTable();
            table.Columns.Add("Coin");
            table.Columns.Add("Amount ($)", typeof(decimal));
            table.Columns.Add("Buy Price ($)", typeof(decimal));
            table.Columns.Add("Amount Bought", typeof(decimal));
            table.Columns.Add("Current Price", typeof(decimal));
            table.Columns.Add("Profit/Loss", typeof(decimal));

            // Create new TabPage
            var tab = new TabPage(tabName);

            // Create DataGridView
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = table,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Name = "dataGridView_" + tabName
            };

            tab.Controls.Add(dgv);
            tabControl1.TabPages.Add(tab);
            tabControl1.SelectedTab = tab;

            // Store table in dictionary
            tabData[tabName] = table;
        }

        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 400,
                    Height = 150,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };

                Label textLabel = new Label() { Left = 20, Top = 20, Text = text, Width = 340 };
                TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 340 };
                Button confirmation = new Button() { Text = "OK", Left = 270, Width = 90, Top = 80 };
                confirmation.DialogResult = DialogResult.OK;

                prompt.Controls.AddRange(new Control[] { textLabel, inputBox, confirmation });
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabCount > 0)
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
            else
            {
                MessageBox.Show("No tabs found to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
