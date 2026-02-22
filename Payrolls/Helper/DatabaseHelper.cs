using Microsoft.Data.SqlClient;
using Payrolls.Helper;
using System.Data;

namespace Payrolls.Helpers
{
    public static class DatabaseHelper
    {
        public static List<T> ExecuteReader<T>(string connectionString,
                                               string query,
                                               Func<SqlDataReader, T> mapFunction,
                                               params SqlParameter[] parameters)
        {
            var result = new List<T>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.Add(mapFunction(rdr));
                        }
                    }
                }
            }

            return result;
        }
    }
}