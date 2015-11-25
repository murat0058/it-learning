using AutoMapper;
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

            request.Password = request.Password != null ? 
                request.Password.ToBase64() : 
                request.Password;

            request.PasswordConfirmation = request.PasswordConfirmation != null ? 
                request.PasswordConfirmation.ToBase64() : 
                request.Password;

            var validationResult = validator.Validate(request);

            if (validationResult.IsValid)
            {
                var result = _groupsRepository.CreateGroup(request);

                if (result.IsSuccess)
                {
                    return CommonResult<CreateGroupResult>.Success(result.Item);
                }
                else
                {
                    return CommonResult<CreateGroupResult>.Failure(result.ErrorMessage);
                }
            }
            else
            {
                return CommonResult<CreateGroupResult>.Failure(validationResult.Errors.First().ErrorMessage);
            }
        }

        public CommonResult<IEnumerable<GroupBasicDataResult>> GetGroupBasicData(string userName, int noOfGroups)
        {
            var result = _groupsRepository.GetGroupsBasicData();

            if (result.IsSuccess)
            {
                var userGroups = result.Item.Where(x => x.OwnerUserName == userName).Take(noOfGroups);

                if (userGroups != null && userGroups.Any())
                {
                    return CommonResult<IEnumerable<GroupBasicDataResult>>.Success(userGroups.Select(x => Mapper.Map<GroupBasicDataResult>(x)));
                }
                else
                {
                    return CommonResult<IEnumerable<GroupBasicDataResult>>.Failure("Nie utworzyłeś jeszcze żadnej grupy.");
                }
            }
            else
            {
                return CommonResult<IEnumerable<GroupBasicDataResult>>.Failure(result.ErrorMessage);
            }
        }
    }
}
