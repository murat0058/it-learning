using ITLearning.Contract.Attributes;
using ITLearning.Contract.Enums;

namespace ITLearning.Contract.Data.Model.Administration
{
    public class ClaimsData
    {
        [ClaimMapping(ClaimTypeEnum.Controller, ClaimValueEnum.Controller_HomeController)]
        public bool ControllerHomeController { get; set; }

        [ClaimMapping(ClaimTypeEnum.Controller, ClaimValueEnum.Controller_NewsController)]
        public bool ControllerNewsController { get; set; }

        [ClaimMapping(ClaimTypeEnum.Controller, ClaimValueEnum.Controller_GroupsController)]
        public bool ControllerGroupsController { get; set; }

        [ClaimMapping(ClaimTypeEnum.Controller, ClaimValueEnum.Controller_TasksController)]
        public bool ControllerTasksController { get; set; }

        [ClaimMapping(ClaimTypeEnum.Controller, ClaimValueEnum.Controller_AdministrationController)]
        public bool ControllerAdministrationController { get; set; }

        [ClaimMapping(ClaimTypeEnum.Task, ClaimValueEnum.Task_Create)]
        public bool TaskCreate { get; set; }

        [ClaimMapping(ClaimTypeEnum.News, ClaimValueEnum.News_Create)]
        public bool NewsCreate { get; set; }

        [ClaimMapping(ClaimTypeEnum.Group, ClaimValueEnum.Group_Create)]
        public bool GroupCreate { get; set; }
    }
}
