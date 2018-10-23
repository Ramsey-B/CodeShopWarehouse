using CodeShopWarehouse.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShopWarehouse.Business.Validators
{
    internal static class FillOrderValidator
    {
        public static void ValidateProductId(FillOrder fillOrder)
        {
            if (fillOrder.ProductId < 0)
            {
                throw new Exception("Fill Order must have valid Product Id");
            }
        }

        public static void ValidateStock(FillOrder fillOrder)
        {
            if (fillOrder.Stock == 0)
            {
                throw new Exception("Fill Order can not have a stock value of 0.");
            }
        }

        public static void ValidateFillOrderUnprocessed(FillOrder fillOrder)
        {
            if (fillOrder.Processed || fillOrder.ProcessDate != null)
            {
                throw new Exception("Fill Order has already been processed!");
            }
        }
    }
}
