using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AnyCompany
{
    public class OrderRepository : IOrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public void Save(Order order)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT)", connection);

            command.Parameters.AddWithValue("@OrderId", order.OrderId);
            command.Parameters.AddWithValue("@Amount", order.Amount);
            command.Parameters.AddWithValue("@VAT", order.VAT);

            command.ExecuteNonQuery();

            connection.Close();
        }

        /// <summary>
        /// Load Order with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order Load(int id)
        {
            var orders = ExecuteQuery("SELECT * FROM Order WHERE OrderId = " + id);
             return orders.FirstOrDefault();
        }

        /// <summary>
        /// Load all orders
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> LoadAll()
        {
            var orders = ExecuteQuery("SELECT * FROM Order");
            return orders;
        }

        /// <summary>
        /// Load Order for a gien customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IEnumerable<Order> LoadByCustomerId(int customerId)
        {
            var orders = ExecuteQuery("SELECT * FROM Order WHERE CustomerId = " + customerId);
            return orders;
        }

        
        private IEnumerable<Order> ExecuteQuery(string query)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return new Order
                        {
                            OrderId = int.Parse(reader["OrderId"].ToString()),
                            CustomerId = int.Parse(reader["CustomerId"].ToString()),
                            VAT = double.Parse(reader["VAT"].ToString()),
                            Amount = double.Parse(reader["Country"].ToString())
                        };
                    }
                }
            }
        }
    }
}
