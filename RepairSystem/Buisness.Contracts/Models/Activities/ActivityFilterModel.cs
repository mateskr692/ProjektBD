using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Contracts.Models.Activities
{
    public class ActivityFilterModel
    {
        public string WorkerName { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string RegistrationDate { get; set; }
    }
}
