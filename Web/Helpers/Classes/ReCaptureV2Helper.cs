using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Helpers.Interfaces;

namespace Web.Helpers.Classes
{
    public class ReCaptureV2Helper : ICaptchaHelper
    {
        public string Validate(string EncodedResponse)
        {
            var client = new System.Net.WebClient();

            string PrivateKey = "6LcKdHIUAAAAAKxXnsr_-NLDungbebQPQuQwUsUR";

            var GoogleReply = client.DownloadString(
                string.Format(
                    "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", 
                    PrivateKey, EncodedResponse));

            var captchaResponse = JsonConvert.DeserializeObject<ReCaptureV2Helper>(GoogleReply);

            return captchaResponse.Success.ToLower();
        }

        [JsonProperty("success")]
        public string Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}