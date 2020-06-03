using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Enums;

namespace Buisness.Contracts.Models.Requests
{
    public class RequestModel
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public string ManagerId { get; set; }

        public string ClientName { get; set; }
        public string ObjectName { get; set; }
        public string ManagerName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string Status { get; set; }
        public ActivityStatus StatusDesctiption { get => CodesDictionary.ActivityType( this.Status ); }

        public string Description { get; set; }
        public string Result { get; set; }
    }
}
