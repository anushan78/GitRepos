using System.Collections.Generic;
using Evil.Entity;

namespace Evil.Repository
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
    }
}