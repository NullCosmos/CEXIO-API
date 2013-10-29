using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace CEXIO_API
{
    public class CEXIO
    {
        public static String Key;
        public static String Secret;
        public static String UserName;
        public static Int32 MaxRequestsPerTenMin = 550;
        public static Int32 CurrentRequestCount = 0;
        public static DateTime TimeOfLastReset;
        public enum Commodity { GHS_BTC, BF1_BTC }

        public static NameValueCollection AuthHeader
        {
            get
            {
                String nonce = (DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond).ToString();
                HMACSHA256 hmac = new HMACSHA256(System.Text.Encoding.UTF8.GetBytes(Secret));

                String sig = BitConverter.ToString(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(nonce + UserName + Key)));
                sig = sig.Replace("-","").ToUpper();

                return new NameValueCollection() { 
                    {"key",Key},
                    {"signature",sig},
                    {"nonce",nonce},
                };
            }
        }

        public static String DetectError(String json)
        {
            try
            {
                Error e = JsonConvert.DeserializeObject<Error>(json);
                return e.error;
            }
            catch
            {
                return null;
            }
        }

        public static Boolean CanRequest()
        {
            if (TimeOfLastReset.AddMinutes(10) < DateTime.Now)
            {
                CurrentRequestCount = 0;
                TimeOfLastReset = DateTime.Now;
            }
            return CurrentRequestCount++ < MaxRequestsPerTenMin;
        }
    }
}
