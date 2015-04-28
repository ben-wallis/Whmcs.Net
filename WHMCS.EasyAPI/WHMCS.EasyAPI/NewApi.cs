using Whmcs.DataBrokers;
using Whmcs.Model.Products;

namespace Whmcs
{
    public class NewApi
    {
        private readonly IAPIDataBroker _dataBroker;

        public NewApi(IAPIDataBroker dataBroker)
        {
            _dataBroker = dataBroker;
        }

        public ProductsResponse GetProductsByProductId(int productId)
        {
            return _dataBroker.GetProductsByProductId(productId);
        }
    }
}
