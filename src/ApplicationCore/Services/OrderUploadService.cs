using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Services;
public class OrderUploadService : IOrderUploadService
{
    private readonly IAppLogger<OrderUploadService> _logger;

    public OrderUploadService(IAppLogger<OrderUploadService> logger)
    {
        _logger = logger;
    }

    public async Task UploadToOrderItemsReserver(OrderModel orderItems)
    {
        using (HttpClient httpClient = new HttpClient())
        {

            var functionurl = "https://eshoponwebtaskfunctionapp.azurewebsites.net/api/UploadToCosmosDb?code=SmeyVz7GRY9Xa7D9zt01JJTQwIa-F3azZvVa1XB-nAIMAzFukb0L1g%3D%3D";

            //var content = new StringContent(JsonSerializer.Serialize(orderItems), Encoding.UTF8, "application/json");

            HttpResponseMessage response = httpClient.PostAsJsonAsync(functionurl, orderItems).Result;
            _logger.LogInformation(response.Content.ToString());
        }
    }
}

public class OrderModel
{
    public int OrderId { get; set; }
    public string UserId { get; set; }
    public IEnumerable<Item> Items { get; set; }
}

public class Item
{
    public int Id { get; set; }
    public int Quantity { get; set; }
}
