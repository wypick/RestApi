using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data;

namespace RestApi
{
    public class DbUtils
    {
        public static string bd = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = DB; Integrated Security = True; Persist Security Info = False; Pooling = False; MultipleActiveResultSets = False; Connect Timeout = 60; Encrypt = False; TrustServerCertificate = True";

        public static Pass Get(string guid)
        {
            var sql = $@"SELECT guid, personName, personSurname, personPatronymic, passportNumber 
                        FROM Passes 
                        WHERE guid = '{guid}'";

            using (SqlConnection connection = new SqlConnection(bd))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
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
                        };
                    }

                    // Call Close when done reading.
                    reader.Close();

                    return result;
                }
            };
        }

        public static string Post(Pass pass)
        {
            var windowsTime = "19-05-2021";
            var time = DateTime.Parse(windowsTime);
            // DateTime.ParseExact(mystring, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            /*DateTime date = DateTime.Now;
            string time = date.ToString("yyyy-MM-dd HH:mm:ss.fff");*/

            var guid = Utils.GetValidGuid();
            var sql = $@"INSERT INTO [dbo].[Passes] ([GUID], [PersonName], [PersonSurname], [PersonPatronymic], [PassportNumber], [DateFrom], [DateTo]) 
                            VALUES ('{guid}', '{pass.PersonName}', '{pass.PersonSurname}', 
                                '{pass.PersonPatronymic}', '{pass.PassportNumber}', '{pass.DateFrom.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{pass.DateTo.ToString("yyyy-MM-dd HH:mm:ss.fff")}')";
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

        public static void Put(Pass pass)
        {
            var sql = $@"UPDATE Passes
                        SET [PersonName] = '{pass.PersonName}', [PersonSurname] = '{pass.PersonSurname}', 
                                [PersonPatronymic] = '{pass.PersonPatronymic}', [PassportNumber] = '{pass.PassportNumber}' 
                        WHERE guid = '{pass.Guid}'";
            using (SqlConnection connection = new SqlConnection(bd))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            };
        }

        public static void Delete(string guid)
        {
            var sql = $@"DELETE FROM Passes
                        WHERE guid = '{guid}'";
            using (SqlConnection connection = new SqlConnection(bd))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            };
        }
    }
}
