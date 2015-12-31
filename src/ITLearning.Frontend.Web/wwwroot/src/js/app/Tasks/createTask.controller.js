(function () {

    angular
        .module('app.tasks')
        .controller('CreateTaskController', CreateTaskController);

    CreateTaskController.$inject = ['tasksService', 'uiFeaturesService'];

    function CreateTaskController(tasksService, uiFeaturesService) {
        var createTaskVm = this;

        createTaskVm.availableLanguages;
        createTaskVm.selectedLanguage;
        createTaskVm.userGroups;
        createTaskVm.selectedUserGroup;
        createTaskVm.repositoryLink;
        createTaskVm.CreatedTaskId;
        createTaskVm.shouldActivateTask;
        createTaskVm.branchNameMustBeUnique = false;

        createTaskVm.IsPostInProgress = false;

        createTaskVm.IsFirstStepFinished = false;
        createTaskVm.IsSecondStepFinished = false;

        createTaskVm.isTaskActive = true;
        createTaskVm.isAddBranchPanelVisible = false;

        createTaskVm.branches = [
            {
                id: 1,
                name: 'master',
                description: 'Główny branch. Nie można go usunąć. Wrzuć swój kod, od którego użytkownicy mają zacząć.'
            }
        ];

        createTaskVm.init = function (model) {
            createTaskVm.userGroups = model.UserGroups;
            createTaskVm.selectedUserGroup = createTaskVm.userGroups[0];

            createTaskVm.repositoryLink = model.RepositoryLink;

            model.AvailableLanguages.forEach(function (item) {
                item.Name = uiFeaturesService.languageEnumDisplayName[item.Name];
            });
            createTaskVm.availableLanguages = model.AvailableLanguages;
            createTaskVm.selectedLanguage = createTaskVm.availableLanguages[0];
        };

        createTaskVm.addBranch = function (form) {
            if (form.$valid) {
                createTaskVm.branchNameMustBeUnique = false;

                var branchExist = createTaskVm.branches.filter(function (item) {
                    return item.name == createTaskVm.branchName;
                });

                if (branchExist.length > 0) {
                    createTaskVm.branchNameMustBeUnique = true;
                    return;
                }

                createTaskVm.branches.sort(function (a, b) {
                    return a.id > b.id;
                });

                var newId = createTaskVm.branches[createTaskVm.branches.length - 1].id + 1;

                createTaskVm.branches.push({
                    id: newId,
                    name: createTaskVm.branchName,
                    description: createTaskVm.branchDescription
                });

                tasksService.addBranch(createTaskVm.CreatedTaskId, createTaskVm.branchName, createTaskVm.branchDescription);

                createTaskVm.branchName = '';
                createTaskVm.branchDescription = '';
            }
        };

        createTaskVm.deleteBranch = function (branchId) {
            var branchName;

            for (var i = 0; i < createTaskVm.branches.length; i++) {
                if (createTaskVm.branches[i].id === branchId) {
                    branchName = createTaskVm.branches[i].name;
                    createTaskVm.branches.splice(i, 1);
                    break;
                }
            }

            tasksService.deleteBranch(createTaskVm.CreatedTaskId, branchName);
        };

        createTaskVm.addTask = function (form) {
            if (form.$valid) {
                createTask();
            }
        };

        function createTask() {
            var request = {
                name: createTaskVm.taskName,
                description: createTaskVm.taskDescription,
                isActive: createTaskVm.isTaskActive,
                selectedLanguage: createTaskVm.selectedLanguage.Id,
                selectedGroupId: createTaskVm.selectedUserGroup ? createTaskVm.selectedUserGroup.Id : -1,
            };

            createTaskVm.IsPostInProgress = true;

            return tasksService.createTask(request, taskCreated);
        }

        function taskCreated(data) {
            createTaskVm.CreatedTaskId = data.Item.Id;
            createTaskVm.repositoryLink = data.Item.RepositoryLink;
            createTaskVm.shouldActivateTask = data.Item.ShouldActivateTask;

            createTaskVm.IsFirstStepFinished = true;
        }
    }
})();