namespace Harvest.Tests.Tests;

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Common;
using Moq;
using Moq.Protected;

[TestFixture]
public class InvoiceItemCategoriesRequestBuilderTests
{
    public class WhenListingInvoiceItemCategories : BaseTestFixture
    {
        [Test]
        public async Task ShouldSetQueryParameters()
        {
            // Arrange
            (HarvestServiceClient harvestServiceClient, Mock<HttpMessageHandler> httpMessageHandler) =
                this.GetHarvestServiceClient();

            var updatedSince = new DateTime(2023, 4, 9);
            int page = 2;
            int perPage = 100;

            string expectedRequestUrl =
                $"https://api.harvestapp.com/v2/invoice_item_categories?updated_since={HttpUtility.UrlEncode(updatedSince.ToString("O"))}&page={page}&per_page={perPage}"
                    .ToLowerInvariant();

            // Act
            await harvestServiceClient.InvoiceItemCategories.GetAsync(c =>
            {
                c.QueryParameters.UpdatedSince = updatedSince;
                c.QueryParameters.Page = page;
                c.QueryParameters.PerPage = perPage;
            });

            // Assert
            httpMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri.ToString().ToLowerInvariant() == expectedRequestUrl),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}