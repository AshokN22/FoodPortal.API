using FoodPortal.API.Core.Entity;

namespace FoodPortal.API.Core.Repository.Contract{
    public interface IUnitOfWork{
        IRepository<Customer,int> CustRepository{get;}
        void Complete();
    }
}