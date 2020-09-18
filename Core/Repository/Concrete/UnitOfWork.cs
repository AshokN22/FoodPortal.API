using FoodPortal.API.Core.Context;
using FoodPortal.API.Core.Entity;
using FoodPortal.API.Core.Repository.Contract;

namespace FoodPortal.API.Core.Repository.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private FPDbContext context = null;
        private IRepository<Customer, int> custRepo = null;
        public UnitOfWork(FPDbContext context)
        {
            this.context = context;
        }
        public IRepository<Customer, int> CustRepository {
            get{
                if(custRepo == null){
                    custRepo = new CustomerRepository(context);
                }
                return custRepo;
            }
        }

        public void Complete()
        {
            context.SaveChanges();
        }
    }
}