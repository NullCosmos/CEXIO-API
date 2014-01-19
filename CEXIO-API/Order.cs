using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using Newtonsoft.Json;

namespace CEXIO_API
{
    public class Order
    {
        private const String OpenOrdersURL = "https://cex.io/api/open_orders/";
        private const String PlaceOrderURL = "https://cex.io/api/place_order/";
        private const String CancelOrderURL = "https://cex.io/api/cancel_order/";
        public enum Type { Buy, Sell }
        
        
        /// <summary>
        /// order id
        /// </summary>
        public Int64 id = 0;
        /// <summary>
        /// timestamp
        /// </summary>
        public Int64 time = 0;
        /// <summary>
        /// buy or sell
        /// </summary>
        public Type type = Type.Buy;
        /// <summary>
        /// the commodity of the order
        /// </summary>
        public CEXIO.Commodity commodity = CEXIO.Commodity.GHS_BTC;
        /// <summary>
        /// price
        /// </summary>
        public Double price = 0;
        /// <summary>
        /// amount
        /// </summary>
        public Double amount = 0;
        /// <summary>
        /// pending amount (if partially executed)
        /// </summary>
        public Double pending = 0;

        public static Order Deserialize(String json)
        {
            return JsonConvert.DeserializeObject<Order>(json);
        }
        public static List<Order> DeserializeList(String json)
        {
            return JsonConvert.DeserializeObject<List<Order>>(json);
        }

        public static List<Order> GetOpenOrders(CEXIO.Commodity commodity)
        {
            if (!CEXIO.CanRequest())
                return null;

            String[] cS = Enum.GetName(typeof(CEXIO.Commodity), commodity).Split('_');
            
            WebClient www = new WebClient();
            byte[] res = www.UploadValues(OpenOrdersURL + cS[0] + "/" + cS[1], CEXIO.AuthHeader);
            String strRes = System.Text.Encoding.UTF8.GetString(res);

            String err = CEXIO.DetectError(strRes);
            if (!String.IsNullOrEmpty(err))
                return null;

            List<Order> orders = DeserializeList(strRes);
            foreach (Order o in orders)
                o.commodity = commodity;

            return orders;
        }

        public static Order PlaceNewOrder(CEXIO.Commodity commodity, Type type, Double amount, Double price)
        {
            if (!CEXIO.CanRequest())
                return null;

            NameValueCollection postData = CEXIO.AuthHeader;
            postData.Add("type", Enum.GetName(typeof(Type),type).ToLower());
            postData.Add("amount", amount.ToString("F8", CultureInfo.InvariantCulture));
            postData.Add("price", price.ToString("F8", CultureInfo.InvariantCulture));

            String[] cS = Enum.GetName(typeof(CEXIO.Commodity), commodity).Split('_');

            WebClient www = new WebClient();
            byte[] res = www.UploadValues(PlaceOrderURL + cS[0] + "/" + cS[1], postData);
            String strRes = System.Text.Encoding.UTF8.GetString(res);

            String err = CEXIO.DetectError(strRes);
            if (!String.IsNullOrEmpty(err))
                return null;

            Order o = Deserialize(strRes);
            o.commodity = commodity;
            return o;
        }
        
        public Boolean Cancel()
        {
            if (!CEXIO.CanRequest())
                return false;

            NameValueCollection postData = CEXIO.AuthHeader;
            postData.Add("id", id.ToString());

            WebClient www = new WebClient();
            byte[] res = www.UploadValues(CancelOrderURL, postData);
            String strRes = System.Text.Encoding.UTF8.GetString(res);

            String err = CEXIO.DetectError(strRes);
            if (!String.IsNullOrEmpty(err))
                return false;

            return (strRes.ToLower() == "true");
        }
    }
}
