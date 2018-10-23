using System.Collections.Generic;
using CodeShopWarehouse.Entities;

namespace CodeShopWarehouse.Data
{
    public interface IFillOrderRepo
    {
        FillOrder GetFillOrderById(int fillOrderId);
        void UpdateFillOrder(FillOrder editedFillOrder);
        FillOrder CreateFillOrder(FillOrder createdFillOrder);
        IEnumerable<FillOrder> GetUnProcessedFillOrders();
        IEnumerable<FillOrder> GetFillOrdersByProductId(int productId);
    }
}