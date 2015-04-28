using System.Collections.Specialized;
using Moq;
using NUnit.Framework;
using Whmcs.DataBrokers;
using Whmcs.Model.Products;
using Whmcs.Services;

namespace Whmcs.Tests.DataBrokers
{
    [TestFixture]
    public class TestAPIDataBroker
    {
        [Test]
        public void GetProductsByProductId_CallsAPIService()
        {
            // Arrange
            const int TestProductId = 5;
            const string TestOutputApiResponse = "testresponse";

            var expectedJson = new NameValueCollection
            {
                {"action", "getproducts"},
                {"pid", TestProductId.ToString()}
            };

            var mockAPIService = new Mock<IAPIService>();
            mockAPIService.Setup(a => a.GetData(expectedJson)).Returns(TestOutputApiResponse).Verifiable();

            var mockJSONService = new Mock<IJSONService>();

            var dataBroker = new APIDataBroker(mockAPIService.Object, mockJSONService.Object);

            // Act
            var result = dataBroker.GetProductsByProductId(TestProductId);

            // Assert
            mockAPIService.Verify(a => a.GetData(expectedJson)); // Verify that APIService.GetData was called with the JSON we expect the APIDataBroker to pass to it.
        }

        [Test]
        public void GetProductsByProductId_ReturnsObjectFromJSONService()
        {
            // Arrange
            const int TestProductId = 5;
            var testJson = new NameValueCollection { { "test", "test" } };
            const string TestOutputApiResponse = "testresponse";
            var expectedResult = new ProductsResponse();
            
            var mockAPIService = new Mock<IAPIService>();
            mockAPIService.Setup(a => a.GetData(testJson)).Returns(TestOutputApiResponse).Verifiable();

            var mockJSONService = new Mock<IJSONService>();
            mockJSONService.Setup(j => j.DeserialiseJSON<ProductsResponse>(It.IsAny<string>()))
                .Returns(expectedResult);

            var dataBroker = new APIDataBroker(mockAPIService.Object, mockJSONService.Object);

            // Act
            var result = dataBroker.GetProductsByProductId(TestProductId);

            // Assert
            Assert.AreEqual(expectedResult, result);
            mockJSONService.Verify(j => j.DeserialiseJSON<ProductsResponse>(It.IsAny<string>()));
        }
        
    }
}
