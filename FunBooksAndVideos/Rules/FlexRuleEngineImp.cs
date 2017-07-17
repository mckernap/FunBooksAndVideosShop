using FunBooksAndVideos.Model;
using FunBooksAndVideos.Repositories;
using FunBooksAndVideos.Shipping;

namespace FunBooksAndVideos.Rules
{
    public class FlexRuleEngineImp
    {
        private readonly Result _result;
        private readonly PurchaseOrder _purchaseOrder;
        private readonly ICustomerRepository _customerRepository;
        private readonly IShippingSlip2 _shippingSlipFactory;
        private readonly ProductRepository _productRepository;

        public FlexRuleEngineImp(Result result,
                                 PurchaseOrder purchaseOrder,
                                 ICustomerRepository customerRepository,
                                 IShippingSlip2 shippingSlipFactory,
                                 ProductRepository productRepository)
        {
            _result = result;
            _purchaseOrder = purchaseOrder;
            _customerRepository = customerRepository;
            _shippingSlipFactory = shippingSlipFactory;
            _productRepository = productRepository;
        }

        public PurchaseOrder RunPurchaseOrder()
        {
            _purchaseOrder.ApplyRule(new DependentProductRule(_productRepository));

            return _purchaseOrder;
        }

        public Result Run()
        {
            _result
                .ApplyRule(new MembershipRule(_purchaseOrder, _customerRepository))
                .ApplyRule(new ShippingSlipRule(_purchaseOrder, _shippingSlipFactory));

            return _result;
        }
    }
}
