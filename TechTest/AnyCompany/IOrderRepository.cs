using System.Collections.Generic;

namespace AnyCompany
{
    /// <summary>
    /// Repository Interface to enable decoupling of the repositories from real implementations.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Save given order
        /// </summary>
        /// <param name="order"></param>
        void Save(Order order);

        /// <summary>
        /// Load a given order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Order Load(int id);

        /// <summary>
        /// Load all order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<Order> LoadAll();

        /// <summary>
        /// Returns all the given customer's orders. 
        /// </summary>
        IEnumerable<Order> LoadByCustomerId(int customerId);
    }
}
