using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AnyCompany.Helper
{
    public static class SQLHelper
    {
        /// <summary>
        /// Helper Methos to SQLQuery
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IEnumerable<IDataRecord> ExecuteQuery(string query, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return (IDataRecord)reader;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Helper Methos to non SQLQuery
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionString"></param>
        public static void ExecuteNonQuery(string query, SqlParameter[] parameters, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    if (parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }           
                    connection.Open();
                    cmd.ExecuteNonQuery();       
                }
            }

        }
    }
}
