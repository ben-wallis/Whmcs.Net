using Whmcs.Model.Products;

namespace Whmcs.DataBrokers
{
    public interface IAPIDataBroker
    {
        ProductsResponse GetProductsByProductId(int productId);
    }
}