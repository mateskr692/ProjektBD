using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Buisness.Contracts.Models.Activities
{
    public class WorkerActivityFilterModel
    {
        public string RegistrationDate { get; set; }
        public string ActivityType { get; set; }
        public string WorkerId { get; set; }
        public string ActivityStatus { get; set; } = CodesDictionary.ActivityType( Common.Enums.ActivityStatus.Open );
    }
}
