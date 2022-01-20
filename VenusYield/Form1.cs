using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Nethereum.Contracts;
using Nethereum.Web3;
using System.IO;
using System.Globalization;
using System.Drawing;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;
using System.Linq;

namespace VenusYield
{
    public partial class Form1 : Form
    {
        public string BSCAddress { get; private set; }
        public string TelegramBotAPI { get; private set; }
        public uint TelegramChatID { get; private set; }
        public uint BorrowUnder { get; private set; }
        public uint BorrowOver { get; private set; }
        public uint RefreshMins { get; private set; }
        public double Balance { get; private set; }
        public double BorrowLimit { get; private set; }        

        readonly string VenusAPIvToken = "https://api.venus.io/api/vtoken";
        readonly string BinanceAPIticker = "https://api.binance.com/api/v3/ticker/24hr?symbol=";
        readonly string[] BSCendpoints = { "https://bsc-dataseed.binance.org", "https://bsc-dataseed1.binance.org", "https://bsc-dataseed2.binance.org", "https://bsc-dataseed3.binance.org", "https://bsc-dataseed4.binance.org" };
        readonly string urlCoinGeckoGlobal = "https://api.coingecko.com/api/v3/global";
        readonly string urlCoinGeckoDefi = "https://api.coingecko.com/api/v3/global/decentralized_finance_defi";
        readonly string urlFandG = "https://api.alternative.me/fng/";

        readonly string BSCvXVScontract = "0x151b1e2635a717bcdc836ecd6fbb62b674fe3e1d";
        readonly string BSCvSXPcontract = "0x2fF3d0F6990a40261c66E1ff2017aCBc282EB6d0";
        readonly string BSCvUSDCcontract = "0xecA88125a5ADbe82614ffC12D0DB554E2e2867C8";
        readonly string BSCvUSDTcontract = "0xfD5840Cd36d94D7229439859C0112a4185BC0255";
        readonly string BSCvBUSDcontract = "0x95c78222B3D6e262426483D42CfA53685A67Ab9D";
        readonly string BSCvBNBcontract = "0xA07c5b74C9B40447a954e1466938b865b6BBea36";
        readonly string BSCvBTCcontract = "0x882C173bC7Ff3b7786CA16dfeD3DFFfb9Ee7847B";
        readonly string BSCvETHcontract = "0xf508fCD89b8bd15579dc79A6827cB4686A3592c8";
        readonly string BSCvLTCcontract = "0x57A5297F2cB2c0AaC9D554660acd6D385Ab50c6B";
        readonly string BSCvXRPcontract = "0xB248a295732e0225acd3337607cc01068e3b9c10";
        readonly string BSCvBCHcontract = "0x5F0388EBc2B94FA8E123F404b79cCF5f40b29176";
        readonly string BSCvDOTcontract = "0x1610bc33319e9398de5f57b33a5b184c806ad217";
        readonly string BSCvLINKcontract = "0x650b940a1033B8A1b1873f78730FcFC73ec11f1f";
        readonly string BSCvBETHcontract = "0x972207A639CC1B374B893cc33Fa251b55CEB7c07";
        readonly string BSCvDAIcontract = "0x334b3eCB4DCa3593BCCC3c7EBD1A1C1d1780FBF1";
        readonly string BSCvFILcontract = "0xf91d58b5aE142DAcC749f58A49FCBac340Cb0343";
        readonly string BSCvADAcontract = "0x9A0AF7FDb2065Ce470D72664DE73cAE409dA28Ec";
        readonly string BSCvDOGEcontract = "0xec3422ef92b2fb59e84c8b02ba73f1fe84ed8d71";
        readonly string BSCVAIcontract = "0x4bd17003473389a42daf6a0a729f6fdb328bbbd7";
        readonly string BSCVAIvaultcontract = "0x0667Eed0a0aAb930af74a3dfeDD263A73994f216";
        readonly string BSCUnitrollercontract = "0xfD36E2c2a6789Db23113685031d7F16329158384";

