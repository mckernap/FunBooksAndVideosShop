
using FunBooksAndVideos.Repositories;
using System.Collections.Generic;
using Moq;
using FunBooksAndVideos.Model;
using FunBooksAndVideos.Memberships;
using System;
using System.Linq;
using NUnit.Framework;
using FunBooksAndVideos.Rules;
using FunBooksAndVideos1.Shipping;

namespace FunBooksAndVideos.Tests
{
    [TestFixture]
    public class ProcessorTests
    {
        private const string _url = "http://www.FunBooksAndVideos.com";
        private const string _site = "/bin/scripts/";
        private string _documentLocation = string.Concat(_url, _site);

        private ProductRepository _productRepository;
        private Mock<ICustomerRepository> _customerRepository;
        private ShippingSlipDocFactory _shippingSlipFactory2;

        private static Random _random = new Random();
        private static int _customerId = _random.Next();

        private readonly Membership _bookMembership = new BookMembership();
        private readonly Membership _videoMembership = new VideoMembership();
        private readonly Membership _premiumMembership = new PremiumMembership();

        private readonly Product _book1 = new Product
        {
            ProductId = 1,
            Type = Product.ProductType.Book,
            Name = "The Girl on the train",
            UnitPrice = 3.00
        };

        private readonly Product _book2 = new Product
        {
            ProductId = 2,
            Type = Product.ProductType.Book,
            Name = "The Girl on the train (Part II)",
            UnitPrice = 3.00
        };

        private readonly Product _video_with_dependencies = new Product
        {
            ProductId = 3,
            Type = Product.ProductType.Video,
            Name = "Comprehensive First Aid Training",
            UnitPrice = 10.00
        };

        private readonly Product _video1 = new Product
        {
            ProductId = 4,
            Type = Product.ProductType.Video,
            Name = "Basic First Aid Training",
            UnitPrice = 3.00
        };

        private static PurchaseOrder CreatePurchaseOrder(params BaseProduct[] products)
        {
            var lineItems = new List<BaseProduct>(products);
            return new PurchaseOrder
            {
                PurchaseOrderId = _random.Next(),
                CustomerId = _customerId,
                Total = lineItems.Where(item => item is Product)
                    .Cast<Product>()
                    .Sum(x => x.UnitPrice),
                DateOrdered = DateTime.Now,
                LineItems = lineItems
            };
        }       

        [SetUp]
        public void SetUp()
        {
            _productRepository = new ProductRepository();
            _customerRepository = new Mock<ICustomerRepository>();
            _shippingSlipFactory2 = new ShippingSlipDocFactory(_documentLocation);
        }

        [Test]
        public void test_creation_of_shipping_slip_if_physical_product_is_purchased()
        {
            var purchaseOrder = CreatePurchaseOrder(_video1);

            var rules = new List<AbstractBaseRule<Result>>();
            rules.Add(new MembershipRule(purchaseOrder, _customerRepository.Object));
            rules.Add(new ShippingSlipRule(purchaseOrder, _shippingSlipFactory2));
            var engine = new FlexRuleEngineImp<Result>(rules);
            var result = engine.Run();
            Assert.That(result.OutputItem, Is.EqualTo(_documentLocation));
        }

        [Test]
        public void test_shipping_slip_is_not_created_if_no_physical_product_is_purchased()
        {
            var purchaseOrder = CreatePurchaseOrder(_videoMembership);
            var rules = new List<AbstractBaseRule<Result>>();
            rules.Add(new ShippingSlipRule(purchaseOrder, _shippingSlipFactory2));
            var engine = new FlexRuleEngineImp<Result>(rules);
            var result = engine.Run();
            Assert.That(result.OutputItem, Is.Null);
        }
        [Test]
        public void test_no_membership_is_created()
        {
            var purchaseOrder = CreatePurchaseOrder(_book1);
            var rules = new List<AbstractBaseRule<Result>>();
            rules.Add(new MembershipRule(purchaseOrder, _customerRepository.Object));
            var engine = new FlexRuleEngineImp<Result>(rules);
            var result = engine.Run();

            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _bookMembership), Times.Never);
            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _videoMembership), Times.Never);
            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _premiumMembership), Times.Never);
        }

        [Test]
        public void test_creation_of_book_membership()
        {
            var purchaseOrder = CreatePurchaseOrder(_book1, _book2, _bookMembership);
            var rules = new List<AbstractBaseRule<Result>>();
            rules.Add(new MembershipRule(purchaseOrder, _customerRepository.Object));
            var engine = new FlexRuleEngineImp<Result>(rules);
            var result = engine.Run();

            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _bookMembership));
            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _videoMembership), Times.Never);
            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _premiumMembership), Times.Never);
        }

        [Test]
        public void test_creation_of_premium_membership()
        {
            var purchaseOrder = CreatePurchaseOrder(_video1, _book2, _premiumMembership);
            var rules = new List<AbstractBaseRule<Result>>();
            rules.Add(new MembershipRule(purchaseOrder, _customerRepository.Object));
            var engine = new FlexRuleEngineImp<Result>(rules);
            var result = engine.Run();

            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _bookMembership), Times.Never);
            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _videoMembership), Times.Never);
            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _premiumMembership));
        }

        [Test]
        public void test_creation_of_video_membership()
        {
            var purchaseOrder = CreatePurchaseOrder(_video1, _videoMembership);
            var rules = new List<AbstractBaseRule<Result>>();
            rules.Add(new MembershipRule(purchaseOrder, _customerRepository.Object));
            var engine = new FlexRuleEngineImp<Result>(rules);
            var result = engine.Run();

            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _bookMembership), Times.Never);
            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _videoMembership));
            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _premiumMembership), Times.Never);
        }

        [Test]
        public void test_creation_of_video_and_book_membership()
        {
            var purchaseOrder = CreatePurchaseOrder(_videoMembership, _bookMembership);
            var rules = new List<AbstractBaseRule<Result>>();
            rules.Add(new MembershipRule(purchaseOrder, _customerRepository.Object));
            var engine = new FlexRuleEngineImp<Result>(rules);
            var result = engine.Run();

            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _bookMembership));
            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _videoMembership));
            _customerRepository.Verify(customers => customers.ActivateMembership(_customerId, _premiumMembership), Times.Never);
        }

        [Test]
        public void test_purchase_order_update_of_dependent_videos()
        {
            var products = new List<Product>(new Product[] { _video_with_dependencies, _video1 });
            _productRepository.AddRange(products);

            var purchaseOrder = CreatePurchaseOrder(_video_with_dependencies);
            var rules = new List<AbstractBaseRule<PurchaseOrder>>();
            rules.Add(new DependentProductRule(purchaseOrder, _productRepository));
            var engine = new FlexRuleEngineImp<PurchaseOrder>(rules);
            var result = engine.Run();

            CollectionAssert.Contains(result.LineItems, _video1);
        }
    }
}