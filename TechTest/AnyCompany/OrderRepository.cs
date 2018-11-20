using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AnyCompany.Helper;

namespace AnyCompany
{
    public class OrderRepository : IOrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        /// <summary>
        /// Saves Order to database
        /// </summary>
        /// <param name="order"></param>
        public void Save(Order order)
        {
            var parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@OrderId", order.OrderId);
            parameters[1] = new SqlParameter("@Amount", order.Amount);
            parameters[2] = new SqlParameter("@VAT", order.VAT);

            SQLHelper.ExecuteNonQuery("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT)", parameters, ConnectionString);        
        }

        /// <summary>
        /// Load Order with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order Load(int id)
        {
            var orders = SQLHelper.ExecuteQuery("SELECT * FROM Order WHERE OrderId = " + id, ConnectionString);
             return ExtractOrder(orders)?.FirstOrDefault();
        }

        /// <summary>
        /// Load all orders
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> LoadAll()
        {
            var orders = SQLHelper.ExecuteQuery("SELECT * FROM Order", ConnectionString);
            return ExtractOrder(orders);
        }

        /// <summary>
        /// Load Order for a given customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IEnumerable<Order> LoadByCustomerId(int customerId)
        {
            var orders = SQLHelper.ExecuteQuery("SELECT * FROM Order WHERE CustomerId = " + customerId, ConnectionString);
            return ExtractOrder(orders);
        }

        /// <summary>
        /// Maps IEnumerable<IDataRecord> to Order objects
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IEnumerable<Order> ExtractOrder(IEnumerable<IDataRecord> data)
        {
            foreach (var row in data ?? Enumerable.Empty<IDataRecord>())
            {
                yield return new Order()
                {
                    OrderId = int.Parse(row["OrderId"].ToString()),
                    CustomerId = int.Parse(row["CustomerId"].ToString()),
                    VAT = double.Parse(row["VAT"].ToString()),
                    Amount = double.Parse(row["Country"].ToString())
                };
            }
        }
    }
}
