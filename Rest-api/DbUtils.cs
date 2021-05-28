using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data;
using Dapper;

namespace RestApi
{
    public class DbUtils
    {
        public static string bd = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = DB; Integrated Security = True; Persist Security Info = False; Pooling = False; MultipleActiveResultSets = False; Connect Timeout = 60; Encrypt = False; TrustServerCertificate = True";

        public static Pass Get(string guid)
        {
            var sql = $@"SELECT guid, personName, personSurname, personPatronymic, passportNumber, dateFrom, dateTo 
                        FROM Passes 
                        WHERE guid = '{guid}'";

            using (SqlConnection connection = new SqlConnection(bd))
            {
                connection.Open();

                var result = connection.QuerySingleOrDefault<Pass>(sql);
                /*using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    Pass result = null;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = new Pass()
                        {
                            Guid = reader[0].ToString(),
                            PersonName = reader[1].ToString(),
                            PersonSurname = reader[2].ToString(),
                            PersonPatronymic = reader[3].ToString(),
                            PassportNumber = reader[4].ToString(),
                            DateFrom = (DateTime)reader[5],
                            DateTo = (DateTime)reader[6],
                        };
                    }

                    // Call Close when done reading.
                    reader.Close();

                    return result;
                }*/
                return result;
            };
        }

        public static string Post(Pass pass)
        {
            //var guid = Utils.GetValidGuid();
            var guid = Guid.NewGuid().ToString();
            var sql = $@"INSERT INTO [dbo].[Passes] ([GUID], [PersonName], [PersonSurname], [PersonPatronymic], [PassportNumber], [DateFrom], [DateTo]) 
                            VALUES ('{guid}', '{pass.PersonName}', '{pass.PersonSurname}', 
                                '{pass.PersonPatronymic}', '{pass.PassportNumber}', '{pass.DateFrom.ToString("yyyy-MM-dd HH:mm:ss.fff")}',
                                '{pass.DateTo.ToString("yyyy-MM-dd HH:mm:ss.fff")}')";
            //Add(new TimeSpan(23, 59, 59))
            using (SqlConnection connection = new SqlConnection(bd))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                return guid;
            };
        }

        public static bool Put(Pass pass)
        {
            var sql = $@"UPDATE Passes
                        SET [PersonName] = '{pass.PersonName}', [PersonSurname] = '{pass.PersonSurname}', 
                                [PersonPatronymic] = '{pass.PersonPatronymic}', [PassportNumber] = '{pass.PassportNumber}', 
                                [DateFrom] = '{pass.DateFrom.ToString("yyyy-MM-dd HH:mm:ss.fff")}', [DateTo] = '{pass.DateTo.ToString("yyyy-MM-dd HH:mm:ss.fff")}'  
                        WHERE guid = '{pass.Guid}'";
            using (SqlConnection connection = new SqlConnection(bd))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    return cmd.ExecuteNonQuery() == 1 ? true : false;
                }
            };
        }

        public static bool Delete(string guid)
        {
            var sql = $@"DELETE FROM Passes
                        WHERE guid = '{guid}'";
            using (SqlConnection connection = new SqlConnection(bd))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    return cmd.ExecuteNonQuery() == 1 ? true : false;
                }
            };
        }
    }
}
