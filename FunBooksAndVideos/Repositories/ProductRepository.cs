using FunBooksAndVideos.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunBooksAndVideos.Repositories
{
    public class ProductRepository : IRepository<BaseProduct>
    {
        private List<BaseProduct> repo = new List<BaseProduct>();

        public BaseProduct Get(int Id)
        {
            return repo
                .OfType<Product>()
                .Where(search => search.ProductId == Id)
                .FirstOrDefault();
        }

        public IEnumerable<BaseProduct> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(BaseProduct entity)
        {
            repo.Add(entity);
        }

        public void AddRange(IEnumerable<BaseProduct> entities)
        {
            repo.AddRange(entities);
        }

        public void Remove(BaseProduct entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<BaseProduct> entity)
        {
            throw new NotImplementedException();
        }       
    }
}
