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
Ticker t = Ticker.GetTicker(CEXIO.Commodity.GHS_BTC);

Order o = Order.PlaceNewOrder(CEXIO.Commodity.GHS_BTC, Order.Type.Buy, 1.0, t.last);
```