        readonly string ABIvTokens = @"[{'inputs':[{'internalType':'address','name':'underlying_','type':'address'},{'internalType':'contract ComptrollerInterface','name':'comptroller_','type':'address'},{'internalType':'contract InterestRateModel','name':'interestRateModel_','type':'address'},{'internalType':'uint256','name':'initialExchangeRateMantissa_','type':'uint256'},{'internalType':'string','name':'name_','type':'string'},{'internalType':'string','name':'symbol_','type':'string'},{'internalType':'uint8','name':'decimals_','type':'uint8'},{'internalType':'address payable','name':'admin_','type':'address'},{'internalType':'address','name':'implementation_','type':'address'},{'internalType':'bytes','name':'becomeImplementationData','type':'bytes'}],'payable':false,'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'uint256','name':'cashPrior','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'interestAccumulated','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'borrowIndex','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'totalBorrows','type':'uint256'}],'name':'AccrueInterest','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'owner','type':'address'},{'indexed':true,'internalType':'address','name':'spender','type':'address'},{'indexed':false,'internalType':'uint256','name':'amount','type':'uint256'}],'name':'Approval','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'borrower','type':'address'},{'indexed':false,'internalType':'uint256','name':'borrowAmount','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'accountBorrows','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'totalBorrows','type':'uint256'}],'name':'Borrow','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'uint256','name':'error','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'info','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'detail','type':'uint256'}],'name':'Failure','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'liquidator','type':'address'},{'indexed':false,'internalType':'address','name':'borrower','type':'address'},{'indexed':false,'internalType':'uint256','name':'repayAmount','type':'uint256'},{'indexed':false,'internalType':'address','name':'vTokenCollateral','type':'address'},{'indexed':false,'internalType':'uint256','name':'seizeTokens','type':'uint256'}],'name':'LiquidateBorrow','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'minter','type':'address'},{'indexed':false,'internalType':'uint256','name':'mintAmount','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'mintTokens','type':'uint256'}],'name':'Mint','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'oldAdmin','type':'address'},{'indexed':false,'internalType':'address','name':'newAdmin','type':'address'}],'name':'NewAdmin','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'contract ComptrollerInterface','name':'oldComptroller','type':'address'},{'indexed':false,'internalType':'contract ComptrollerInterface','name':'newComptroller','type':'address'}],'name':'NewComptroller','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'oldImplementation','type':'address'},{'indexed':false,'internalType':'address','name':'newImplementation','type':'address'}],'name':'NewImplementation','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'contract InterestRateModel','name':'oldInterestRateModel','type':'address'},{'indexed':false,'internalType':'contract InterestRateModel','name':'newInterestRateModel','type':'address'}],'name':'NewMarketInterestRateModel','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'oldPendingAdmin','type':'address'},{'indexed':false,'internalType':'address','name':'newPendingAdmin','type':'address'}],'name':'NewPendingAdmin','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'uint256','name':'oldReserveFactorMantissa','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'newReserveFactorMantissa','type':'uint256'}],'name':'NewReserveFactor','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'redeemer','type':'address'},{'indexed':false,'internalType':'uint256','name':'redeemAmount','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'redeemTokens','type':'uint256'}],'name':'Redeem','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'payer','type':'address'},{'indexed':false,'internalType':'address','name':'borrower','type':'address'},{'indexed':false,'internalType':'uint256','name':'repayAmount','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'accountBorrows','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'totalBorrows','type':'uint256'}],'name':'RepayBorrow','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'benefactor','type':'address'},{'indexed':false,'internalType':'uint256','name':'addAmount','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'newTotalReserves','type':'uint256'}],'name':'ReservesAdded','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'admin','type':'address'},{'indexed':false,'internalType':'uint256','name':'reduceAmount','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'newTotalReserves','type':'uint256'}],'name':'ReservesReduced','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'from','type':'address'},{'indexed':true,'internalType':'address','name':'to','type':'address'},{'indexed':false,'internalType':'uint256','name':'amount','type':'uint256'}],'name':'Transfer','type':'event'},{'payable':true,'stateMutability':'payable','type':'fallback'},{'constant':false,'inputs':[],'name':'_acceptAdmin','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'addAmount','type':'uint256'}],'name':'_addReserves','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'reduceAmount','type':'uint256'}],'name':'_reduceReserves','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'contract ComptrollerInterface','name':'newComptroller','type':'address'}],'name':'_setComptroller','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'implementation_','type':'address'},{'internalType':'bool','name':'allowResign','type':'bool'},{'internalType':'bytes','name':'becomeImplementationData','type':'bytes'}],'name':'_setImplementation','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'contract InterestRateModel','name':'newInterestRateModel','type':'address'}],'name':'_setInterestRateModel','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address payable','name':'newPendingAdmin','type':'address'}],'name':'_setPendingAdmin','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'newReserveFactorMantissa','type':'uint256'}],'name':'_setReserveFactor','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'accrualBlockNumber','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[],'name':'accrueInterest','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'admin','outputs':[{'internalType':'address payable','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'owner','type':'address'},{'internalType':'address','name':'spender','type':'address'}],'name':'allowance','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'spender','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'}],'name':'approve','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'owner','type':'address'}],'name':'balanceOf','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'owner','type':'address'}],'name':'balanceOfUnderlying','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'borrowAmount','type':'uint256'}],'name':'borrow','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'borrowBalanceCurrent','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'borrowBalanceStored','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'borrowIndex','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'borrowRatePerBlock','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'comptroller','outputs':[{'internalType':'contract ComptrollerInterface','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'decimals','outputs':[{'internalType':'uint8','name':'','type':'uint8'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'bytes','name':'data','type':'bytes'}],'name':'delegateToImplementation','outputs':[{'internalType':'bytes','name':'','type':'bytes'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'internalType':'bytes','name':'data','type':'bytes'}],'name':'delegateToViewImplementation','outputs':[{'internalType':'bytes','name':'','type':'bytes'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[],'name':'exchangeRateCurrent','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'exchangeRateStored','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'getAccountSnapshot','outputs':[{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'getCash','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'implementation','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'interestRateModel','outputs':[{'internalType':'contract InterestRateModel','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'isVToken','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'borrower','type':'address'},{'internalType':'uint256','name':'repayAmount','type':'uint256'},{'internalType':'contract VTokenInterface','name':'vTokenCollateral','type':'address'}],'name':'liquidateBorrow','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'mintAmount','type':'uint256'}],'name':'mint','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'name','outputs':[{'internalType':'string','name':'','type':'string'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'pendingAdmin','outputs':[{'internalType':'address payable','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'redeemTokens','type':'uint256'}],'name':'redeem','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'redeemAmount','type':'uint256'}],'name':'redeemUnderlying','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'repayAmount','type':'uint256'}],'name':'repayBorrow','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'borrower','type':'address'},{'internalType':'uint256','name':'repayAmount','type':'uint256'}],'name':'repayBorrowBehalf','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'reserveFactorMantissa','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'liquidator','type':'address'},{'internalType':'address','name':'borrower','type':'address'},{'internalType':'uint256','name':'seizeTokens','type':'uint256'}],'name':'seize','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'supplyRatePerBlock','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'symbol','outputs':[{'internalType':'string','name':'','type':'string'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'totalBorrows','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[],'name':'totalBorrowsCurrent','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'totalReserves','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'totalSupply','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'dst','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'}],'name':'transfer','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'src','type':'address'},{'internalType':'address','name':'dst','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'}],'name':'transferFrom','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'underlying','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'}]";
        readonly string ABIVAItoken = @"[{'inputs':[{'internalType':'uint256','name':'chainId_','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'src','type':'address'},{'indexed':true,'internalType':'address','name':'guy','type':'address'},{'indexed':false,'internalType':'uint256','name':'wad','type':'uint256'}],'name':'Approval','type':'event'},{'anonymous':true,'inputs':[{'indexed':true,'internalType':'bytes4','name':'sig','type':'bytes4'},{'indexed':true,'internalType':'address','name':'usr','type':'address'},{'indexed':true,'internalType':'bytes32','name':'arg1','type':'bytes32'},{'indexed':true,'internalType':'bytes32','name':'arg2','type':'bytes32'},{'indexed':false,'internalType':'bytes','name':'data','type':'bytes'}],'name':'LogNote','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'src','type':'address'},{'indexed':true,'internalType':'address','name':'dst','type':'address'},{'indexed':false,'internalType':'uint256','name':'wad','type':'uint256'}],'name':'Transfer','type':'event'},{'constant':true,'inputs':[],'name':'DOMAIN_SEPARATOR','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'PERMIT_TYPEHASH','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'},{'internalType':'address','name':'','type':'address'}],'name':'allowance','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'usr','type':'address'},{'internalType':'uint256','name':'wad','type':'uint256'}],'name':'approve','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'}],'name':'balanceOf','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'usr','type':'address'},{'internalType':'uint256','name':'wad','type':'uint256'}],'name':'burn','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'decimals','outputs':[{'internalType':'uint8','name':'','type':'uint8'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'guy','type':'address'}],'name':'deny','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'usr','type':'address'},{'internalType':'uint256','name':'wad','type':'uint256'}],'name':'mint','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'src','type':'address'},{'internalType':'address','name':'dst','type':'address'},{'internalType':'uint256','name':'wad','type':'uint256'}],'name':'move','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'name','outputs':[{'internalType':'string','name':'','type':'string'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'}],'name':'nonces','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'holder','type':'address'},{'internalType':'address','name':'spender','type':'address'},{'internalType':'uint256','name':'nonce','type':'uint256'},{'internalType':'uint256','name':'expiry','type':'uint256'},{'internalType':'bool','name':'allowed','type':'bool'},{'internalType':'uint8','name':'v','type':'uint8'},{'internalType':'bytes32','name':'r','type':'bytes32'},{'internalType':'bytes32','name':'s','type':'bytes32'}],'name':'permit','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'usr','type':'address'},{'internalType':'uint256','name':'wad','type':'uint256'}],'name':'pull','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'usr','type':'address'},{'internalType':'uint256','name':'wad','type':'uint256'}],'name':'push','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'guy','type':'address'}],'name':'rely','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'symbol','outputs':[{'internalType':'string','name':'','type':'string'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'totalSupply','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'dst','type':'address'},{'internalType':'uint256','name':'wad','type':'uint256'}],'name':'transfer','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'src','type':'address'},{'internalType':'address','name':'dst','type':'address'},{'internalType':'uint256','name':'wad','type':'uint256'}],'name':'transferFrom','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'version','outputs':[{'internalType':'string','name':'','type':'string'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'}],'name':'wards','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'}]";
        readonly string ABIVAIvault = @"[{'inputs':[],'payable':false,'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'oldAdmin','type':'address'},{'indexed':true,'internalType':'address','name':'newAdmin','type':'address'}],'name':'AdminTransfered','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'user','type':'address'},{'indexed':false,'internalType':'uint256','name':'amount','type':'uint256'}],'name':'Deposit','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'user','type':'address'},{'indexed':false,'internalType':'uint256','name':'amount','type':'uint256'}],'name':'Withdraw','type':'event'},{'constant':false,'inputs':[{'internalType':'contract VAIVaultProxy','name':'vaiVaultProxy','type':'address'}],'name':'_become','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'accXVSPerShare','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'admin','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[],'name':'burnAdmin','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[],'name':'claim','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'_amount','type':'uint256'}],'name':'deposit','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'getAdmin','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'pendingAdmin','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'pendingRewards','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'pendingVAIVaultImplementation','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'_user','type':'address'}],'name':'pendingXVS','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'newAdmin','type':'address'}],'name':'setNewAdmin','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'_xvs','type':'address'},{'internalType':'address','name':'_vai','type':'address'}],'name':'setVenusInfo','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[],'name':'updatePendingRewards','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'}],'name':'userInfo','outputs':[{'internalType':'uint256','name':'amount','type':'uint256'},{'internalType':'uint256','name':'rewardDebt','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'vai','outputs':[{'internalType':'contract IBEP20','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'vaiVaultImplementation','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'_amount','type':'uint256'}],'name':'withdraw','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'xvs','outputs':[{'internalType':'contract IBEP20','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'xvsBalance','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'}]";
        readonly string ABIUnitroller = @"[{'inputs':[],'payable':false,'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'string','name':'action','type':'string'},{'indexed':false,'internalType':'bool','name':'pauseState','type':'bool'}],'name':'ActionPaused','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'contract VToken','name':'vToken','type':'address'},{'indexed':false,'internalType':'string','name':'action','type':'string'},{'indexed':false,'internalType':'bool','name':'pauseState','type':'bool'}],'name':'ActionPaused','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'bool','name':'state','type':'bool'}],'name':'ActionProtocolPaused','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'contract VToken','name':'vToken','type':'address'},{'indexed':true,'internalType':'address','name':'borrower','type':'address'},{'indexed':false,'internalType':'uint256','name':'venusDelta','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'venusBorrowIndex','type':'uint256'}],'name':'DistributedBorrowerVenus','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'contract VToken','name':'vToken','type':'address'},{'indexed':true,'internalType':'address','name':'supplier','type':'address'},{'indexed':false,'internalType':'uint256','name':'venusDelta','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'venusSupplyIndex','type':'uint256'}],'name':'DistributedSupplierVenus','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'vaiMinter','type':'address'},{'indexed':false,'internalType':'uint256','name':'venusDelta','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'venusVAIMintIndex','type':'uint256'}],'name':'DistributedVAIMinterVenus','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'uint256','name':'amount','type':'uint256'}],'name':'DistributedVAIVaultVenus','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'uint256','name':'error','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'info','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'detail','type':'uint256'}],'name':'Failure','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'contract VToken','name':'vToken','type':'address'},{'indexed':false,'internalType':'address','name':'account','type':'address'}],'name':'MarketEntered','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'contract VToken','name':'vToken','type':'address'},{'indexed':false,'internalType':'address','name':'account','type':'address'}],'name':'MarketExited','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'contract VToken','name':'vToken','type':'address'}],'name':'MarketListed','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'contract VToken','name':'vToken','type':'address'},{'indexed':false,'internalType':'uint256','name':'newBorrowCap','type':'uint256'}],'name':'NewBorrowCap','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'oldBorrowCapGuardian','type':'address'},{'indexed':false,'internalType':'address','name':'newBorrowCapGuardian','type':'address'}],'name':'NewBorrowCapGuardian','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'uint256','name':'oldCloseFactorMantissa','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'newCloseFactorMantissa','type':'uint256'}],'name':'NewCloseFactor','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'contract VToken','name':'vToken','type':'address'},{'indexed':false,'internalType':'uint256','name':'oldCollateralFactorMantissa','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'newCollateralFactorMantissa','type':'uint256'}],'name':'NewCollateralFactor','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'uint256','name':'oldLiquidationIncentiveMantissa','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'newLiquidationIncentiveMantissa','type':'uint256'}],'name':'NewLiquidationIncentive','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'uint256','name':'oldMaxAssets','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'newMaxAssets','type':'uint256'}],'name':'NewMaxAssets','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'oldPauseGuardian','type':'address'},{'indexed':false,'internalType':'address','name':'newPauseGuardian','type':'address'}],'name':'NewPauseGuardian','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'contract PriceOracle','name':'oldPriceOracle','type':'address'},{'indexed':false,'internalType':'contract PriceOracle','name':'newPriceOracle','type':'address'}],'name':'NewPriceOracle','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'contract VAIControllerInterface','name':'oldVAIController','type':'address'},{'indexed':false,'internalType':'contract VAIControllerInterface','name':'newVAIController','type':'address'}],'name':'NewVAIController','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'uint256','name':'oldVAIMintRate','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'newVAIMintRate','type':'uint256'}],'name':'NewVAIMintRate','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'vault_','type':'address'},{'indexed':false,'internalType':'uint256','name':'releaseStartBlock_','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'releaseInterval_','type':'uint256'}],'name':'NewVAIVaultInfo','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'uint256','name':'oldVenusVAIRate','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'newVenusVAIRate','type':'uint256'}],'name':'NewVenusVAIRate','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'uint256','name':'oldVenusVAIVaultRate','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'newVenusVAIVaultRate','type':'uint256'}],'name':'NewVenusVAIVaultRate','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'contract VToken','name':'vToken','type':'address'},{'indexed':false,'internalType':'uint256','name':'newSpeed','type':'uint256'}],'name':'VenusSpeedUpdated','type':'event'},{'constant':false,'inputs':[{'internalType':'contract Unitroller','name':'unitroller','type':'address'}],'name':'_become','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'_borrowGuardianPaused','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'_mintGuardianPaused','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'newBorrowCapGuardian','type':'address'}],'name':'_setBorrowCapGuardian','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'newCloseFactorMantissa','type':'uint256'}],'name':'_setCloseFactor','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'contract VToken','name':'vToken','type':'address'},{'internalType':'uint256','name':'newCollateralFactorMantissa','type':'uint256'}],'name':'_setCollateralFactor','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'newLiquidationIncentiveMantissa','type':'uint256'}],'name':'_setLiquidationIncentive','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'contract VToken[]','name':'vTokens','type':'address[]'},{'internalType':'uint256[]','name':'newBorrowCaps','type':'uint256[]'}],'name':'_setMarketBorrowCaps','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'newMaxAssets','type':'uint256'}],'name':'_setMaxAssets','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'contract PriceOracle','name':'newOracle','type':'address'}],'name':'_setPriceOracle','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'bool','name':'state','type':'bool'}],'name':'_setProtocolPaused','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'contract VAIControllerInterface','name':'vaiController_','type':'address'}],'name':'_setVAIController','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'newVAIMintRate','type':'uint256'}],'name':'_setVAIMintRate','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vault_','type':'address'},{'internalType':'uint256','name':'releaseStartBlock_','type':'uint256'},{'internalType':'uint256','name':'minReleaseAmount_','type':'uint256'}],'name':'_setVAIVaultInfo','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'contract VToken','name':'vToken','type':'address'},{'internalType':'uint256','name':'venusSpeed','type':'uint256'}],'name':'_setVenusSpeed','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'venusVAIRate_','type':'uint256'}],'name':'_setVenusVAIRate','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'uint256','name':'venusVAIVaultRate_','type':'uint256'}],'name':'_setVenusVAIVaultRate','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'contract VToken','name':'vToken','type':'address'}],'name':'_supportMarket','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'},{'internalType':'uint256','name':'','type':'uint256'}],'name':'accountAssets','outputs':[{'internalType':'contract VToken','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'admin','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'uint256','name':'','type':'uint256'}],'name':'allMarkets','outputs':[{'internalType':'contract VToken','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vToken','type':'address'},{'internalType':'address','name':'borrower','type':'address'},{'internalType':'uint256','name':'borrowAmount','type':'uint256'}],'name':'borrowAllowed','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'borrowCapGuardian','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'}],'name':'borrowCaps','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'}],'name':'borrowGuardianPaused','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vToken','type':'address'},{'internalType':'address','name':'borrower','type':'address'},{'internalType':'uint256','name':'borrowAmount','type':'uint256'}],'name':'borrowVerify','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'account','type':'address'},{'internalType':'contract VToken','name':'vToken','type':'address'}],'name':'checkMembership','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'holder','type':'address'},{'internalType':'contract VToken[]','name':'vTokens','type':'address[]'}],'name':'claimVenus','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'holder','type':'address'}],'name':'claimVenus','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address[]','name':'holders','type':'address[]'},{'internalType':'contract VToken[]','name':'vTokens','type':'address[]'},{'internalType':'bool','name':'borrowers','type':'bool'},{'internalType':'bool','name':'suppliers','type':'bool'}],'name':'claimVenus','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'closeFactorMantissa','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'comptrollerImplementation','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vaiMinter','type':'address'},{'internalType':'bool','name':'distributeAll','type':'bool'}],'name':'distributeVAIMinterVenus','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address[]','name':'vTokens','type':'address[]'}],'name':'enterMarkets','outputs':[{'internalType':'uint256[]','name':'','type':'uint256[]'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vTokenAddress','type':'address'}],'name':'exitMarket','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'getAccountLiquidity','outputs':[{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'getAllMarkets','outputs':[{'internalType':'contract VToken[]','name':'','type':'address[]'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'getAssetsIn','outputs':[{'internalType':'contract VToken[]','name':'','type':'address[]'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'getBlockNumber','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'account','type':'address'},{'internalType':'address','name':'vTokenModify','type':'address'},{'internalType':'uint256','name':'redeemTokens','type':'uint256'},{'internalType':'uint256','name':'borrowAmount','type':'uint256'}],'name':'getHypotheticalAccountLiquidity','outputs':[{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'getXVSAddress','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'isComptroller','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vTokenBorrowed','type':'address'},{'internalType':'address','name':'vTokenCollateral','type':'address'},{'internalType':'address','name':'liquidator','type':'address'},{'internalType':'address','name':'borrower','type':'address'},{'internalType':'uint256','name':'repayAmount','type':'uint256'}],'name':'liquidateBorrowAllowed','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vTokenBorrowed','type':'address'},{'internalType':'address','name':'vTokenCollateral','type':'address'},{'internalType':'address','name':'liquidator','type':'address'},{'internalType':'address','name':'borrower','type':'address'},{'internalType':'uint256','name':'actualRepayAmount','type':'uint256'},{'internalType':'uint256','name':'seizeTokens','type':'uint256'}],'name':'liquidateBorrowVerify','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'vTokenBorrowed','type':'address'},{'internalType':'address','name':'vTokenCollateral','type':'address'},{'internalType':'uint256','name':'actualRepayAmount','type':'uint256'}],'name':'liquidateCalculateSeizeTokens','outputs':[{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'liquidationIncentiveMantissa','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'}],'name':'markets','outputs':[{'internalType':'bool','name':'isListed','type':'bool'},{'internalType':'uint256','name':'collateralFactorMantissa','type':'uint256'},{'internalType':'bool','name':'isVenus','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'maxAssets','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'minReleaseAmount','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vToken','type':'address'},{'internalType':'address','name':'minter','type':'address'},{'internalType':'uint256','name':'mintAmount','type':'uint256'}],'name':'mintAllowed','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'}],'name':'mintGuardianPaused','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'mintVAIGuardianPaused','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vToken','type':'address'},{'internalType':'address','name':'minter','type':'address'},{'internalType':'uint256','name':'actualMintAmount','type':'uint256'},{'internalType':'uint256','name':'mintTokens','type':'uint256'}],'name':'mintVerify','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'}],'name':'mintedVAIs','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'oracle','outputs':[{'internalType':'contract PriceOracle','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'pauseGuardian','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'pendingAdmin','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'pendingComptrollerImplementation','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'protocolPaused','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vToken','type':'address'},{'internalType':'address','name':'redeemer','type':'address'},{'internalType':'uint256','name':'redeemTokens','type':'uint256'}],'name':'redeemAllowed','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vToken','type':'address'},{'internalType':'address','name':'redeemer','type':'address'},{'internalType':'uint256','name':'redeemAmount','type':'uint256'},{'internalType':'uint256','name':'redeemTokens','type':'uint256'}],'name':'redeemVerify','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'releaseStartBlock','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[],'name':'releaseToVault','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vToken','type':'address'},{'internalType':'address','name':'payer','type':'address'},{'internalType':'address','name':'borrower','type':'address'},{'internalType':'uint256','name':'repayAmount','type':'uint256'}],'name':'repayBorrowAllowed','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vToken','type':'address'},{'internalType':'address','name':'payer','type':'address'},{'internalType':'address','name':'borrower','type':'address'},{'internalType':'uint256','name':'actualRepayAmount','type':'uint256'},{'internalType':'uint256','name':'borrowerIndex','type':'uint256'}],'name':'repayBorrowVerify','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'repayVAIGuardianPaused','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vTokenCollateral','type':'address'},{'internalType':'address','name':'vTokenBorrowed','type':'address'},{'internalType':'address','name':'liquidator','type':'address'},{'internalType':'address','name':'borrower','type':'address'},{'internalType':'uint256','name':'seizeTokens','type':'uint256'}],'name':'seizeAllowed','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'seizeGuardianPaused','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vTokenCollateral','type':'address'},{'internalType':'address','name':'vTokenBorrowed','type':'address'},{'internalType':'address','name':'liquidator','type':'address'},{'internalType':'address','name':'borrower','type':'address'},{'internalType':'uint256','name':'seizeTokens','type':'uint256'}],'name':'seizeVerify','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'owner','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'}],'name':'setMintedVAIOf','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vToken','type':'address'},{'internalType':'address','name':'src','type':'address'},{'internalType':'address','name':'dst','type':'address'},{'internalType':'uint256','name':'transferTokens','type':'uint256'}],'name':'transferAllowed','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'transferGuardianPaused','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'internalType':'address','name':'vToken','type':'address'},{'internalType':'address','name':'src','type':'address'},{'internalType':'address','name':'dst','type':'address'},{'internalType':'uint256','name':'transferTokens','type':'uint256'}],'name':'transferVerify','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'vaiController','outputs':[{'internalType':'contract VAIControllerInterface','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'vaiMintRate','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'vaiVaultAddress','outputs':[{'internalType':'address','name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'}],'name':'venusAccrued','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'}],'name':'venusBorrowState','outputs':[{'internalType':'uint224','name':'index','type':'uint224'},{'internalType':'uint32','name':'block','type':'uint32'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'},{'internalType':'address','name':'','type':'address'}],'name':'venusBorrowerIndex','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'venusInitialIndex','outputs':[{'internalType':'uint224','name':'','type':'uint224'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'venusRate','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'}],'name':'venusSpeeds','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'},{'internalType':'address','name':'','type':'address'}],'name':'venusSupplierIndex','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'internalType':'address','name':'','type':'address'}],'name':'venusSupplyState','outputs':[{'internalType':'uint224','name':'index','type':'uint224'},{'internalType':'uint32','name':'block','type':'uint32'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'venusVAIRate','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'venusVAIVaultRate','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'}]";
        readonly List<string> StableCoins = new List<string> { "VAI", "USDT", "USDC", "DAI", "BUSD" };

