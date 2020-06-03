using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;

namespace Buisness.Contracts.Models
{
    public class UserInfoModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; set; }
    }
}
