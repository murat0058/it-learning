(function () {

    angular
        .module('app.tasks')
        .controller('InstanceSingleTaskViewController', InstanceSingleTaskViewController);

    InstanceSingleTaskViewController.$inject = ['tasksService', 'uiFeaturesService'];

    function InstanceSingleTaskViewController(tasksService, uiFeaturesService) {
        var instanceSingleTaskVM = this;

        instanceSingleTaskVM.Language;
        instanceSingleTaskVM.BackgroundColor;
        instanceSingleTaskVM.BackgroundImage;
        instanceSingleTaskVM.taskInstance;
        instanceSingleTaskVM.branches = [];

        instanceSingleTaskVM.ArchitectureRate = 0;
        instanceSingleTaskVM.OptymizationRate = 0;
        instanceSingleTaskVM.CleanCodeRate = 0;
        instanceSingleTaskVM.Comment;

        instanceSingleTaskVM.init = function (language, branches, taskInstanceData) {
            instanceSingleTaskVM.Language = uiFeaturesService.languageEnumDisplayName[language];
            instanceSingleTaskVM.branches = branches;
            instanceSingleTaskVM.taskInstance = taskInstanceData;

            instanceSingleTaskVM.ArchitectureRate = instanceSingleTaskVM.taskInstance.CodeReview ? instanceSingleTaskVM.taskInstance.CodeReview.ArchitectureRate : 0;
            instanceSingleTaskVM.OptymizationRate = instanceSingleTaskVM.taskInstance.CodeReview ? instanceSingleTaskVM.taskInstance.CodeReview.OptymizationRate : 0;
            instanceSingleTaskVM.CleanCodeRate = instanceSingleTaskVM.taskInstance.CodeReview ? instanceSingleTaskVM.taskInstance.CodeReview.CleanCodeRate : 0;
            instanceSingleTaskVM.Comment = instanceSingleTaskVM.taskInstance.CodeReview ? instanceSingleTaskVM.taskInstance.CodeReview.Comment : "";

            for (var i = 0; i < instanceSingleTaskVM.branches.length; i++) {
                instanceSingleTaskVM.branches[i].id = i + 1;
            }

            instanceSingleTaskVM.BackgroundImage =
                {
                    'background-image': "url('/common/images/tasks/" + uiFeaturesService.languageToTaskHeaderBackgroundImage[instanceSingleTaskVM.Language] + "')"
                };
            instanceSingleTaskVM.BackgroundColor =
                {
                    'background-color': uiFeaturesService.languageToColorMappings[instanceSingleTaskVM.Language]
                };
        };

        instanceSingleTaskVM.showBranch = function (branchName) {
            //TODO refresh
            //instanceSingleTaskVM.branches
            //TODO pass task instance id
            return tasksService.showBranch(branchName);
        };
    }
})();