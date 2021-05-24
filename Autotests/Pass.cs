using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;

namespace Autotests
{
    public class Pass
    {
        public string Guid { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public string PersonPatronymic { get; set; }
        public string PassportNumber { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}