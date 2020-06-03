using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Contracts.Models.Requests
{
    public class RequestFiltersModel
    {
        public string ClientName { get; set; }
        public string ManagerName { get; set; }
        public string ObjectName { get; set; }
        public string Status { get; set; }
        public string RegistrationDate { get; set; }
    }
}
