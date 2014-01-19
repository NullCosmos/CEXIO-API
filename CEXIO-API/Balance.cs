using Newtonsoft.Json;
using System;
using System.Net;

namespace CEXIO_API
{
    public class Balance
    {
        private const String url = "https://cex.io/api/balance/";

        public Int64 timestamp;
        /// <summary>
        /// referral program bonus
        /// </summary>
        public Double bonus = 0;
        public BalanceItem BTC;
        public BalanceItem GHS;
        public BalanceItem BF1;
        public BalanceItem NMC;
        public BalanceItem ICX;
        public BalanceItem DVC;

        public class BalanceItem
        {
            /// <summary>
            /// available balance
            /// </summary>
            public Double available = 0;
            /// <summary>
            /// balance in pending orders
            /// </summary>
            public Double orders = 0;
        }

        public static Balance GetBalance()
        {
            if (!CEXIO.CanRequest())
                return null;

            WebClient www = new WebClient();
            byte[] res = www.UploadValues(url, CEXIO.AuthHeader);
            String strRes = System.Text.Encoding.UTF8.GetString(res);

            String err = CEXIO.DetectError(strRes);
            if (!String.IsNullOrEmpty(err))
                return null;

            return Deserialize(strRes);
        }

        public static Balance Deserialize(String json)
        {
            return JsonConvert.DeserializeObject<Balance>(json);
        }
    }
}
