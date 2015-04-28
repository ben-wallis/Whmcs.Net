using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using Whmcs.Exception;

namespace Whmcs.Services
{
    public class APIService : IAPIService
    {
        private string _url;
        private bool _initialised;
        private NameValueCollection _serverCredentials;

        public void InitialiseAPI(string username, string password, string domain, bool secure)
        {
            _serverCredentials = new NameValueCollection
            {
                {"username", username},
                {"password", password},
                {"responsetype", "json"}
            };
            _url = (secure ? "https://" : "http://") + domain + "/includes/api.php";
            _initialised = true;
        }

        public NameValueCollection BuildServerRequest(NameValueCollection request)
        {
            return new NameValueCollection
            {
                _serverCredentials,
                request
            };
        }

        public string GetData(NameValueCollection values)
        {
            if (!_initialised)
            {
                throw new System.Exception("APIService has not been intialised!");
            }

            var serverRequest = new NameValueCollection
            {
                _serverCredentials,
                values
            };

            try
            {
                var webResponse = new WebClient().UploadValues(_url, serverRequest);
                return Encoding.ASCII.GetString(webResponse);
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("403"))
                {
                    throw new ApiConnectionFailedException(
                        "Unable to connect to WHMCS API: Wrong username or password. Access Denied.", ex);
                }

                if (ex.Message.Contains("404"))
                {
                    throw new ApiConnectionFailedException("Unable to connect to: " + _url + ".", ex);
                }

                throw new ApiConnectionFailedException("Unable to connect to WHMCS API. " + ex.Message);
            }
        }
    }
}
