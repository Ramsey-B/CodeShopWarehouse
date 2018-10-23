using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShopWarehouse.Entities
{
    public class FillOrder
    {
        public int ProductId { get; set; }
        public int Id { get; set; }
        public int Stock { get; set; }
        public bool Processed { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ProcessDate { get; set; }
    }
}
