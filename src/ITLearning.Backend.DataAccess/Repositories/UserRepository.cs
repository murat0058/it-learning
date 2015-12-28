using AutoMapper;
using System.Linq;
using System.IO;
using ITLearning.Contract.DataAccess.Repositories;
using ITLearning.Shared.Configs;
using Microsoft.Extensions.OptionsModel;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Model.User;
using ITLearning.Backend.Database;
using ITLearning.Contract.Providers;
using System;
using System.Collections.Generic;

namespace ITLearning.Backend.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IOptions<DatabaseConfiguration> _dbConfiguration;
        private readonly IAppConfigurationProvider _configurationProvider;

        public UserRepository(IOptions<DatabaseConfiguration> dbConfiguration, IAppConfigurationProvider configurationProvider)
        {
            _dbConfiguration = dbConfiguration;
            _configurationProvider = configurationProvider;
        }

        public CommonResult<UserProfileData> GetUserById(int id)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var user = context.Users.First(x => x.Id == id);

                var mapped = Mapper.Map<UserProfileData>(user);
                mapped.ProfileImagePath = GenerateImagePath(user.ImageName);

                return CommonResult<UserProfileData>.Success(mapped);
            }
        }

        public CommonResult<UserProfileData> GetUserProfile(string userName)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var user = context.Users.First(x => x.UserName == userName);

                var mapped = Mapper.Map<UserProfileData>(user);
                mapped.ProfileImagePath = GenerateImagePath(user.ImageName);

                return CommonResult<UserProfileData>.Success(mapped);
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

                var mapped = Mapper.Map<UserProfileData>(user);
                mapped.ProfileImagePath = GenerateImagePath(user.ImageName);

                return CommonResult<UserProfileData>.Success(mapped);
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

        private string GenerateImagePath(string imageName)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                var croppedProfileImage = _configurationProvider.GetProfileCroppedImagesFolderPath() + imageName;
                if (File.Exists(croppedProfileImage))
                {
                    return _configurationProvider.GetProfileCroppedImagesFolderInternalPath() + imageName;
                }
                else
                {
                    return _configurationProvider.GetProfileOriginalImagesFolderInternalPath() + imageName;
                }
            }
            else
            {
                return _configurationProvider.GetProfileDefaultImagePath();
            }
        }

        public CommonResult<IEnumerable<UserProfileData>> GetAllUsersProfileData()
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var users = context.Users;

                var usersProfileData = new List<UserProfileData>();

                foreach (var user in users)
                {
                    var userProfileData = Mapper.Map<UserProfileData>(user);
                    userProfileData.ProfileImagePath = GenerateImagePath(user.ImageName);

                    usersProfileData.Add(userProfileData);
                }

                return CommonResult<IEnumerable<UserProfileData>>.Success(usersProfileData);
            }
        }

        public CommonResult<string> GetUserPassword(string userName)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var user = context.Users.First(x => x.UserName == userName);

                return CommonResult<string>.Success(user.PasswordHash);
            }
        }
    }
}