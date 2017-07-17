using FunBooksAndVideos.Model;
using FunBooksAndVideos.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Rules
{
    public class MembershipRule : AbstractBaseRule<Result>
    {
        private readonly PurchaseOrder _purchaseOrder;
        private readonly ICustomerRepository _customerRepository;

        public MembershipRule(PurchaseOrder purchaseOrder, ICustomerRepository customerRepository)
        {
            _purchaseOrder = purchaseOrder;
            _customerRepository = customerRepository;
        }
        public override void Initialize(Result result)
        {
            Conditions.Add(new Equals(1, 1));
        }

        public override Result Apply(Result result)
        {
            var memberships = _purchaseOrder.LineItems
            .Where(item => item is Membership)
            .Cast<Membership>();

            foreach (var membership in memberships)
            {
                _customerRepository.ActivateMembership(_purchaseOrder.CustomerId, membership);
            }
            return result;
        }
    }
}
