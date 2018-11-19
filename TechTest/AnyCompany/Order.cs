namespace AnyCompany
{
    public class Order
    {
        public int OrderId { get; set; }

        //added CustomerId to link to Customer object as a 1 -> M relationship.  Every order belongs to exactly one customer, 
        //and one customer can have many orders
        public int CustomerId { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }
    }
}
