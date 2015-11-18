using AutoMapper;
using ITLearning.Frontend.Web.Common.Configs;
using ITLearning.Frontend.Web.Contract.DAL.Repositories;
using ITLearning.Frontend.Web.Contract.Data.Results;
using ITLearning.Frontend.Web.Contract.Data.Model.User;
using Microsoft.Framework.OptionsModel;
using System.Linq;

namespace ITLearning.Frontend.Web.DAL.Respoitories
{
    public class UserRepository : IUserRepository
    {
        private readonly IOptions<DatabaseConfiguration> _dbConfiguration;

        public UserRepository(IOptions<DatabaseConfiguration> dbConfiguration)
        {
            _dbConfiguration = dbConfiguration;
        }

        public CommonResult<UserProfileData> GetUserProfile(string userName)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var user = context.Users.First(x => x.UserName == userName);

                return CommonResult<UserProfileData>.Success(Mapper.Map<UserProfileData>(user));
            }
        }

        public CommonResult<UserProfileData> UpdateUserProfile(string userName, UserProfileData requestData)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var user = context.Users.First(x => x.UserName == userName);
                user.FirstName = requestData.FirstName;
                user.LastName = requestData.LastName;
                user.Email = requestData.Email;

                context.SaveChanges();

                return CommonResult<UserProfileData>.Success(Mapper.Map<UserProfileData>(user));
            }
        }

        public CommonResult<UserProfileData> UpdateUserProfileImage(string userName, string fileName)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var user = context.Users.First(x => x.UserName == userName);
                user.ImageName = fileName;

                context.SaveChanges();

                return CommonResult<UserProfileData>.Success(Mapper.Map<UserProfileData>(user));
            }
        }
    }
}