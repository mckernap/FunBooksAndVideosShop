using FunBooksAndVideos.Model;
using FunBooksAndVideos.Rules;
using System.Collections.Generic;

namespace FunBooksAndVideos.Shipping
{
    public interface IShippingSlip2
    {
        Result Create(IEnumerable<BaseProduct> products);
    }
}
