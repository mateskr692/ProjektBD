using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Contracts.Models.Activities
{
    public class WorkerActivityFilterModel
    {
        public string RegistrationDate { get; set; }
        public string ActivityType { get; set; }
        public string WorkerId { get; set; }
    }
}
