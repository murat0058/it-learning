using ITLearning.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Attributes
{
    public class ClaimMappingAttribute : Attribute
    {
        public ClaimMappingAttribute(ClaimTypeEnum claimType, ClaimValueEnum claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }

        public ClaimTypeEnum ClaimType { get; set; }
        public ClaimValueEnum ClaimValue { get; set; }

    }
}
