using FunBooksAndVideos.Model;
using FunBooksAndVideos.Repositories;
using System.Linq;

namespace FunBooksAndVideos.Rules
{
    public class DependentProductRule : AbstractBaseRule<PurchaseOrder>
    {
        private readonly ProductRepository _productRepository;
        private readonly PurchaseOrder _purchaseOrder;
        public DependentProductRule(PurchaseOrder purchaseOrder, ProductRepository productRepository)
        {
            _productRepository = productRepository;
            _purchaseOrder = purchaseOrder;
        }

        public override void Initialize(PurchaseOrder purchaseOrder)
        {
            var count = _purchaseOrder.LineItems
                .Where(item => item is Product)
                .Cast<Product>()
                .Where(x => x.Name == "Comprehensive First Aid Training")
                .Count();

            Conditions.Add(new Equals(1, count));
        }
        public override PurchaseOrder Apply(PurchaseOrder purchaseOrder)
        {
            PurchaseOrder po = new PurchaseOrder(_purchaseOrder);
            Product video = (Product)_productRepository.Get(4); // Basic First Aid Training
            po.LineItems = po.LineItems.Concat(new[] { video }).ToList();
            purchaseOrder.Assign(po);
            return po;
        }
    }
}
