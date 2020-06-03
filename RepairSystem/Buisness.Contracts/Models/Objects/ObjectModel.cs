using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Contracts.Models.Objects
{
    public class ObjectModel
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
