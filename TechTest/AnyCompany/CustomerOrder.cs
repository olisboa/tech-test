using System.Collections.Generic;

namespace AnyCompany
{
    public class CustomerOrder
    {
        public Customer Customer { get; set; }
        public IList<Order> Orders { get; set; }
    }
}