
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Services;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

public interface ICosmosDbService
{
    Task SaveOrderDetailsToDb(OrderDetails orderDetails);
}
