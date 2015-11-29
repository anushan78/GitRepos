using System.Collections.Generic;
using AutoMapper;
using Evil.Entity;
using Evil.Repository;
using Evil.Service.Dto;

namespace Evil.Service
{
    public class CustomerService : ICustomerService
    {
        public CustomerService(ICustomerRepository customerRepository)
        {
            CustomerRepository = customerRepository;
        }

        private ICustomerRepository CustomerRepository { get; set; }

        public IEnumerable<CustomerEntry> GetAll()
        {
            Mapper.CreateMap<Customer, CustomerEntry>()
                .ForMember(t => t.FullName, m => m.MapFrom(b => b.Name))
                .ForMember(t => t.Value, m => m.MapFrom(b => b.Value));

            var customers = CustomerRepository.GetAll();

            return Mapper.Map<List<Customer>, List<CustomerEntry>>(customers);
        }
    }
}