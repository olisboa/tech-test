using System.Collections.Generic;

namespace AnyCompany
{
    /// <summary>
    /// Repository Interface to enable decoupling of the repositories from real implementations.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Load a given customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Customer Load(int id);

        /// <summary>
        /// Load all customers
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<Customer> LoadAll();
    }
}
