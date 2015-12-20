using ITLearning.Contract.Data.Results;
using ITLearning.Contract.DataAccess.Repositories;
using ITLearning.Contract.Enums;
using ITLearning.Contract.Services;
using System.Linq;
using ITLearning.Contract.Attributes;
using ITLearning.Contract.Data.Model.Administration;
using System;

namespace ITLearning.Backend.Business.Services
{
    public class PermissionsService : IPermissionsService
    {
        private IPermissionsRepository _permissionsRepository;

        public PermissionsService(IPermissionsRepository permissionsRepository)
        {
            _permissionsRepository = permissionsRepository;
        }

        public CommonResult<ClaimsData> GetClaimsForUser(string userName)
        {
            return _permissionsRepository.GetClaimsForUser(userName);
        }

        public CommonResult AddClaim(string userName, ClaimTypeEnum type, ClaimValueEnum value)
        {
            var addClaimResult = _permissionsRepository.AddClaim(userName, type, value);

            return addClaimResult;
        }

        public CommonResult RemoveClaim(string userName, ClaimTypeEnum type, ClaimValueEnum value)
        {
            var removeClaimResult = _permissionsRepository.RemoveClaim(userName, type, value);

            return removeClaimResult;
        }

        public CommonResult UpdateClaims(string userName, ClaimsData claims)
        {
            var propertiesInfo = claims.GetType().GetProperties();

            foreach (var property in propertiesInfo)
            {
                var claimMapping = (ClaimMappingAttribute)property.GetCustomAttributes(typeof(ClaimMappingAttribute), false).First();

                if ((bool)property.GetValue(claims) == true)
                {
                    AddClaim(userName, claimMapping.ClaimType, claimMapping.ClaimValue);
                }
                else
                {
                    RemoveClaim(userName, claimMapping.ClaimType, claimMapping.ClaimValue);
                }
            }

            return CommonResult.Success();
        }
    }
}
