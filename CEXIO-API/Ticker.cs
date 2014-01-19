using Newtonsoft.Json;
using System;
using System.Net;

namespace CEXIO_API
{
    public class Ticker
    {
        private const String url = "https://cex.io/api/ticker/";

        /// <summary>
        /// last BTC price
        /// </summary>
        public Double last = 0;
        /// <summary>
        /// last 24 hours price high
        /// </summary>
        public Double high = 0;
        /// <summary>
        /// last 24 hours price low
        /// </summary>
        public Double low = 0;
        /// <summary>
        /// last 24 hours volume
        /// </summary>
        public Double volume = 0;
        /// <summary>
        /// highest buy order
        /// </summary>
        public Double bid = 0;
        /// <summary>
        /// lowest sell order
        /// </summary>
        public Double ask = 0;

        public static Ticker GetTicker(CEXIO.Commodity commodity)
        {
            if (!CEXIO.CanRequest())
                return null;

            String[] cS = Enum.GetName(typeof(CEXIO.Commodity), commodity).Split('_');

            WebClient www = new WebClient();
            byte[] res = www.UploadValues(url + cS[0] + "/" + cS[1], CEXIO.AuthHeader);
            String strRes = System.Text.Encoding.UTF8.GetString(res);
            
            String err = CEXIO.DetectError(strRes);
            if (!String.IsNullOrEmpty(err))
                return null;

            return Deserialize(strRes);
        }

        public static Ticker Deserialize(String json)
        {
            return JsonConvert.DeserializeObject<Ticker>(json);
        }
    }
}
