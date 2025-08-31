using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Globalization;
using System.Net.Http;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Dictionary<string, DataTable> tabData = new Dictionary<string, DataTable>();
        private System.Windows.Forms.Timer priceUpdateTimer;
        private readonly string saveFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "save");
        private readonly string saveFile = "tabsData.json";
        private string SaveFilePath => Path.Combine(saveFolder, saveFile);

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAllTabsData();

            priceUpdateTimer = new System.Windows.Forms.Timer();
            priceUpdateTimer.Interval = 30_000; // 30 seconds
            priceUpdateTimer.Tick += PriceUpdateTimer_Tick;
            priceUpdateTimer.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SaveAllTabsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data on exit:\n" + ex.Message);
            }
        }

        private async Task<Dictionary<string, decimal>> GetCurrentPricesAsync(List<string> coinList)
        {
            Dictionary<string, decimal> prices = new();

            using (HttpClient client = new HttpClient())
            {
                string url = "https://api.binance.com/api/v3/ticker/price";
                var response = await client.GetStringAsync(url);
                JArray allPrices = JArray.Parse(response);

                foreach (string coin in coinList)
                {
                    var pair = allPrices.FirstOrDefault(p => p["symbol"]?.ToString() == coin.ToUpper() + "USDT");
                    if (pair != null)
                    {
                        prices[coin.ToUpper()] = decimal.Parse(pair["price"].ToString(), CultureInfo.InvariantCulture);
                    }
                }
            }

            return prices;
        }

        private async void PriceUpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                HashSet<string> allCoins = new();

                foreach (var table in tabData.Values)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string coin = row["Coin"]?.ToString()?.ToUpper();
                        if (!string.IsNullOrWhiteSpace(coin))
                            allCoins.Add(coin);
                    }
                }

                if (allCoins.Count == 0) return;

                var coinPrices = await GetCurrentPricesAsync(allCoins.ToList());

                foreach (var table in tabData.Values)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string coin = row["Coin"]?.ToString()?.ToUpper();
                        if (string.IsNullOrWhiteSpace(coin)) continue;

                        if (coinPrices.TryGetValue(coin, out decimal currentPrice))
                        {
                            row["CurrentPrice"] = Math.Round(currentPrice, 4);

                            if (decimal.TryParse(row["Invested"]?.ToString(), out decimal invested) &&
                                decimal.TryParse(row["BuyPrice"]?.ToString(), out decimal buyPrice))
                            {
                                decimal amount = Math.Round(invested / buyPrice, 4);
                                row["Amount"] = amount;

                                decimal currentValue = Math.Round(amount * currentPrice, 4);
                                row["CurrentValue"] = currentValue;

                                if (invested != 0)
                                {
                                    decimal profitLoss = Math.Round(((currentValue - invested) / invested) * 100, 4);
                                    row["ProfitLoss"] = profitLoss;
                                }
                                else
                                {
                                    row["ProfitLoss"] = 0m;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating prices:\n" + ex.Message);
            }
        }

        private void btnAddTab_Click(object sender, EventArgs e)
        {
            string tabName = Prompt.ShowDialog("Enter tab name (e.g., August 2025):", "New Investment Tab");

            if (string.IsNullOrWhiteSpace(tabName)) return;
            if (tabData.ContainsKey(tabName))
            {
                MessageBox.Show("Tab with this name already exists.");
                return;
            }

            DataTable table = CreateInvestmentTable(tabName);

            var tab = new TabPage(tabName);

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

            tabData[tabName] = table;

            SetColumnHeadersAndReadOnly(dgv);
        }

        private DataTable CreateInvestmentTable(string tableName)
        {
            DataTable table = new DataTable(tableName);

            table.Columns.Add("Coin", typeof(string));
            table.Columns.Add("Invested", typeof(decimal));
            table.Columns.Add("BuyPrice", typeof(decimal));
            table.Columns.Add("Amount", typeof(decimal));
            table.Columns.Add("CurrentPrice", typeof(decimal));
            table.Columns.Add("CurrentValue", typeof(decimal));
            table.Columns.Add("ProfitLoss", typeof(decimal));

            return table;
        }

        private void SetColumnHeadersAndReadOnly(DataGridView dgv)
        {
            dgv.Columns["Coin"].HeaderText = "Coin";
            dgv.Columns["Invested"].HeaderText = "Invested ($)";
            dgv.Columns["BuyPrice"].HeaderText = "Buy Price ($)";
            dgv.Columns["Amount"].HeaderText = "Amount";
            dgv.Columns["CurrentPrice"].HeaderText = "Current Price ($)";
            dgv.Columns["CurrentValue"].HeaderText = "Current Value ($)";
            dgv.Columns["ProfitLoss"].HeaderText = "Profit/Loss (%)";

            dgv.Columns["Amount"].ReadOnly = true;
            dgv.Columns["CurrentPrice"].ReadOnly = true;
            dgv.Columns["CurrentValue"].ReadOnly = true;
            dgv.Columns["ProfitLoss"].ReadOnly = true;
        }

        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new()
                {
                    Width = 400,
                    Height = 150,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };

                Label textLabel = new() { Left = 20, Top = 20, Text = text, Width = 340 };
                TextBox inputBox = new() { Left = 20, Top = 50, Width = 340 };
                Button confirmation = new() { Text = "OK", Left = 270, Width = 90, Top = 80 };
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
                tabData.Remove(tabControl1.SelectedTab.Text);
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
            else
            {
                MessageBox.Show("No tabs found to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAllTabsData()
        {
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            var dictToSave = new Dictionary<string, string>();

            foreach (var kvp in tabData)
            {
                var table = kvp.Value;

                if (string.IsNullOrEmpty(table.TableName))
                    table.TableName = kvp.Key;

                using (var sw = new StringWriter())
                {
                    table.WriteXml(sw, XmlWriteMode.WriteSchema);
                    dictToSave[kvp.Key] = sw.ToString();
                }
            }

            string allTabsJson = Newtonsoft.Json.JsonConvert.SerializeObject(dictToSave, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(SaveFilePath, allTabsJson);
        }

        private void LoadAllTabsData()
        {
            if (!File.Exists(SaveFilePath))
                return;

            string allTabsJson = File.ReadAllText(SaveFilePath);

            var dictFromFile = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(allTabsJson);

            if (dictFromFile == null) return;

            tabControl1.TabPages.Clear();
            tabData.Clear();

            foreach (var kvp in dictFromFile)
            {
                string tabName = kvp.Key;
                string tableXml = kvp.Value;

                DataTable table = new DataTable(tabName);

                using (var sr = new StringReader(tableXml))
                {
                    table.ReadXml(sr);
                }

                var tab = new TabPage(tabName);
                var dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    DataSource = table,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    Name = "dataGridView_" + tabName
                };

                tab.Controls.Add(dgv);
                tabControl1.TabPages.Add(tab);

                tabData[tabName] = table;

                SetColumnHeadersAndReadOnly(dgv);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await Task.Run(() => PriceUpdateTimer_Tick(null, EventArgs.Empty));
        }
    }
}
