using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Results.Groups
{
    public class GroupBasicDataResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public int NoOfUsers { get; set; }
    }
}
