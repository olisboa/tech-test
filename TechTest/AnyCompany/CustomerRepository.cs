using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AnyCompany.Helper;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        /// <summary>
        /// Loads Customer with the given id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static Customer Load(int customerId)
        {
            var customer = SQLHelper.ExecuteQuery("SELECT * FROM Customer WHERE CustomerId = " + customerId, ConnectionString);
            return ExtractCustomer(customer)?.FirstOrDefault();
        }

        /// <summary>
        /// Loads all Customer Objects
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<Customer> LoadAll()
        {
            var customers = SQLHelper.ExecuteQuery("SELECT * FROM Customer", ConnectionString);
            return ExtractCustomer(customers);
        }

        /// <summary>
        /// Maps IEnumerable<IDataRecord> to Customer objects
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static IEnumerable<Customer> ExtractCustomer(IEnumerable<IDataRecord> data)
        {
            foreach (var row in data ?? Enumerable.Empty<IDataRecord>())
            {
                yield return new Customer()
                {
                    Name = row["Name"].ToString(),
                    DateOfBirth = DateTime.Parse(row["DateOfBirth"].ToString()),
                    Country = row["Country"].ToString()
                };
            }
       }
    }
}
