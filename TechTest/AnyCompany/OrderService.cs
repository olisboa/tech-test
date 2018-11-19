using System.Collections.Generic;
using System.Linq;

namespace AnyCompany
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        /// <summary>
        /// Represent Dependency Injection for the required respositories
        /// </summary>
        /// <param name="orderRepository"></param>
        /// <param name="customerRepository"></param>
        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Place an order, linked to a customer
        /// </summary>
        /// <param name="order"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool PlaceOrder(Order order, int customerId)
        {
            var result = false;

            if (order == null) return result;
            Customer customer = _customerRepository.Load(customerId);
            if (customer != null)
            {
                if (order.Amount == 0)
                    return result;

                if (customer.Country == "UK")
                    order.VAT = 0.2d;
                else
                    order.VAT = 0;

                order.CustomerId = customerId;

                _orderRepository.Save(order);

                result = true;
            }
            return result;
        }

        /// <summary>
        /// Load all customers and their linked orders
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerOrder> LoadAllCustomerOrders()
        {
            var orders = _orderRepository.LoadAll()?.GroupBy(o => o.CustomerId);
            var customers = _customerRepository.LoadAll();
            foreach (var customer in customers ?? Enumerable.Empty<Customer>())
            {
                yield return new CustomerOrder()
                {
                    Customer = customer,
                    Orders = orders.Where(o => o.Key == customer.CustomerId)?.FirstOrDefault()?.ToList()
                };
            }
        }
    }
}
