using CodeShopWarehouse.Business;
using CodeShopWarehouse.Data;
using CodeShopWarehouse.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace CodeShopWarehouse.test
{
    [TestClass]
    public class FillOrderServiceTest
    {
        [TestMethod]
        public void Unresolved_FillOrders_CanBe_Retrieved()
        {
            List<FillOrder> orders = new List<FillOrder>()
            {
                new FillOrder()
                {
                    ProcessDate = null,
                    Id = 1,
                    Processed = false,
                    ProductId = 1,
                    Stock = 1,
                    CreatedDate = DateTimeOffset.Now
                },
                new FillOrder()
                {
                    ProcessDate = null,
                    Id = 2,
                    Processed = false,
                    ProductId = 1,
                    Stock = 1,
                    CreatedDate = DateTimeOffset.Now
                },
                new FillOrder()
                {
                    ProcessDate = null,
                    Id = 3,
                    Processed = false,
                    ProductId = 1,
                    Stock = 1,
                    CreatedDate = DateTimeOffset.Now
                }
            };
            var mockFillOrderRepo = Substitute.For<IFillOrderRepo>();
            mockFillOrderRepo.GetUnProcessedFillOrders().Returns(orders);
            FillOrderService fillOrderService = new FillOrderService(mockFillOrderRepo);

            IEnumerable<FillOrder> result;
            try
            {
                result = fillOrderService.GetUnProcessedFillOrders();
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
                return;
            }

            foreach(FillOrder fillOrder in result)
            {
                Assert.AreEqual(fillOrder.Processed, false, "Returned a processed order!");
            }
        }

        [TestMethod]
        public void Unresolved_FillOrder_CanBe_Processed()
        {
            int fillOrderId = 1;
            FillOrder testFillOrder = new FillOrder() { Id = fillOrderId, ProductId = 1, Processed = false, CreatedDate = DateTimeOffset.Now, ProcessDate = null, Stock = 1 };
            var mockFillOrderRepo = Substitute.For<IFillOrderRepo>();
            mockFillOrderRepo.GetFillOrderById(fillOrderId).Returns(testFillOrder);
            FillOrderService fillOrderService = new FillOrderService(mockFillOrderRepo);

            try
            {
                fillOrderService.ProcessFillOrder(testFillOrder);
                Assert.IsTrue(true);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
                return;
            }
        }

        [TestMethod]
        public void Processed_FillOrder_CannontBe_Modified()
        {
            int fillOrderId = 1;
            FillOrder testFillOrder = new FillOrder() { Id = fillOrderId, ProductId = 1, Processed = true, CreatedDate = DateTimeOffset.Now, ProcessDate = DateTimeOffset.Now, Stock = 1 };
            var mockFillOrderRepo = Substitute.For<IFillOrderRepo>();
            mockFillOrderRepo.GetFillOrderById(fillOrderId).Returns(testFillOrder);
            FillOrderService fillOrderService = new FillOrderService(mockFillOrderRepo);

            try
            {
                fillOrderService.ProcessFillOrder(testFillOrder);
                Assert.Fail("FillOrder was processed!");
            }
            catch (Exception exception)
            {
                Assert.AreEqual("FillOrder cannot be processed. Try again later.", exception.Message);
                return;
            }
        }
    }
}
