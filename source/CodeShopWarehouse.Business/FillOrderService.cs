using CodeShopWarehouse.Business.Validators;
using CodeShopWarehouse.Data;
using CodeShopWarehouse.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShopWarehouse.Business
{
    public class FillOrderService
    {
        private readonly IFillOrderRepo _fillOrderRepo;

        public FillOrderService(IFillOrderRepo fillOrderRepo)
        {
            _fillOrderRepo = fillOrderRepo;
        }

        public FillOrder CreateFillOrder(FillOrder newFillOrder)
        {
            newFillOrder.CreatedDate = DateTime.Now;
            newFillOrder.ProcessDate = null;
            newFillOrder.Processed = false;
            try
            {
                FillOrderValidator.ValidateProductId(newFillOrder);
                FillOrderValidator.ValidateStock(newFillOrder);
                return _fillOrderRepo.CreateFillOrder(newFillOrder);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public FillOrder GetFillOrderById(int fillOrderId)
        {
            return _fillOrderRepo.GetFillOrderById(fillOrderId);
        }

        public IEnumerable<FillOrder> GetUnProcessedFillOrders()
        {
            return _fillOrderRepo.GetUnProcessedFillOrders();
        }

        public IEnumerable<FillOrder> GetFillOrdersByProductId(int productId)
        {
            return _fillOrderRepo.GetFillOrdersByProductId(productId);
        }

        public void ProcessFillOrder(FillOrder submittedFillOrder)
        {
            FillOrder actualFillOrder = _fillOrderRepo.GetFillOrderById(submittedFillOrder.Id);
            try
            {
                FillOrderValidator.ValidateFillOrderUnprocessed(actualFillOrder);
                actualFillOrder.Processed = true;
                actualFillOrder.ProcessDate = DateTime.Now;
                _fillOrderRepo.UpdateFillOrder(actualFillOrder);
            }
            catch (Exception)
            {
                throw new Exception("FillOrder cannot be processed. Try again later.");
            }
        }
    }
}
