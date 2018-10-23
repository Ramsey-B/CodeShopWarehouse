using CodeShopWarehouse.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CodeShopWarehouse.Data
{

    public class FillOrderRepo : IFillOrderRepo
    {
        private readonly IDbConnection _db;

        public FillOrderRepo(IDbConnection db)
        {
            _db = db;
        }

        public FillOrder CreateFillOrder(FillOrder createdFillOrder)
        {
            int newFillOrderId;
            try
            {
                newFillOrderId = _db.ExecuteScalar<int>(@"
                INSERT INTO fillorders (productId, stock, processed, createdDate, processDate)
                VALUES (@ProductId, @Stock, @Processed, @CreatedDate, @ProcessDate);
                SELECT LAST_INSERT_ID();
                ", createdFillOrder);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            createdFillOrder.Id = newFillOrderId;
            return createdFillOrder;
            //return new FillOrder()
            //{
            //    ProcessDate = null,
            //    Id = 1,
            //    Processed = false,
            //    ProductId = 1,
            //    Stock = 1,
            //    CreatedDate = DateTimeOffset.Now
            //};
        }

        public FillOrder GetFillOrderById(int fillOrderId)
        {
            return _db.QueryFirstOrDefault<FillOrder>(@"select * from fillorders
              where (fillorders.id = @fillorderid);", new { fillOrderId });
            //return new FillOrder()
            //{
            //    ProcessDate = null,
            //    Id = fillOrderId,
            //    Processed = false,
            //    ProductId = 1,
            //    Stock = 1,
            //    CreatedDate = DateTimeOffset.Now
            //};
        }

        public IEnumerable<FillOrder> GetFillOrdersByProductId(int productId)
        {
            return _db.Query<FillOrder>("SELECT * FROM fillorders WHERE productId = @productId", new { productId });
            //List<FillOrder> orders = new List<FillOrder>()
            //{
            //    new FillOrder()
            //    {
            //        ProcessDate = null,
            //        Id = 1,
            //        Processed = false,
            //        ProductId = productId,
            //        Stock = 1,
            //        CreatedDate = DateTimeOffset.Now
            //    },
            //    new FillOrder()
            //    {
            //        ProcessDate = null,
            //        Id = 2,
            //        Processed = false,
            //        ProductId = productId,
            //        Stock = 1,
            //        CreatedDate = DateTimeOffset.Now
            //    },
            //    new FillOrder()
            //    {
            //        ProcessDate = null,
            //        Id = 3,
            //        Processed = false,
            //        ProductId = productId,
            //        Stock = 1,
            //        CreatedDate = DateTimeOffset.Now
            //    }
            //};
            //return orders;
        }

        public IEnumerable<FillOrder> GetUnProcessedFillOrders()
        {
            return _db.Query<FillOrder>("SELECT * FROM fillOrders WHERE processed = false");
            //List<FillOrder> orders = new List<FillOrder>()
            //{
            //    new FillOrder()
            //    {
            //        ProcessDate = null,
            //        Id = 1,
            //        Processed = false,
            //        ProductId = 1,
            //        Stock = 1,
            //        CreatedDate = DateTimeOffset.Now
            //    },
            //    new FillOrder()
            //    {
            //        ProcessDate = null,
            //        Id = 2,
            //        Processed = false,
            //        ProductId = 1,
            //        Stock = 1,
            //        CreatedDate = DateTimeOffset.Now
            //    },
            //    new FillOrder()
            //    {
            //        ProcessDate = null,
            //        Id = 3,
            //        Processed = false,
            //        ProductId = 1,
            //        Stock = 1,
            //        CreatedDate = DateTimeOffset.Now
            //    }
            //};
            //return orders;
        }

        public void UpdateFillOrder(FillOrder editedFillOrder)
        {
            _db.Execute(@"
                UPDATE fillorders SET
                    productId = @ProductId,
                    stock = @Stock,
                    processed = @Processed,
                    processDate = @processDate
                WHERE id = @Id;
            ", editedFillOrder);
        }
    }
}
