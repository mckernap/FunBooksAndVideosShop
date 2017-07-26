using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Model
{
    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }
        public int CustomerId { get; set; }
        public double Total { get; set; }
        public DateTime DateOrdered { get; set; }
        public IEnumerable<BaseProduct> LineItems { get; set; }
        public PurchaseOrder()
        {
        }
        public PurchaseOrder(PurchaseOrder rhs)
        {
            Assign(rhs);
        }

        public void Assign(PurchaseOrder rhs)
        {
            this.CustomerId = rhs.CustomerId;
            this.LineItems = new List<BaseProduct>(rhs.LineItems);
            this.PurchaseOrderId = rhs.PurchaseOrderId;
            this.Total = rhs.Total;
        }
    }
}
