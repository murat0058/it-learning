using ITLearning.Backend.Business.Validators;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Groups;
using ITLearning.Contract.DataAccess.Repositories;
using ITLearning.Contract.Services;
using ITLearning.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Backend.Business.Services
{
    public class GroupsService : IGroupsService
    {
        private IGroupsRepository _groupsRepository;

        public GroupsService(IGroupsRepository groupsRepository)
        {
            _groupsRepository = groupsRepository;
        }

        public CommonResult<CreateGroupResult> CreateGroup(CreateGroupRequestData request)
        {
            var validator = new CreateGroupRequestDataValidator();

            request.Password = request.Password.ToBase64();
            request.PasswordConfirmation = request.PasswordConfirmation.ToBase64();

            var validationResult = validator.Validate(request);

            if (validationResult.IsValid)
            {
                return _groupsRepository.CreateGroup(request);
            }
            else
            {
                return CommonResult<CreateGroupResult>.Failure(validationResult.Errors.First().ErrorMessage);
            }
        }
    }
}
