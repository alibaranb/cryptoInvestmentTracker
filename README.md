# Crypto Investment Tracker

A Windows Forms application for tracking your cryptocurrency investments with real-time price updates from Binance.

## Features

- 📊 **Multiple Portfolio Tabs** - Track different investment periods separately
- 💰 **Real-time Price Updates** - Automatic price updates every 30 seconds from Binance API
- 📈 **Profit/Loss Tracking** - See both individual coin and total portfolio performance
- 💾 **Auto-save** - Your data is automatically saved when you close the app
- 🗑️ **Easy Management** - Add/remove coins and tabs as needed

## How to Use

### Getting Started

1. **Launch the Application**
   - Run `cryptoInvestmentTracker.exe`
   - The app will load any previously saved data automatically

### Creating Your First Portfolio Tab

1. **Add a New Tab**
   - Click the **"Add Tab"** button
   - Enter a name for your portfolio (e.g., "January 2025", "Long Term", "Altcoins")
   - Click OK

2. **Add Your Investments**
   - Click on the new tab you created
   - In the grid, enter your investment details:
     - **Coin**: Enter the coin symbol (e.g., BTC, ETH, SOL, ADA)
     - **Invested ($)**: Amount you invested in USD
     - **Buy Price ($)**: The price you bought at
   - Press Enter to confirm each row

3. **Update Prices**
   - Click the **"Update Prices"** button to fetch current prices
   - Or wait 30 seconds for automatic updates
   - The app will automatically calculate:
     - **Amount**: How many coins you own
     - **Current Price**: Latest price from Binance
     - **Current Value**: What your coins are worth now
     - **Profit/Loss (%)**: Percentage gain/loss per coin

### Managing Your Portfolio

#### Adding More Coins
1. Go to any tab
2. Click on the empty row at the bottom
3. Enter the coin details (Coin, Invested, Buy Price)
4. The rest is calculated automatically

#### Removing a Coin
1. Click on the row number to select the entire row
2. Click the **"Remove Row"** button
3. The coin will be deleted from your portfolio

#### Deleting a Tab
1. Select the tab you want to delete
2. Click the **"Delete Tab"** button
3. The entire tab and all its data will be removed

### Understanding Your Dashboard

At the top of the window, you'll see three summary values:

- **Total Invested**: Total amount you've invested across all coins in the current tab
- **Total Current Value**: What all your coins are worth right now
- **Total Profit/Loss**: Dollar amount you've gained or lost (Current Value - Invested)

### Supported Cryptocurrencies

The app supports any cryptocurrency that trades on Binance with a USDT pair. Popular examples:

- Bitcoin (BTC)
- Ethereum (ETH)
- Binance Coin (BNB)
- Solana (SOL)
- Cardano (ADA)
- XRP (XRP)
- Dogecoin (DOGE)
- Polkadot (DOT)
- And many more!

**Note**: Enter the coin symbol exactly as it appears on Binance (usually uppercase, e.g., BTC not Bitcoin).

### Tips for Best Results

✅ **DO:**
- Use uppercase for coin symbols (BTC, ETH, SOL)
- Keep the app running for automatic price updates
- Create separate tabs for different investment strategies
- Update prices before making investment decisions

❌ **DON'T:**
- Don't use full coin names (use BTC, not Bitcoin)
- Don't manually edit calculated columns (Amount, Current Price, Current Value, Profit/Loss)
- Don't close the app during price updates

### Data Storage

- Your portfolio data is saved automatically when you close the app
- Data is stored in: `[App Directory]/save/tabsData.json`
- To backup your data, simply copy this file
- To restore data, place the backup file in the same location

### Troubleshooting

**Prices not updating?**
- Check your internet connection
- Make sure the coin symbol is correct (must match Binance)
- Try clicking "Update Prices" manually

**Coin not found?**
- Verify the coin trades on Binance
- Make sure you're using the correct symbol (e.g., BTC not BITCOIN)
- Check Binance.com to confirm the exact symbol

**App won't start?**
- Make sure you have .NET Framework installed
- Check if antivirus is blocking the app
- Try running as Administrator

## Requirements

- Windows 10 or later
- .NET Framework 4.7.2 or higher
- Internet connection for price updates

## Privacy & Security

- All data is stored locally on your computer
- No data is sent to external servers (except Binance API for prices)
- No login or account required
- Your investment information stays private

## Example Workflow

1. **Morning Check**: Open the app to see overnight price changes
2. **New Investment**: Add a new row with your coin, amount invested, and buy price
3. **Track Progress**: Watch real-time updates throughout the day
4. **Review Performance**: Check Total Profit/Loss to see how you're doing
5. **Organize**: Create new tabs for different months or strategies
6. **Close**: Simply close the app - everything saves automatically

## Support

If you encounter any issues or have suggestions, please open an issue on GitHub.

---

**Disclaimer**: This tool is for tracking purposes only. Always do your own research before making investment decisions. Cryptocurrency investments carry risk.
