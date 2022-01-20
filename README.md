# VenusYield

["Venus Protocol"](https://venus.io) is a decentralized crypto-money market for Lending and Borrow/Mint, running on ["Binance Smart Chain"](https://www.binance.org/en/smartChain).

The aim of this "VenusYield" APP is to warn you through a Telegram message when your Supply/Borrow/Mint ratio is outside a given percentage in order to avoid a liquidation (currently a tough 10% of your underwater collaterals).

"VenusYield" is released as an open source software developed on Visual C# 16.9 (.NET Framework 4.8); Compile it for yourself or download the packed Windows 10 executable here ["VenusYield v0.6"](https://github.com/J1Mtonic/VenusYield/blob/main/Release/VenusYield.v0.6.zip) --> Is absolutely clean but "Don´t trust. [Verify](https://opentip.kaspersky.com)".

Now, let me show you how it looks like and how it works:

*Keep in mind that, this is an example balance and unfortunately not my real one...

<img src="https://github.com/J1Mtonic/VenusYield/blob/main/Pics/VenusYield.1.png" width="205" height="546">

"VenusYield" lists all supported assets on "Venus Protocol" and gets information from a given (Settings) BSC Public Wallet Address.
Note#1: Remember, never-ever share your private key or your mnemonic phrase!

Then it fetches information from different places:
 - Price vTokens: Binance API (USDT pairs)(real-time) --> Wanted to extract it from there to gain some reaction time against 'Venus Price Oracle'
 - Price StableCoins: Venus API
 - Supply/Borrow/Mint/Vault: Querying (Web3) Binance Smartchain with Nethereum .Net library and using the Binance free 'BSCendpoint'
 - Limit: Calculation is given by (TotalBorrowInUSD + BorrowVAI + VAIvault - (VAIvault - VAIminted)) / (TotalSupplyInUSD * Factor)

Factors:
- 80%
BTC, ETH, BNB, XVS, SXP, USDT, BUSD, USDC
- 60%
XRP, ADA, DOT, LTC, BCH, LINK, FIL, BETH, DAI
- 40%
DOGE
 
Let´s have an eye on 'Settings':
 
<img src="https://github.com/J1Mtonic/VenusYield/blob/main/Pics/VenusYield.2.png" width="319" height="195">

- BSC Public Address: Put Public BEP20 Wallet Address in here and remember Note#1.
- Borrow Reporting and Refresh Rate will give you some freedom for tuning alerts. Try to no overexpose yourself too much...

Finally, let´s cover Telegram reporting:
If you created a Telegram-Bot in advance it will be straightforward, in case you didn´t, here you have some basic info from their API:
- Install Telegram
- Add as a contact to Telegram 'BotFather' (check the verified symbol) 
- Create a Bot using the command '/newbot'
- Name it and copy the given "HTTP API" on the 'Telegram Bot' field (VenusYield APP)
- Click on the Link for your Bot given by 'BotFather' and write a Hi!
- Click on "Get ID" button (VenusYield APP) and a number will appear on the box

Information is 'autosaved' so just close that window. Config file (VenusYield.json) is stored locally in your computer, never shared but... 'Don´t trust. Verify'. 

After some seconds you will see your Venus Yield information!

Telegram Bot commands:
- Send a "/report" command to get real time 'Limit' and 'Balance' values
- More to be added soon...

If you have some doubts, or want to share your thoughts/requests, find me on [Venus Community](https://community.venus.io/c/uncategorized/1).

And remember:
- Never-ever share your private key or your mnemonic phrase!
- Don´t trust. Verify.
- #BUIDL, #HODL & #BTFD
- Donations are always very welcome! :)

CHANGES from v0.1 to v0.2
- fixed wrong calculation on Limit% when VAI in Vault is different from VAI Minted (thanks @YongeNoodle & @sosolean for reporting)
- fixed issue when updating settings during a balance update (thanks @mikibighead for reporting)

CHANGES from v0.2 to v0.3
- Added additional BSCendpoints to avoid a single point failure
- Improved Thread management

CHANGES from v0.3 to v0.4
- Added $ADA Cardano as per VIP-9
- Added "/report" command for Telegram Bot to get Limit and Balance values
- Delete manually C:\ProgramData\VenusYield.json data file before running V0.4

CHANGES from v0.4 to v0.5
- Added $DOGE Dogecoin as per VIP-17
- Added "/dominance" command for Telegram Bot to get Market Dominance for BTC.D, DeFi.D and USDT.D (fetched from Coingecko)
- Added "/sentiment" command for Telegram Bot to get Crypto Fear and Greed Index value (fetched from alternative.me/crypto)
- Added a compressed view by clicking on VenusYield logo
- Improved stability

CHANGES from v0.5 to v0.6
- Adjusted Factors from 60% to 80% as per VIP-22 on $BTC, $ETH, $BNB, $XVS, $SXP, $USDT, $BUSD, $USDC
- Adjusted Factor from 60% to 40% as per VIP-21 on $DOGE
- Fixed issue on Supply/Borrow vDOGE calculation
- Minor changes un VenusYield UI
