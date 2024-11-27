using System;
using System.Collections.Generic;

namespace Palleoptimering.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public List<int> ElementIds { get; set; }  

        
        public Order()
        {
            ElementIds = new List<int>();
        }

        // Constructor for at lave instans af Order
        public Order(int orderId, DateTime orderDate, string status, List<int> elementIds)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            Status = status;
            ElementIds = elementIds;
        }
    }
}
