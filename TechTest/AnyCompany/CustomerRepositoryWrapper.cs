using System.Collections.Generic;

namespace AnyCompany
{
    /// <summary>
    /// Wrapper Object on CustomerRepository to enable tastability as its a legacy static class 
    /// that must not be refactored to a non-static class 
    /// </summary>
    public class CustomerRepositoryWrapper : ICustomerRepository
    {
        /// <summary>
        /// Loads Customer with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer Load(int id)
        {
            return CustomerRepository.Load(id);
        }

        /// <summary>
        /// Loads all Customer Objects
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> LoadAll()
        {
            return CustomerRepository.LoadAll();
        }
    }
}
