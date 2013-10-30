CEXIO-API
=========

A simple .NET interface to CEX.IO API.

If this helps, please consider donating:

19qCZUt4WqLCZ3MQ8tZXcd5gx71EdGjPX2

Using
------
```csharp
using CEXIO_API;
```

Initialization
------
```csharp
CEXIO.Key = "[API Key]";
CEXIO.Secret = "[API Secret]";
CEXIO.UserName = "[username]";
```

Usage
------
```csharp
//Get balance of all commodities
Balance b = Balance.GetBalance();

//Do we have BTC to sell?
if(b.BTC.available>0)
{
  //Get latest data
  Ticker t = Ticker.GetTicker(CEXIO.Commodity.GHS_BTC);

  //Calculate amount to sell
  Double amount = Floor(b.BTC.available / t.last, 8);
  
  //Place order, retreive Order object (this contains the ID, which can be used to cancel the order later)
  Order o = Order.PlaceNewOrder(CEXIO.Commodity.GHS_BTC, Order.Type.Buy, amount, t.last);
}
```

