﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;

namespace Buisness.Contracts.Models
{
    public class UserInfoModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public UserRoles Role { get; set; }
    }
}
