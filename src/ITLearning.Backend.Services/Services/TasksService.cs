using System;
using System.Collections.Generic;
using ITLearning.Contract.Data.Model.Tasks;
using ITLearning.Contract.Data.Requests.Tasks;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Services;
using ITLearning.Contract.Enums;

namespace ITLearning.Backend.Business.Services
{
    public class TasksService : ITasksService
    {
        public void Create(CreateTaskRequestData requestData)
        {
            throw new NotImplementedException();
        }

        public CommonResult<List<TaskListItemData>> GetForGroup(int groupId)
        {
            return CommonResult<List<TaskListItemData>>.Success(new List<TaskListItemData>
            {
                new TaskListItemData() { Id = 0, Name = "Wprowadzenie do C#", GroupName = "Ludzie i c#", IsCompleted = false, Language = LanguageEnum.CSharp.ToString(), GroupId = 1 },
                new TaskListItemData() { Id = 1, Name = "Wprowadzenie do JS", GroupName = "Ludzie i js", IsCompleted = false, Language = LanguageEnum.JavaScript.ToString(), GroupId = 1 },
                new TaskListItemData() { Id = 2, Name = "Wprowadzenie do JAVA", GroupName = "Ludzie i java", IsCompleted = true, Language = LanguageEnum.Other.ToString(), GroupId = 1 }
            });
        }

        public CommonResult<List<TaskListItemData>> GetLatest(int count)
        {
            throw new NotImplementedException();
        }
    }
}