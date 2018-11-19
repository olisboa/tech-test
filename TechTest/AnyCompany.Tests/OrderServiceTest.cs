
using System.Linq;
using AnyCompany.Tests.Helper;
using NSubstitute;
using NUnit.Framework;

namespace AnyCompany.Tests
{
    ///TODO: Given the time constraint, Could possibly write test to cover when an exception is thrown during and of the Repositories Save/Load methods in the OrderService
    [TestFixture]
    public class OrderServiceTest
    {
        private IOrderRepository orderDb;
        private ICustomerRepository customerDb;

        [SetUp]
        public void Init()
        {
            //create a fake of the order and customer repository so that our 
            //test would not fail on the repository implementation details... so that we dont need to access the real db.
            orderDb = Substitute.For<IOrderRepository>();
            customerDb = Substitute.For<ICustomerRepository>();
        }


        [Test]
        public void WhenANewOrderIsPlaced_WithNullCustomerId_ShouldNotCreateOrder()
        {
            //GIVEN
            var orderService = new OrderService(orderDb, customerDb);
            //create order instance
            var order = TestObjectCreator.Instance<Order>();

            //WHEN
            var result = orderService.PlaceOrder(order, 0);

            //THEN
            //the mock obected created by NSubstitute record all calls it received. We call its DidNotReceive() method which 
            //checks whether the Save() method was not called with the given order.
            orderDb.DidNotReceive().Save(order);
            Assert.IsFalse(result);
        }

        [Test]
        public void WhenANewOrderIsPlaced_WithNullOrder_ShouldNotCreateOrder()
        {
            //GIVEN
            var orderService = new OrderService(orderDb, customerDb);

            //WHEN
            var result = orderService.PlaceOrder(null, 0);

            //THEN
            //the mock obected created by NSubstitute record all calls it received. We call its DidNotReceive() method which 
            //checks whether the Save() method was not called with the given order.
            orderDb.DidNotReceive().Save(TestObjectCreator.Instance<Order>());
            Assert.IsFalse(result);
        }

        [Test]
        public void WhenANewOrderIsPlaced_ShouldInsertOrderToDatabase()
        {
            //GIVEN
            //set customerDb.Load() method to return a Customer Instance
            customerDb.Load(1).Returns(TestObjectCreator.Instance<Customer>());

            var orderService = new OrderService(orderDb, customerDb);
            //create order instance
            var order = TestObjectCreator.Instance<Order>();

            //WHEN
            var result = orderService.PlaceOrder(order, 1);

            //THEN
            //the mock obected created by NSubstitute record all calls it received. We call its Received() method which 
            //checks whether the Save() method was called with the given order.
            orderDb.Received(1).Save(order);

            Assert.IsTrue(result);
        }

        [Test]
        public void GivenCustomerCountryISUK_WhenOrderIsSucessfullyPlaced_VATShouldBeTwentyPercent()
        {
            //GIVEN
            //create order instance
            var order = TestObjectCreator.Order(amount: 12d);
            var customerId = 1;

            //set customerDb.Load() method to return a Customer Instance
            customerDb.Load(customerId).Returns(TestObjectCreator.UKCustomer(customerId));
            orderDb.LoadByCustomerId(customerId).Returns(TestObjectCreator.GetOrderByCustomerId(customerId));

            var orderService = new OrderService(orderDb, customerDb);

            //WHEN
            orderService.PlaceOrder(order, customerId);

            //THEN
            var actualOrder = orderDb.LoadByCustomerId(customerId)?.FirstOrDefault(o => o.Amount == order.Amount);
            Assert.AreEqual(actualOrder.VAT, 0.2d);

            var actualCustomer = customerDb.Load(customerId);
            Assert.AreEqual(actualCustomer.Country, "UK");
        }

        [Test]
        public void GivenCustomerCountryISNONUK_WhenOrderIsSucessfullyPlaced_VATShouldBeZeroPercent()
        {
            //GIVEN
            //create order instance
            var order = TestObjectCreator.Order(amount: 50d);
            var customerId = 3;

            //set customerDb.Load() method to return a Customer Instance
            customerDb.Load(customerId).Returns(TestObjectCreator.NonUKCustomer(customerId));
            orderDb.LoadByCustomerId(customerId).Returns(TestObjectCreator.GetOrderByCustomerId(customerId));

            var orderService = new OrderService(orderDb, customerDb);

            //WHEN
            orderService.PlaceOrder(order, customerId);

            //THEN
            var actual = orderDb.LoadByCustomerId(customerId)?.FirstOrDefault(o => o.Amount == order.Amount);
            Assert.AreEqual(actual.VAT, 0);

            var actualCustomer = customerDb.Load(customerId);
            Assert.AreEqual(actualCustomer.Country, "USA");
        }


        [Test]
        public void GivenNoCustomerRecord_CustomerWithOrderRecordIsNull()
        {
            //GIVEN
            //set customerDb.LoadAll() method to return a Customers
            customerDb.LoadAll().Returns(Enumerable.Empty<Customer>());
            var orderService = new OrderService(orderDb, customerDb);

            //WHEN
            var customerOrders = orderService.LoadAllCustomerOrders();

            //THEN
           Assert.AreEqual(customerOrders.Count(), 0);
        }

        [Test]
        public void GivenCustomerRecordsExist_CustomerWithOrderRecordIsNotEmpy()
        {
            //GIVEN
            //set customerDb.LoadAll() method to return a Customers
            var testCustomers = TestObjectCreator.GetAllCustomers();
            var testOrders = TestObjectCreator.GetAllOrders();
            customerDb.LoadAll().Returns(testCustomers);
            orderDb.LoadAll().Returns(testOrders);

            var orderService = new OrderService(orderDb, customerDb);

            //WHEN
            var customerOrders = orderService.LoadAllCustomerOrders();

            //THEN
            Assert.AreEqual(customerOrders.Count(), testCustomers.Count());
            foreach (var customerOrder in customerOrders)
            {
                Assert.AreEqual(customerOrder.Orders== null? 0 :customerOrder.Orders.Count(), testOrders.Where(o => o.CustomerId == customerOrder.Customer.CustomerId)?.Count());
            }       
        }
    }
}
