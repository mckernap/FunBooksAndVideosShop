using FunBooksAndVideos.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FunBooksAndVideos.Repositories
{
    public class CustomerRepository : IRepository<Customer>, ICustomerRepository 
    {
        private List<Customer> repo = new List<Customer>();

        public void ActivateMembership(int id, Membership membership)
        {
            throw new NotImplementedException();
        }

        public void Add(Customer entity)
        {
            repo.Add(entity);
        }

        public void AddRange(IEnumerable<Customer> entity)
        {
            throw new NotImplementedException();
        }

        public Customer Get(int Id)
        {
            return repo.Find(x => x.CustomerId == Id);
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Customer> entity)
        {
            throw new NotImplementedException();
        }
    }
}
