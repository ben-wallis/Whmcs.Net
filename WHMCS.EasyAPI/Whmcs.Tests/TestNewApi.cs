using Moq;
using NUnit.Framework;
using Whmcs.DataBrokers;
using Whmcs.Model.Products;

namespace Whmcs.Tests
{
    [TestFixture]
    public class TestNewApi
    {
        [Test]
        public void GetProductsByProductId_ReturnsResultFromDataBroker()
        {
            // Arrange
            const int TestInputProductId = 1; // A product ID to test with
            var expectedResult = new ProductsResponse(); // A dummy ProductsResponse object that we will expect to be returned.

            // Create a Mock IAPIDataBroker object
            var mockDataBroker = new Mock<IAPIDataBroker>(); 

            // Setup the mock so that when GetProductsById is called with TestInputProductId it returns the expectedResult ProductsResponse object.
            // The .Verifiable() on the end enables us to check whether it was actually called.
            mockDataBroker.Setup(d => d.GetProductsByProductId(TestInputProductId)).Returns(expectedResult).Verifiable();
            

            var api = new NewApi(mockDataBroker.Object); // Instantiate the class under test

            // Act
            var result = api.GetProductsByProductId(TestInputProductId); // Perform the method we're testing.

            // Assert

            // Check that the method returned the expectedResult object, which we told the mock data broker to return.
            // This tests that we're actually returning the right data, not some random ProductResponse object that was created in the NewApi class.
            Assert.AreEqual(expectedResult, result);
            
            // Verify that the DataBroker class was called as per the above setup in the Arrange section.
            mockDataBroker.Verify();
        }
    }
}
