using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Model
{
    public class Product : BaseProduct
	{

        public enum ProductType { Book, Video, Membership };
        public int ProductId { get; set; }

        public double UnitPrice { get; set; }

        public ProductType Type { get; set; }

        public IEnumerable<BaseProduct> dependencies;
    }
}
