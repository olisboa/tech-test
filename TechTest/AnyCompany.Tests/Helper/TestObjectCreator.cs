using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;

namespace AnyCompany.Tests.Helper
{
    public static class TestObjectCreator
    {
        private static Fixture any = new Fixture();

        public static T Instance<T>()
        {
            return any.Create<T>();
        }

        public static Order Order(int Id = 1, double amount = 0)
        {
            return new Order()
            {
                OrderId = Id,
                Amount = amount           
            };
        }

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

        public static Customer GetCustomer(int id)
        {
            return GetAllCustomers().FirstOrDefault(c => c.CustomerId == id);
        }

        public static Order GetOrder(int id)
        {
            return GetAllOrders().FirstOrDefault(o => o.OrderId == id);
        }

        public static IEnumerable<Order> GetOrderByCustomerId(int id)
        {
            return GetAllOrders().Where(o => o.CustomerId == id);
        }

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