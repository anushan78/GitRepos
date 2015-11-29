using System.Collections.Generic;
using Evil.Entity;

namespace Evil.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository(IDataContext<Customer> customerDataContext)
        {
            CustomerDataContext = customerDataContext;
        }

        private IDataContext<Customer> CustomerDataContext { get; set; }

        public List<Customer> GetAll()
        {
            return CustomerDataContext.Retrieve();
        }
    }
}