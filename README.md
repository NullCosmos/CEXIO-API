CEXIO-API
=========

A simple .NET interface to CEX.IO API.

If this helps, please consider donating:
19qCZUt4WqLCZ3MQ8tZXcd5gx71EdGjPX2

Usage:

```csharp
using CEXIO_API;
```

Initialization:
```csharp
CEXIO.Key = "[API Key]";
CEXIO.Secret = "[API Secret]";
CEXIO.UserName = "[username]";
```

Call API:
```csharp
//Get balance
Balance b = Balance.GetBalance();

//Get latest data
Ticker t = Ticker.GetTicker(CEXIO.Commodity.GHS_BTC);

if(b.BTC.available>0)
{
  //Calculate amount to sell
  Double amount = Floor(b.BTC.available / t.last, 8);
  
  //Place order, retreive Order object (this contains the ID, which can be used to cancel the order later)
  Order o = Order.PlaceNewOrder(CEXIO.Commodity.GHS_BTC, Order.Type.Buy, amount, t.last);
}
```

