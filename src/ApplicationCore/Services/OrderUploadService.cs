using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
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

    public async Task UploadOrderToServiceBusQueue(OrderModel orderItems)
    {
        var content = new StringContent(JsonSerializer.Serialize(orderItems), Encoding.UTF8, "application/json");
        var connectionString = "Endpoint=sb://servicebusdemorohith.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ea1IWccjcSUzRqXpzAs4a5p2syCL8wYaU+ASbAqPFa4=";
        var queueName = "OrderQueue";

        await using var client = new ServiceBusClient(connectionString);
        await using var serviceBusSender = client.CreateSender(queueName);

        try
        {
            var message = new ServiceBusMessage(content.ToString());
            await serviceBusSender.SendMessageAsync(message);
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            await serviceBusSender.DisposeAsync();
            await client.DisposeAsync();
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
