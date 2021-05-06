using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

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

            return new Pass();
        }

        public static string Post(Pass pass)
        {
            var guid = Utils.GetValidGuid();
            var sql = $@"INSERT INTO [dbo].[Passes] ([GUID], [PersonName], [PersonSurname], [PersonPatronymic], [PassportNumber]) VALUES ('{guid}', '{pass.PersonName}', '{pass.PersonSurname}', 
                                '{pass.PersonPatronymic}', '{pass.PassportNumber}')";
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
            var sql = $@"UPDATE {pass.Guid}, {pass.PersonName}, {pass.PersonSurname}, 
                                {pass.PersonPatronymic}, {pass.PassportNumber} 
                        INTO Passes
                        WHERE guid = '{pass.Guid}'";
        }

        public static void Delete(string guid)
        {
            var sql = $@"DELETE * FROM Passes
                        WHERE guid = '{guid}'";
        }

    }
}
