using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;

namespace AnyCompany.Tests.Helper
{
    /// <summary>
    /// Helper static class to create required test data
    /// </summary>
    public static class TestObjectCreator
    {
        private static Fixture any = new Fixture();

        /// <summary>
        /// Returns instance of the given type where the values of the instance are irrelevant
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Instance<T>()
        {
            return any.Create<T>();
        }

        /// <summary>
        /// Returns instance of Order with the given values
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static Order Order(int Id = 1, double amount = 0)
        {
            return new Order()
            {
                OrderId = Id,
                Amount = amount           
            };
        }

        /// <summary>
        /// Returns instance of Customer with the given values
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static Customer UKCustomer(int Id = 1)
        {
            return new Customer()
            {
                CustomerId = Id,
                Country = "UK",
                Name = "Alex Jones",
                DateOfBirth = new DateTime()
            };
        }

        /// <summary>
        /// Returns instance of Customer with the given values
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public static Customer NonUKCustomer(int Id = 2, string country = "USA")
        {
            return new Customer()
            {
                CustomerId = Id,
                Country = country,
                Name = "Davia Brown",
                DateOfBirth = new DateTime()
            };
        }

        /// <summary>
        /// Returns Customer with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int id)
        {
            return GetAllCustomers().FirstOrDefault(c => c.CustomerId == id);
        }

        /// <summary>
        /// Returns instance of Order with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Order GetOrder(int id)
        {
            return GetAllOrders().FirstOrDefault(o => o.OrderId == id);
        }

        /// <summary>
        /// Returns Orders with the given customer id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IEnumerable<Order> GetOrderByCustomerId(int id)
        {
            return GetAllOrders().Where(o => o.CustomerId == id);
        }


        /// <summary>
        /// Returns all Customers
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Customer> GetAllCustomers()
        {
            yield return new Customer()
                {
                    CustomerId = 1,
                    Country = "UK",
                    Name = "Alex Jones",
                    DateOfBirth = new DateTime()
                };

            yield return new Customer()
            {
                CustomerId = 2,
                Country = "USA",
                Name = "Davia Brown",
                DateOfBirth = new DateTime()
            };

            yield return new Customer()
            {
                CustomerId = 3,
                Country = "USA",
                Name = "Timothy Brown",
                DateOfBirth = new DateTime()
            };

            yield return new Customer()
            {
                CustomerId = 4,
                Country = "UK",
                Name = "Timothy Ada",
                DateOfBirth = new DateTime()
            };

        }

        /// <summary>
        /// Returns all Orders
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Order> GetAllOrders()
        {
            yield return new Order()
            {
                OrderId = 1,
                CustomerId = 1,
                Amount = 12d,
                VAT = 0.2d
            };

            yield return new Order()
            {
                OrderId = 2,
                CustomerId = 1,
                Amount = 12,
                VAT = 0.2d
            };

            yield return new Order()
            {
                OrderId = 3,
                CustomerId = 2,
                Amount = 100d,
                VAT = 0
            };

            yield return new Order()
            {
                OrderId = 4,
                CustomerId = 2,
                Amount = 100d,
                VAT = 0
            };

            yield return new Order()
            {
                OrderId = 5,
                CustomerId = 2,
                Amount = 100d,
                VAT = 0
            };
            yield return new Order()
            {
                OrderId = 6,
                CustomerId = 3,
                Amount = 50d,
                VAT = 0
            };

        }
    }
}