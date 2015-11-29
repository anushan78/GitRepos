using System.Collections.Generic;
using Evil.Service.Dto;

namespace Evil.Service
{
    public interface ICustomerService
    {
        IEnumerable<CustomerEntry> GetAll();
    }
}