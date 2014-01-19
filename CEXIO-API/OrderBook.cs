using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace CEXIO_API
{
    public class OrderBook
    {
        private const String OrderBookURL = "https://cex.io/api/order_book/";

        public CEXIO.Commodity commodity = CEXIO.Commodity.GHS_BTC;
        public Int32 timestamp;
        public List<List<Double>> bids = new List<List<Double>>();
        public List<List<Double>> asks = new List<List<Double>>();

        public static OrderBook Deserialize(String json)
        {
            return JsonConvert.DeserializeObject<OrderBook>(json);
        }

        public static OrderBook GetOrderBook(CEXIO.Commodity commodity)
        {
            if (!CEXIO.CanRequest())
                return null;
            
            String[] cS = Enum.GetName(typeof(CEXIO.Commodity), commodity).Split('_');
            
            WebClient www = new WebClient();
            byte[] res = www.UploadValues(OrderBookURL + cS[0] + "/" + cS[1], CEXIO.AuthHeader);
            String strRes = System.Text.Encoding.UTF8.GetString(res);

            String err = CEXIO.DetectError(strRes);
            if (!String.IsNullOrEmpty(err))
                return null;

            OrderBook oB = Deserialize(strRes);
            oB.commodity = commodity;
            return oB;
        }

    }
}
