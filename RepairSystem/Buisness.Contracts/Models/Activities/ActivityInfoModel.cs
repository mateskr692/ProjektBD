using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Contracts.Models.Activities
{
    public class ActivityInfoModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string ActivityType { get; set; }
        public string RegistrationDate { get; set; }

    }
}
