using System.Collections.Generic;

namespace AnyCompany
{
    /// <summary>
    /// Represent Customer Object with it's associated Orders
    /// </summary>
    public class CustomerOrder
    {
        public Customer Customer { get; set; }
        public IList<Order> Orders { get; set; }
    }
}