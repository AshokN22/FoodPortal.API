using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using FoodPortal.API.Core.Context;
using FoodPortal.API.Core.Entity;
using FoodPortal.API.Core.Repository.Contract;

namespace FoodPortal.API.Core.Repository.Concrete
{
    public class CustomerRepository : IRepository<Customer, int>
    {
        private FPDbContext context = null;

        public CustomerRepository(FPDbContext context){
            this.context = context;
        }

        public void Add(Customer obj)
        {
            context.Customers.Add(obj);            
        }

        public void Delete(int data)
        {
            var obj = context.Customers.Single(c=>c.CID == data);
            context.Customers.Remove(obj);
        }

        public IEnumerable<Customer> GetAll()
        {
            return context.Customers;
        }

        public IEnumerable<Customer> GetFiltered(Func<Customer, bool> predicate)
        {
            return context.Customers.Where(predicate);
        }

        public void Update(Customer obj)
        {
            context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;            
        }
    }
}