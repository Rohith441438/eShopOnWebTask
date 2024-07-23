
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Services;

namespace Microsoft.eShopWeb.Web.Pages.Basket;

public interface ICosmosDbService
{
    Task SaveOrderDetailsToDb(OrderDetails orderDetails);
}
