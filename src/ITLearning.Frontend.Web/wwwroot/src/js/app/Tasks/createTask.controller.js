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

            model.AvailableLanguages.forEach(function (item) {
                item.Name = uiFeaturesService.languageEnumDisplayName[item.Name];
            });
            createTaskVm.availableLanguages = model.AvailableLanguages;
            createTaskVm.selectedLanguage = createTaskVm.availableLanguages[0];
        };

        createTaskVm.addBranch = function (form) {
            if (form.$valid) {
                createTaskVm.branches.sort(function (a, b) {
                    return a.id > b.id;
                });

                var newId = createTaskVm.branches[createTaskVm.branches.length - 1].id + 1;

                createTaskVm.branches.push({
                    id: newId,
                    name: createTaskVm.branchName,
                    description: createTaskVm.branchDescription
                });

                createTaskVm.branchName = '';
                createTaskVm.branchDescription = '';
            }
        };

        createTaskVm.deleteBranch = function (branchId) {
            for (var i = 0; i < createTaskVm.branches.length; i++) {
                if (createTaskVm.branches[i].id === branchId) {
                    createTaskVm.branches.splice(i, 1);
                    break;
                }
            }
        };

        createTaskVm.addTask = function (form) {
            if (form.$valid) {
                createTask();
            }
        };

        function createTask() {
            var request = {
                title: createTaskVm.taskName,
                description: createTaskVm.taskDescription,
                isActive: createTaskVm.isTaskActive,
                selectedLanguage: createTaskVm.selectedLanguage.Id,
                selectedGroupId: createTaskVm.selectedUserGroup.Id,
                branches: createTaskVm.branches
            };

            return tasksService.createTask(request);
        }
    }
})();