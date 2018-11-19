using System.Collections.Generic;

namespace AnyCompany
{
    public class CustomerRepositoryWrapper : ICustomerRepository
    {
        public Customer Load(int id)
        {
            return CustomerRepository.Load(id);
        }

        public IEnumerable<Customer> LoadAll()
        {
            return CustomerRepository.LoadAll();
        }
    }
}
