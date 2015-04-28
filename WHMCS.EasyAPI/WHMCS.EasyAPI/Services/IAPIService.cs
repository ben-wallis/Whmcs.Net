using System.Collections.Specialized;

namespace Whmcs.Services
{
    public interface IAPIService
    {
        void InitialiseAPI(string username, string password, string domain, bool secure);
        string GetData(NameValueCollection values);
    }
}