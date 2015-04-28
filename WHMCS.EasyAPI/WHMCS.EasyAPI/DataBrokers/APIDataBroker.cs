using System.Collections.Specialized;
using Whmcs.Model.Products;
using Whmcs.Services;

namespace Whmcs.DataBrokers
{
    public class APIDataBroker : IAPIDataBroker
    {
        private readonly IAPIService _apiService;
        private readonly IJSONService _jsonService;

        public APIDataBroker(IAPIService apiService, IJSONService jsonService)
        {
            _apiService = apiService;
            _jsonService = jsonService;
        }

        public ProductsResponse GetProductsByProductId(int productId)
        {
            var inputData = new NameValueCollection
            {
                {"action", "getproducts"},
                {"pid", productId.ToString()}
            };

            var apiResponse = _apiService.GetData(inputData);

            return _jsonService.DeserialiseJSON<ProductsResponse>(apiResponse);
        }
    }
}
