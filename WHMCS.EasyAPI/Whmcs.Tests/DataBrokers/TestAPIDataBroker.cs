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

            // Create the APIService mock and set it up to return the TestOutputApiResponse string when we pass it in the expectedJson,
            // which is created to mirror the JSON the APIDataBroker is expected to create.
            var mockAPIService = new Mock<IAPIService>();
            mockAPIService.Setup(a => a.GetData(expectedJson)).Returns(TestOutputApiResponse).Verifiable();

            var mockJSONService = new Mock<IJSONService>(); // In this test we don't care what JSON is returned so no setups for IJSONService.

            var dataBroker = new APIDataBroker(mockAPIService.Object, mockJSONService.Object); // Instantiate the class under test

            // Act
            var result = dataBroker.GetProductsByProductId(TestProductId); // Invoke the method being tested.

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
            
            // As per above, set up the mock API service but this time it's just going to return junk test data
            // because we aren't testing the API service in this test.
            var mockAPIService = new Mock<IAPIService>();
            mockAPIService.Setup(a => a.GetData(testJson)).Returns(TestOutputApiResponse).Verifiable(); 

            // Set up the Mock JSONService to return the expectedResult ProductsResponse object no matter what's passed into it
            var mockJSONService = new Mock<IJSONService>();
            mockJSONService.Setup(j => j.DeserialiseJSON<ProductsResponse>(It.IsAny<string>()))
                .Returns(expectedResult);

            var dataBroker = new APIDataBroker(mockAPIService.Object, mockJSONService.Object);

            // Act
            var result = dataBroker.GetProductsByProductId(TestProductId);

            // Assert

            // Verify that the string we set the Mock JSONService object to return was returned by the method under test,
            // and verify that the DeserialiseJSON<ProductsResponse> method was called on the Mock JSONService.
            Assert.AreEqual(expectedResult, result);
            mockJSONService.Verify(j => j.DeserialiseJSON<ProductsResponse>(It.IsAny<string>()));
        }
        
    }
}
