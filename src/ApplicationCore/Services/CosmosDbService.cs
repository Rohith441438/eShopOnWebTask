using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Basket;

namespace Microsoft.eShopWeb.ApplicationCore.Services;
public class CosmosDbService : ICosmosDbService
{
    private readonly IAppLogger<CosmosDbService> _logger;

    public CosmosDbService(IAppLogger<CosmosDbService> logger)
    {
        _logger = logger;
    }

    public async Task SaveOrderDetailsToDb(OrderDetails orderDetails)
    {
        using (HttpClient httpClient = new HttpClient())
        {

            var functionurl = "https://eshoponwebtaskfunctionapp.azurewebsites.net/api/OrderModelUpload?code=-vr_jqDEcT_lMruys_-yaUPvrxV1H78BYyQJQk4y8qWgAzFuom9LlA%3D%3D";

            HttpResponseMessage response = httpClient.PostAsJsonAsync(functionurl, orderDetails).Result;
            _logger.LogInformation(response.Content.ToString());
        }
    }
}

public class OrderDetails
{
    public int Id { get; set; }
    public Address Address { get; set; }
    public List<Item> Items { get; set; }
    public decimal FinalPrice { get; set; }
}
