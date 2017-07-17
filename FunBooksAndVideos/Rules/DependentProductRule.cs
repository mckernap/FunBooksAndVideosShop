using FunBooksAndVideos.Model;
using FunBooksAndVideos.Repositories;
using System.Linq;

namespace FunBooksAndVideos.Rules
{
    public class DependentProductRule : AbstractBaseRule<PurchaseOrder>
    {
        private readonly ProductRepository _productRepository;
        public DependentProductRule(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public override void Initialize(PurchaseOrder purchaseOrder)
        {
            var count = purchaseOrder.LineItems
                .Where(item => item is Product)
                .Cast<Product>()
                .Where(x => x.Name == "Comprehensive First Aid Training")
                .Count();

            Conditions.Add(new Equals(1, count));
        }
        public override PurchaseOrder Apply(PurchaseOrder purchaseOrder)
        {
            Product video = (Product)_productRepository.Get(4); // Basic First Aid Training
            purchaseOrder.LineItems = purchaseOrder.LineItems.Concat(new[] { video });
           
            return purchaseOrder;
        }
    }
}
