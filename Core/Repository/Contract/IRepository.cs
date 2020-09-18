

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FoodPortal.API.Core.Repository.Contract{
    public interface IRepository<T,T1> where T : class
    {
        void Add(T obj);
        void Update(T obj);
        void Delete(T1 data);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetFiltered(Func<T,bool> predicate);
    }
}