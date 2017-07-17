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
    }
}
