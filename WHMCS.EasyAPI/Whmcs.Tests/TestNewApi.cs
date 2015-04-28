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
            const int TestInputProductId = 1;
            var expectedResult = new ProductsResponse();

            var mockDataBroker = new Mock<IAPIDataBroker>();
            mockDataBroker.Setup(d => d.GetProductsByProductId(TestInputProductId)).Returns(expectedResult).Verifiable();

            var api = new NewApi(mockDataBroker.Object);

            // Act
            var result = api.GetProductsByProductId(TestInputProductId);

            // Assert
            Assert.AreEqual(expectedResult, result);
            mockDataBroker.Verify();
        }
    }
}
