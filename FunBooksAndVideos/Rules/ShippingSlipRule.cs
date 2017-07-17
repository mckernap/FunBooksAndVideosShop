using FunBooksAndVideos.Model;
using FunBooksAndVideos.Shipping;
using System.Linq;

namespace FunBooksAndVideos.Rules
{
    class ShippingSlipRule : AbstractBaseRule<Result>
    {
        private readonly PurchaseOrder _purchaseOrder;
        private readonly IShippingSlip2 _shippingSlip;

        public ShippingSlipRule(PurchaseOrder purchaseOrder, IShippingSlip2 shippingSlip)
        {
            _purchaseOrder = purchaseOrder;
            _shippingSlip = shippingSlip;
        }
        public override void Initialize(Result result)
        {
            Conditions.Add(new Equals(1, 1));
        }

        public override Result Apply(Result result)
        {
            if (_purchaseOrder.LineItems.Any(item => item is Product))
            {
                result.OutputItem = _shippingSlip.Create(_purchaseOrder.LineItems).OutputItem;
            }
            return result;
        }
    }
}
