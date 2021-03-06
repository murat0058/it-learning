﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Requests.Groups
{
    public class CreateGroupRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string UserName { get; set; }
    }
}
