using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi
{
    public class DbUtils
    {
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
            var sql = $@"INSERT {guid}, {pass.PersonName}, {pass.PersonSurname}, 
                                {pass.PersonPatronymic}, {pass.PassportNumber} 
                        INTO Passes";
            return guid;
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