        private static TelegramBotClient botClient;
        private bool wFullSize = true; 

        public Form1()
        {
            InitializeComponent();
            bool settingsLoaded = ReadSettings();
            if (settingsLoaded)
            {
                RunActivities();
            }
            else
            {
                var newFrm = new Form2();
                newFrm.Closed += delegate
                {
                    ReadSettings();
                    RunActivities();
                };
                newFrm.Show(this);
            }
        }

        public bool ReadSettings()
        {
            var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            try
            {
                using (StreamReader file = System.IO.File.OpenText(systemPath + @"\VenusYield.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    Settings mySettings = (Settings)serializer.Deserialize(file, typeof(Settings));

                    BSCAddress = mySettings.BSCaddress;
                    TelegramBotAPI = mySettings.TelegramBotAPI;
                    TelegramChatID = mySettings.TelegramChatID;
                    BorrowUnder = mySettings.BorrowUnder;
                    BorrowOver = mySettings.BorrowOver;
                    RefreshMins = mySettings.RefreshRate;
                }
                return true;
            }
            catch (Exception ex) { Console.WriteLine("#2 {0}", ex.Message.ToString()); return false; }
        }

        public void RunActivities()
        {
            bgwVenusYield.RunWorkerAsync();
            if (TelegramBotAPI != "")
            {
                botClient = new TelegramBotClient(TelegramBotAPI);
                botClient.OnMessage += BotTelegramReadMsg;
                botClient.OnReceiveError += BotTelegramError;
                botClient.StartReceiving(Array.Empty<UpdateType>());
            }
        }

        private void LVenusYield_Click(object sender, EventArgs e)
        {
            if (wFullSize)
            {
                this.Size = new Size(435, 170);
                wFullSize = false;
            }
            else
            {
                this.Size = new Size(435, 1100);
                wFullSize = true;
            }
        }

        public async Task<VTokenBalances> TokenBalances(string Symbol, string BSCcontract, VenusAPIvTokenService vTokenService)
        {
            Random rand = new Random();
            int index = rand.Next(BSCendpoints.Length);
            string BSCendpoint = BSCendpoints[index];
            Web3 web3 = new Web3(BSCendpoint);
            VTokenBalances tokenBalances = new VTokenBalances();
            BinancePriceChange binancePriceChange = new BinancePriceChange();
            WebClient webclient = new WebClient();
            double Price = 0;
            double Mantissa = 1E+18;
            if (Symbol == "BETH") Symbol = "ETH";
            if (Symbol == "DOGE") Mantissa = 1E+13;

            try
            {
                if (StableCoins.Contains(Symbol))
                    Price = double.Parse(vTokenService.Data.Markets[vTokenService.Data.Markets.FindIndex(j => j.Symbol.Equals("v" + Symbol))].TokenPrice, CultureInfo.InvariantCulture);
                else
                {
                    binancePriceChange = BinancePriceChange.FromJson(webclient.DownloadString(BinanceAPIticker + Symbol + "USDT"));
                    Price = double.Parse(binancePriceChange.LastPrice, CultureInfo.InvariantCulture);
                }
                Contract vContract = web3.Eth.GetContract(ABIvTokens, BSCcontract);
                Function vSupplyFunction = vContract.GetFunction("balanceOf");
                dynamic vSupplyResult = await vSupplyFunction.CallAsync<dynamic>(BSCAddress);
                Function vBorrowFunction = vContract.GetFunction("borrowBalanceStored");
                dynamic vBorrowResult = await vBorrowFunction.CallAsync<dynamic>(BSCAddress);
                Function vExchangeRateFunction = vContract.GetFunction("exchangeRateStored");
                dynamic vExchangeRateResult = await vExchangeRateFunction.CallAsync<dynamic>();
                tokenBalances.Supply = (double)vSupplyResult * (double)vExchangeRateResult / (Mantissa * Mantissa);
                tokenBalances.Borrow = (double)vBorrowResult / Mantissa;
                tokenBalances.PriceUSD = Price;
                tokenBalances.SupplyUSD = tokenBalances.PriceUSD * tokenBalances.Supply;
                tokenBalances.BorrowUSD = tokenBalances.PriceUSD * tokenBalances.Borrow;
            }
            catch (Exception ex) { Console.WriteLine("#0 {0}", ex.Message.ToString()); bgwVenusYield.CancelAsync(); }

            return await Task.FromResult<VTokenBalances>(tokenBalances);
        }

        private async void BgwVenusYield_DoWorkAsync(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            VTokenBalances mytokenBalances = new VTokenBalances();
            VenusAPIvTokenService vTokenService = new VenusAPIvTokenService();
            WebClient webclient = new WebClient();
            Random rand = new Random();
            int index = rand.Next(BSCendpoints.Length);
            string BSCendpoint = BSCendpoints[index];
            Web3 web3 = new Web3(BSCendpoint);

            while (true)
            {
                try
                {
                    double TotalSupplyUSD = 0.0;
                    double TotalBorrowUSD = 0.0;
                    double TotalLimitUSD = 0.0;
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    vTokenService = VenusAPIvTokenService.FromJson(webclient.DownloadString(VenusAPIvToken));
                    mytokenBalances = await TokenBalances("XVS", BSCvXVScontract, null);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.8;
                    if (lPriceXVS.InvokeRequired)
                        lPriceXVS.Invoke(new MethodInvoker(delegate { lPriceXVS.Text = "$" + mytokenBalances.PriceUSD.ToString("N2", CultureInfo.InvariantCulture); }));
                    if (lSupplyXVS.InvokeRequired)
                        lSupplyXVS.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyXVS.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " XVS"; else lSupplyXVS.Text = "---"; }));
                    if (lBorrowXVS.InvokeRequired)
                        lBorrowXVS.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowXVS.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " XVS"; else lBorrowXVS.Text = "---"; }));
                    mytokenBalances = await TokenBalances("BTC", BSCvBTCcontract, null);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.8;
                    if (lPriceBTC.InvokeRequired)
                        lPriceBTC.Invoke(new MethodInvoker(delegate { lPriceBTC.Text = "$" + mytokenBalances.PriceUSD.ToString("N0", CultureInfo.InvariantCulture); }));
                    if (lSupplyBTC.InvokeRequired)
                        lSupplyBTC.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyBTC.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " BTC"; else lSupplyBTC.Text = "---"; }));
                    if (lBorrowBTC.InvokeRequired)
                        lBorrowBTC.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowBTC.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " BTC"; else lBorrowBTC.Text = "---"; }));
                    mytokenBalances = await TokenBalances("ETH", BSCvETHcontract, null);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.8;
                    if (lPriceETH.InvokeRequired)
                        lPriceETH.Invoke(new MethodInvoker(delegate { lPriceETH.Text = "$" + mytokenBalances.PriceUSD.ToString("N0", CultureInfo.InvariantCulture); }));
                    if (lSupplyETH.InvokeRequired)
                        lSupplyETH.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyETH.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " ETH"; else lSupplyETH.Text = "---"; }));
                    if (lBorrowETH.InvokeRequired)
                        lBorrowETH.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowETH.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " ETH"; else lBorrowETH.Text = "---"; }));
                    mytokenBalances = await TokenBalances("BNB", BSCvBNBcontract, null);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.8;
                    if (lPriceBNB.InvokeRequired)
                        lPriceBNB.Invoke(new MethodInvoker(delegate { lPriceBNB.Text = "$" + mytokenBalances.PriceUSD.ToString("N2", CultureInfo.InvariantCulture); }));
                    if (lSupplyBNB.InvokeRequired)
                        lSupplyBNB.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyBNB.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " BNB"; else lSupplyBNB.Text = "---"; }));
                    if (lBorrowBNB.InvokeRequired)
                        lBorrowBNB.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowBNB.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " BNB"; else lBorrowBNB.Text = "---"; }));
                    mytokenBalances = await TokenBalances("XRP", BSCvXRPcontract, null);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.6;
                    if (lPriceXRP.InvokeRequired)
                        lPriceXRP.Invoke(new MethodInvoker(delegate { lPriceXRP.Text = "$" + mytokenBalances.PriceUSD.ToString("N3", CultureInfo.InvariantCulture); }));
                    if (lSupplyXRP.InvokeRequired)
                        lSupplyXRP.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyXRP.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " XRP"; else lSupplyXRP.Text = "---"; }));
                    if (lBorrowXRP.InvokeRequired)
                        lBorrowXRP.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowXRP.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " XRP"; else lBorrowXRP.Text = "---"; }));
                    mytokenBalances = await TokenBalances("ADA", BSCvADAcontract, null);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.6;
                    if (lPriceADA.InvokeRequired)
                        lPriceADA.Invoke(new MethodInvoker(delegate { lPriceADA.Text = "$" + mytokenBalances.PriceUSD.ToString("N3", CultureInfo.InvariantCulture); }));
                    if (lSupplyADA.InvokeRequired)
                        lSupplyADA.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyADA.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " ADA"; else lSupplyADA.Text = "---"; }));
                    if (lBorrowADA.InvokeRequired)
                        lBorrowADA.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowADA.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " ADA"; else lBorrowADA.Text = "---"; }));
                    mytokenBalances = await TokenBalances("DOGE", BSCvDOGEcontract, null);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.4;
                    if (lPriceDOGE.InvokeRequired)
                        lPriceDOGE.Invoke(new MethodInvoker(delegate { lPriceDOGE.Text = "$" + mytokenBalances.PriceUSD.ToString("N3", CultureInfo.InvariantCulture); }));
                    if (lSupplyDOGE.InvokeRequired)
                        lSupplyDOGE.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyDOGE.Text = mytokenBalances.Supply.ToString("N1", CultureInfo.InvariantCulture) + " DOGE"; else lSupplyDOGE.Text = "---"; }));
                    if (lBorrowDOGE.InvokeRequired)
                        lBorrowDOGE.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowDOGE.Text = mytokenBalances.Borrow.ToString("N1", CultureInfo.InvariantCulture) + " DOGE"; else lBorrowDOGE.Text = "---"; }));
                    mytokenBalances = await TokenBalances("DOT", BSCvDOTcontract, null);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.6;
                    if (lPriceDOT.InvokeRequired)
                        lPriceDOT.Invoke(new MethodInvoker(delegate { lPriceDOT.Text = "$" + mytokenBalances.PriceUSD.ToString("N2", CultureInfo.InvariantCulture); }));
                    if (lSupplyDOT.InvokeRequired)
                        lSupplyDOT.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyDOT.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " DOT"; else lSupplyDOT.Text = "---"; }));
                    if (lBorrowDOT.InvokeRequired)
                        lBorrowDOT.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowDOT.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " DOT"; else lBorrowDOT.Text = "---"; }));
                    mytokenBalances = await TokenBalances("LTC", BSCvLTCcontract, null);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.6;
                    if (lPriceLTC.InvokeRequired)
                        lPriceLTC.Invoke(new MethodInvoker(delegate { lPriceLTC.Text = "$" + mytokenBalances.PriceUSD.ToString("N1", CultureInfo.InvariantCulture); }));
                    if (lSupplyLTC.InvokeRequired)
                        lSupplyLTC.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyLTC.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " LTC"; else lSupplyLTC.Text = "---"; }));
                    if (lBorrowLTC.InvokeRequired)
                        lBorrowLTC.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowLTC.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " LTC"; else lBorrowLTC.Text = "---"; }));
                    mytokenBalances = await TokenBalances("BCH", BSCvBCHcontract, null);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.6;
                    if (lPriceBCH.InvokeRequired)
                        lPriceBCH.Invoke(new MethodInvoker(delegate { lPriceBCH.Text = "$" + mytokenBalances.PriceUSD.ToString("N1", CultureInfo.InvariantCulture); }));
                    if (lSupplyBCH.InvokeRequired)
                        lSupplyBCH.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyBCH.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " BCH"; else lSupplyBCH.Text = "---"; }));
                    if (lBorrowBCH.InvokeRequired)
                        lBorrowBCH.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowBCH.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " BCH"; else lBorrowBCH.Text = "---"; }));
                    mytokenBalances = await TokenBalances("LINK", BSCvLINKcontract, null);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.6;
                    if (lPriceLINK.InvokeRequired)
                        lPriceLINK.Invoke(new MethodInvoker(delegate { lPriceLINK.Text = "$" + mytokenBalances.PriceUSD.ToString("N2", CultureInfo.InvariantCulture); }));
                    if (lSupplyLINK.InvokeRequired)
                        lSupplyLINK.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyLINK.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " LINK"; else lSupplyLINK.Text = "---"; }));
                    if (lBorrowLINK.InvokeRequired)
                        lBorrowLINK.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowLINK.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " LINK"; else lBorrowLINK.Text = "---"; }));
                    mytokenBalances = await TokenBalances("FIL", BSCvFILcontract, null);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.6;
                    if (lPriceFIL.InvokeRequired)
                        lPriceFIL.Invoke(new MethodInvoker(delegate { lPriceFIL.Text = "$" + mytokenBalances.PriceUSD.ToString("N2", CultureInfo.InvariantCulture); }));
                    if (lSupplyFIL.InvokeRequired)
                        lSupplyFIL.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyFIL.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " FIL"; else lSupplyFIL.Text = "---"; }));
                    if (lBorrowFIL.InvokeRequired)
                        lBorrowFIL.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowFIL.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " FIL"; else lBorrowFIL.Text = "---"; }));
                    mytokenBalances = await TokenBalances("SXP", BSCvSXPcontract, null);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.8;
                    if (lPriceSXP.InvokeRequired)
                        lPriceSXP.Invoke(new MethodInvoker(delegate { lPriceSXP.Text = "$" + mytokenBalances.PriceUSD.ToString("N3", CultureInfo.InvariantCulture); }));
                    if (lSupplySXP.InvokeRequired)
                        lSupplySXP.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplySXP.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " SXP"; else lSupplySXP.Text = "---"; }));
                    if (lBorrowSXP.InvokeRequired)
                        lBorrowSXP.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowSXP.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " SXP"; else lBorrowSXP.Text = "---"; }));
                    mytokenBalances = await TokenBalances("BETH", BSCvBETHcontract, null);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.6;
                    if (lPriceBETH.InvokeRequired)
                        lPriceBETH.Invoke(new MethodInvoker(delegate { lPriceBETH.Text = "$" + mytokenBalances.PriceUSD.ToString("N0", CultureInfo.InvariantCulture); }));
                    if (lSupplyBETH.InvokeRequired)
                        lSupplyBETH.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyBETH.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " BETH"; else lSupplyBETH.Text = "---"; }));
                    if (lBorrowBETH.InvokeRequired)
                        lBorrowBETH.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowBETH.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " BETH"; else lBorrowBETH.Text = "---"; }));
                    mytokenBalances = await TokenBalances("USDT", BSCvUSDTcontract, vTokenService);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.8;
                    if (lPriceUSDT.InvokeRequired)
                        lPriceUSDT.Invoke(new MethodInvoker(delegate { lPriceUSDT.Text = "$" + mytokenBalances.PriceUSD.ToString("N3", CultureInfo.InvariantCulture); }));
                    if (lSupplyUSDT.InvokeRequired)
                        lSupplyUSDT.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyUSDT.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " USDT"; else lSupplyUSDT.Text = "---"; }));
                    if (lBorrowUSDT.InvokeRequired)
                        lBorrowUSDT.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowUSDT.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " USDT"; else lBorrowUSDT.Text = "---"; }));
                    mytokenBalances = await TokenBalances("USDC", BSCvUSDCcontract, vTokenService);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.8;
                    if (lPriceUSDC.InvokeRequired)
                        lPriceUSDC.Invoke(new MethodInvoker(delegate { lPriceUSDC.Text = "$" + mytokenBalances.PriceUSD.ToString("N3", CultureInfo.InvariantCulture); }));
                    if (lSupplyUSDC.InvokeRequired)
                        lSupplyUSDC.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyUSDC.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " USDC"; else lSupplyUSDC.Text = "---"; }));
                    if (lBorrowUSDC.InvokeRequired)
                        lBorrowUSDC.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowUSDC.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " USDC"; else lBorrowUSDC.Text = "---"; }));
                    mytokenBalances = await TokenBalances("BUSD", BSCvBUSDcontract, vTokenService);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.8;
                    if (lPriceBUSD.InvokeRequired)
                        lPriceBUSD.Invoke(new MethodInvoker(delegate { lPriceBUSD.Text = "$" + mytokenBalances.PriceUSD.ToString("N3", CultureInfo.InvariantCulture); }));
                    if (lSupplyBUSD.InvokeRequired)
                        lSupplyBUSD.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyBUSD.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " BUSD"; else lSupplyBUSD.Text = "---"; }));
                    if (lBorrowBUSD.InvokeRequired)
                        lBorrowBUSD.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowBUSD.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " BUSD"; else lBorrowBUSD.Text = "---"; }));
                    mytokenBalances = await TokenBalances("DAI", BSCvDAIcontract, vTokenService);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    TotalSupplyUSD += mytokenBalances.SupplyUSD; TotalBorrowUSD += mytokenBalances.BorrowUSD; TotalLimitUSD += mytokenBalances.SupplyUSD * 0.6;
                    if (lPriceDAI.InvokeRequired)
                        lPriceDAI.Invoke(new MethodInvoker(delegate { lPriceDAI.Text = "$" + mytokenBalances.PriceUSD.ToString("N3", CultureInfo.InvariantCulture); }));
                    if (lSupplyDAI.InvokeRequired)
                        lSupplyDAI.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Supply != 0) lSupplyDAI.Text = mytokenBalances.Supply.ToString("N2", CultureInfo.InvariantCulture) + " DAI"; else lSupplyDAI.Text = "---"; }));
                    if (lBorrowDAI.InvokeRequired)
                        lBorrowDAI.Invoke(new MethodInvoker(delegate { if (mytokenBalances.Borrow != 0) lBorrowDAI.Text = mytokenBalances.Borrow.ToString("N2", CultureInfo.InvariantCulture) + " DAI"; else lBorrowDAI.Text = "---"; }));
                    //VAI
                    Contract VAIContract = web3.Eth.GetContract(ABIVAItoken, BSCVAIcontract);
                    Function VAIBorrowFunction = VAIContract.GetFunction("balanceOf");
                    dynamic VAIBorrowResult = await VAIBorrowFunction.CallAsync<dynamic>(BSCAddress);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    double BorrowVAI = (double)VAIBorrowResult / 1E+18;
                    //VAIminted
                    Contract VAImintedContract = web3.Eth.GetContract(ABIUnitroller, BSCUnitrollercontract);
                    Function VAImintedFunction = VAImintedContract.GetFunction("mintedVAIs");
                    dynamic VAImintedResult = await VAImintedFunction.CallAsync<dynamic>(BSCAddress);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    double VAIminted = (double)VAImintedResult / 1E+18;
                    if (lVAImint.InvokeRequired)
                        lVAImint.Invoke(new MethodInvoker(delegate { lVAImint.Text = "Minted: " + "$" + VAIminted.ToString("N0", CultureInfo.InvariantCulture); }));
                    //VAIvault
                    Contract VAIvaultContract = web3.Eth.GetContract(ABIVAIvault, BSCVAIvaultcontract);
                    Function VAIvaultFunction = VAIvaultContract.GetFunction("userInfo");
                    dynamic VAIvaultResult = await VAIvaultFunction.CallAsync<dynamic>(BSCAddress);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    double VAIvault = (double)VAIvaultResult / 1E+18;
                    if (lVAIvault.InvokeRequired)
                        lVAIvault.Invoke(new MethodInvoker(delegate { lVAIvault.Text = "Vault: " + "$" + VAIvault.ToString("N0", CultureInfo.InvariantCulture); }));
                    //Balance
                    Balance = TotalSupplyUSD;
                    if (lBalance.InvokeRequired)
                        lBalance.Invoke(new MethodInvoker(delegate { lBalance.Text = "Balance: " + "$" + Balance.ToString("N0", CultureInfo.InvariantCulture); }));
                    //Limit
                    BorrowLimit = (TotalBorrowUSD + BorrowVAI + VAIvault - (VAIvault - VAIminted)) / TotalLimitUSD;
                    if (lLimit.InvokeRequired) lLimit.Invoke(new MethodInvoker(delegate { lLimit.Text = "Limit: " + BorrowLimit.ToString("P2", CultureInfo.InvariantCulture); }));
                    if (pbLimit.InvokeRequired) pbLimit.Invoke(new MethodInvoker(delegate { var Limit = BorrowLimit;  if(BorrowLimit > 1) Limit = 1; pbLimit.Value = (int)(Limit * 100); pbLimit.ForeColor = Color.FromArgb(249, 190, 86); }));
                    //Report
                    ReadSettings();
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    botClient = new TelegramBotClient(TelegramBotAPI);
                    if (BorrowLimit * 100 > BorrowOver)
                    {
                        if (pbLimit.InvokeRequired)
                        {
                            pbLimit.Invoke(new MethodInvoker(delegate { pbLimit.ForeColor = Color.OrangeRed; }));
                        }
                        await botClient.SendTextMessageAsync(TelegramChatID.ToString(), "Borrow Limit is OVER the limit! :( " + BorrowLimit.ToString("P2", CultureInfo.InvariantCulture));

                    }
                    else if (BorrowLimit * 100 < BorrowUnder)
                    {
                        if (pbLimit.InvokeRequired)
                        {
                            pbLimit.Invoke(new MethodInvoker(delegate { pbLimit.ForeColor = Color.GreenYellow; }));
                        }                        
                        await botClient.SendTextMessageAsync(TelegramChatID.ToString(), "Borrow Limit is UNDER the limit! :) " + BorrowLimit.ToString("P2", CultureInfo.InvariantCulture));
                    }
                }
                catch (Exception ex) { Console.WriteLine("#1 {0}", ex.Message.ToString()); worker.CancelAsync(); }

                await Task.Delay(TimeSpan.FromMinutes(RefreshMins));
            }
            if (!worker.IsBusy) bgwVenusYield.RunWorkerAsync();
        }

        private void BgwVenusYield_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled) return;
            else bgwVenusYield.RunWorkerAsync();
        }

        private void LSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newFrm = new Form2();
            newFrm.Closed += delegate
            {
                ReadSettings();
                bgwVenusYield.CancelAsync();
            };
            newFrm.Show(this);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyVenusYield.Visible = true;
            }
        }

        private void NotifyVenusYield_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyVenusYield.Visible = false;
        }

        private async void BotTelegramReadMsg(object sender, MessageEventArgs messageEventArgs)
        {
            var ReadMsg = messageEventArgs.Message;

            if (ReadMsg == null || ReadMsg.Type != MessageType.Text)
                return;

            try
            {
                switch (ReadMsg.Text.Split(' ').First())
                {
                    case "/report":
                        await botClient.SendTextMessageAsync(ReadMsg.Chat.Id, $"Balance: ${Balance.ToString("N0", CultureInfo.InvariantCulture)}");
                        await botClient.SendTextMessageAsync(ReadMsg.Chat.Id, $"Limit: {BorrowLimit.ToString("P2", CultureInfo.InvariantCulture)}");
                        break;
                    case "/dominance":
                        CoinGeckoGlobal coinGeckoGlobal = new CoinGeckoGlobal();
                        CoinGeckoDefi coinGeckoDefi = new CoinGeckoDefi();
                        WebClient webclientx = new WebClient();
                        coinGeckoGlobal = CoinGeckoGlobal.FromJson(webclientx.DownloadString(urlCoinGeckoGlobal));
                        coinGeckoGlobal.Data.MarketCapPercentage.TryGetValue("btc", out double valuebtc);
                        await botClient.SendTextMessageAsync(ReadMsg.Chat.Id, $"BTC.D: {valuebtc.ToString("F1", CultureInfo.InvariantCulture)}");
                        coinGeckoDefi = CoinGeckoDefi.FromJson(webclientx.DownloadString(urlCoinGeckoDefi));
                        await botClient.SendTextMessageAsync(ReadMsg.Chat.Id, $"DEFI.D: {coinGeckoDefi.Data.DefiDominance.ToString("F1", CultureInfo.InvariantCulture)}");
                        coinGeckoGlobal.Data.MarketCapPercentage.TryGetValue("usdt", out double valueusdt);
                        await botClient.SendTextMessageAsync(ReadMsg.Chat.Id, $"USDT.D: {valueusdt.ToString("F1", CultureInfo.InvariantCulture)}");
                        break;
                    case "/sentiment":
                        FandG indexFandG = new FandG();
                        WebClient webclienty = new WebClient();
                        indexFandG = FandG.FromJson(webclienty.DownloadString(urlFandG));
                        await botClient.SendTextMessageAsync(ReadMsg.Chat.Id, $"Market: {indexFandG.Data[0].ValueClassification} ({indexFandG.Data[0].Value.ToString("F0", CultureInfo.InvariantCulture)})");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex) { Console.WriteLine("#3 {0}", ex.Message.ToString());}
        }

        private static void BotTelegramError(object sender, ReceiveErrorEventArgs errorEventArgs)
        {
            Console.WriteLine("Error: {0} — {1}",
                errorEventArgs.ApiRequestException.ErrorCode,
                errorEventArgs.ApiRequestException.Message);
        }
    }

    public class VTokenBalances
    { 
        public double PriceUSD { get; set; }
        public double Supply { get; set; }
        public double SupplyUSD { get; set; }
        public double Borrow { get; set; }
        public double BorrowUSD { get; set; }
    }

    public partial class BinancePriceChange
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("lastPrice")]
        public string LastPrice { get; set; }

        [JsonProperty("priceChangePercent")]
        public string PriceChangePercent { get; set; }

        public static BinancePriceChange FromJson(string json) => JsonConvert.DeserializeObject<BinancePriceChange>(json);
    }

    public partial class BSCscanTokenBalance
    {
        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        public static BSCscanTokenBalance FromJson(string json) => JsonConvert.DeserializeObject<BSCscanTokenBalance>(json);
    }

    public partial class VenusAPIvTokenService
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

        public static VenusAPIvTokenService FromJson(string json) => JsonConvert.DeserializeObject<VenusAPIvTokenService>(json);
    }

    public partial class Data
    {
        [JsonProperty("venusRate")]
        public string VenusRate { get; set; }

        [JsonProperty("dailyVenus")]
        public string DailyVenus { get; set; }

        [JsonProperty("markets")]
        public List<Market> Markets { get; set; }

        [JsonProperty("request")]
        public Request Request { get; set; }
    }

    public partial class Market
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("underlyingAddress")]
        public string UnderlyingAddress { get; set; }

        [JsonProperty("underlyingName")]
        public string UnderlyingName { get; set; }

        [JsonProperty("underlyingSymbol")]
        public string UnderlyingSymbol { get; set; }

        [JsonProperty("venusSpeeds")]
        public string VenusSpeeds { get; set; }

        [JsonProperty("borrowerDailyVenus")]
        public string BorrowerDailyVenus { get; set; }

        [JsonProperty("supplierDailyVenus")]
        public string SupplierDailyVenus { get; set; }

        [JsonProperty("venusBorrowIndex")]
        public string VenusBorrowIndex { get; set; }

        [JsonProperty("venusSupplyIndex")]
        public string VenusSupplyIndex { get; set; }

        [JsonProperty("borrowRatePerBlock")]
        public string BorrowRatePerBlock { get; set; }

        [JsonProperty("supplyRatePerBlock")]
        public string SupplyRatePerBlock { get; set; }

        [JsonProperty("exchangeRate")]
        public string ExchangeRate { get; set; }

        [JsonProperty("underlyingPrice")]
        public string UnderlyingPrice { get; set; }

        [JsonProperty("totalBorrows")]
        public string TotalBorrows { get; set; }

        [JsonProperty("totalBorrows2")]
        public string TotalBorrows2 { get; set; }

        [JsonProperty("totalBorrowsUsd")]
        public string TotalBorrowsUsd { get; set; }

        [JsonProperty("totalSupply")]
        public string TotalSupply { get; set; }

        [JsonProperty("totalSupply2")]
        public string TotalSupply2 { get; set; }

        [JsonProperty("totalSupplyUsd")]
        public string TotalSupplyUsd { get; set; }

        [JsonProperty("cash")]
        public string Cash { get; set; }

        [JsonProperty("totalReserves")]
        public string TotalReserves { get; set; }

        [JsonProperty("reserveFactor")]
        public string ReserveFactor { get; set; }

        [JsonProperty("collateralFactor")]
        public string CollateralFactor { get; set; }

        [JsonProperty("borrowApy")]
        public string BorrowApy { get; set; }

        [JsonProperty("supplyApy")]
        public string SupplyApy { get; set; }

        [JsonProperty("borrowVenusApy")]
        public string BorrowVenusApy { get; set; }

        [JsonProperty("supplyVenusApy")]
        public string SupplyVenusApy { get; set; }

        [JsonProperty("liquidity")]
        public string Liquidity { get; set; }

        [JsonProperty("tokenPrice")]
        public string TokenPrice { get; set; }

        [JsonProperty("totalDistributed")]
        public long TotalDistributed { get; set; }

        [JsonProperty("totalDistributed2")]
        public string TotalDistributed2 { get; set; }

        [JsonProperty("lastCalculatedBlockNumber")]
        public long LastCalculatedBlockNumber { get; set; }

        [JsonProperty("borrowerCount")]
        public long BorrowerCount { get; set; }

        [JsonProperty("supplierCount")]
        public long SupplierCount { get; set; }
    }

    public partial class Request
    {
        [JsonProperty("addresses")]
        public List<object> Addresses { get; set; }
    }

    public partial class CoinGeckoGlobal
    {
        [JsonProperty("data")]
        public CoinGeckoGlobalData Data { get; set; }

        public static CoinGeckoGlobal FromJson(string json) => JsonConvert.DeserializeObject<CoinGeckoGlobal>(json);
    }

    public partial class CoinGeckoGlobalData
    {
        [JsonProperty("market_cap_percentage")]
        public Dictionary<string, double> MarketCapPercentage { get; set; }
    }

    public partial class CoinGeckoDefi
    {
        [JsonProperty("data")]
        public CoinGeckoDefiData Data { get; set; }

        public static CoinGeckoDefi FromJson(string json) => JsonConvert.DeserializeObject<CoinGeckoDefi>(json);
    }

    public partial class CoinGeckoDefiData
    {
        [JsonProperty("defi_dominance")]
        public double DefiDominance { get; set; }
    }

    public partial class FandG
    {
        [JsonProperty("data")]
        public List<FandGDatum> Data { get; set; }

        public static FandG FromJson(string json) => JsonConvert.DeserializeObject<FandG>(json);
    }

    public partial class FandGDatum
    {
        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("value_classification")]
        public string ValueClassification { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("time_until_update")]
        public long TimeUntilUpdate { get; set; }
    }
}
