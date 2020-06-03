using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Enums;

namespace Buisness.Contracts.Models.Activities
{
    public class ActivityModel
    {
        public long Id { get; set; }
        public int? SequanceNumber { get; set; }
        public string Description { get; set; }
        public string Result { get; set; }
        public string Status { get; set; }
        public ActivityStatus? StatusDescription { get => CodesDictionary.ActivityType( this.Status ); }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string WorkerName { get; set; }

        public string ActivityTypeCode { get; set; }
        public string ActivityTypeName { get; set; }
        public string WorkerId { get; set; }
        public long RequestId { get; set; }
    }
}
