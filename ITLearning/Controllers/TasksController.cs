using Newtonsoft.Json;
using ITLearning.Frontend.Web.Contract.Data.Requests;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Controllers
{
    [AllowAnonymous] //TODO refactor to claim ? custom authorization model for api?
    public class TasksController : BaseController
    {


        [HttpPost]
        public JsonResult Latest(GetLatestTasksRequestData requestData)
        {
            var latestTasks = new[]
            {
                new
                {
                    TaskId = 0,
                    GroupId = 1,
                    Language = "C#", //z bazy pewnie jako int, więc todo odpowiadający enum po stronie JSa do zmapowania z powrotem
                    TaskName = "Zadanie testowe",
                    GroupName = "Grupa testowa",
                    IsTaskCompleted = true
                }
            };

            return Json(JsonConvert.SerializeObject(latestTasks));
        }
    }
}
