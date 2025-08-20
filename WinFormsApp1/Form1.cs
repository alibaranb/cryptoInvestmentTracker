using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Globalization;
using System.Net.WebSockets;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Websocket.Client;


namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        // Store each tab's data
        private Dictionary<string, DataTable> tabData = new Dictionary<string, DataTable>();
        private WebsocketClient? _client;
        private ClientWebSocket _ws;
        private System.Windows.Forms.Timer priceUpdateTimer;


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            priceUpdateTimer = new System.Windows.Forms.Timer();
            priceUpdateTimer.Interval = 60_000;
            priceUpdateTimer.Tick += PriceUpdateTimer_Tick;
            priceUpdateTimer.Start();
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
                // 1. Tüm sekmelerden coin'leri topla
                HashSet<string> allCoins = new HashSet<string>();

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

                // 2. Binance REST API ile fiyatları al
                var coinPrices = await GetCurrentPricesAsync(allCoins.ToList());

                // 3. Tüm tablolarda fiyatları ve Profit/Loss değerlerini güncelle
                foreach (var table in tabData.Values)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string coin = row["Coin"]?.ToString()?.ToUpper();
                        if (string.IsNullOrWhiteSpace(coin)) continue;

                        if (coinPrices.TryGetValue(coin, out decimal currentPrice))
                        {
                            row["Current Price"] = currentPrice;

                            // Profit/Loss hesapla
                            if (decimal.TryParse(row["Buy Price ($)"]?.ToString(), out decimal buyPrice) &&
                                decimal.TryParse(row["Amount Bought"]?.ToString(), out decimal amountBought))
                            {
                                decimal profitLoss = (currentPrice - buyPrice) * amountBought;
                                row["Profit/Loss"] = profitLoss;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fiyat güncellenirken hata oluştu:\n" + ex.Message);
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

            DataTable table = new DataTable();
            table.Columns.Add("Coin");
            table.Columns.Add("Amount ($)", typeof(decimal));
            table.Columns.Add("Buy Price ($)", typeof(decimal));
            table.Columns.Add("Amount Bought", typeof(decimal));
            table.Columns.Add("Current Price", typeof(decimal));
            table.Columns.Add("Profit/Loss", typeof(decimal));

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

        private async void button2_Click(object sender, EventArgs e)
        {
            await Task.Run(() => PriceUpdateTimer_Tick(null, EventArgs.Empty));
        }
    }
}
