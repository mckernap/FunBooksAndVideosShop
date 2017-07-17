using FunBooksAndVideos.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunBooksAndVideos.Model;
using FunBooksAndVideos.Rules;

namespace FunBooksAndVideos1.Shipping
{
    public class ShippingSlipDocFactory : IShippingSlip2
    {
        private Result _result { get; set; }

        public ShippingSlipDocFactory(string item)
        {
            _result = new Result();
            _result.OutputItem = item;
        }

        public Result Create(IEnumerable<BaseProduct> products)
        {
            return _result;
        }
    }
}
