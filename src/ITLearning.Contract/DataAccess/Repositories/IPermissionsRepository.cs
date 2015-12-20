using ITLearning.Contract.Data.Model.Administration;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Enums;
using System.Threading.Tasks;

namespace ITLearning.Contract.DataAccess.Repositories
{
    public interface IPermissionsRepository
    {
        CommonResult<ClaimsData> GetClaimsForUser(string userName);
        CommonResult AddClaim(string userName, ClaimTypeEnum type, ClaimValueEnum value);
        CommonResult RemoveClaim(string userName, ClaimTypeEnum type, ClaimValueEnum value);
    }
}