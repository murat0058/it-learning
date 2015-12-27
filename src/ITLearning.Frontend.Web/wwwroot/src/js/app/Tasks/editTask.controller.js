(function () {

    angular
        .module('app.tasks')
        .controller('EditTaskController', EditTaskController);

    EditTaskController.$inject = ['tasksService', 'uiFeaturesService'];

    function EditTaskController(tasksService, uiFeaturesService) {
        var editTaskVm = this;

        editTaskVm.availableLanguages;
        editTaskVm.selectedLanguage;
        editTaskVm.selectedUserGroup;
        editTaskVm.Description;
        editTaskVm.taskName;
        editTaskVm.taskDescription;

        editTaskVm.isBranchEditModeEnabled = false;
        editTaskVm.editedBranchId;
        editTaskVm.isTaskActive =  true;
        editTaskVm.isAddBranchPanelVisible = false;

        editTaskVm.branches = [];

        editTaskVm.init = function (model) {
            editTaskVm.selectedUserGroup = model.SelectedGroup;

            editTaskVm.branches = model.Branches;

            for (var i = 0; i < editTaskVm.branches.length; i++) {
                editTaskVm.branches[i].id = i + 1;
            }

            editTaskVm.taskDescription = model.Description;
            editTaskVm.taskName = model.Name;
            editTaskVm.taskId = model.Id;

            editTaskVm.isTaskActive = model.IsActive;

            model.AvailableLanguages.forEach(function (item) {
                item.Name = uiFeaturesService.languageEnumDisplayName[item.Name];
            });
            editTaskVm.availableLanguages = model.AvailableLanguages;
            editTaskVm.selectedLanguage = model.SelectedLanguage;
        };

        editTaskVm.addBranch = function (form) {
            if (form.$valid) {

                if (editTaskVm.isBranchEditModeEnabled) {
                    var branchToEdit = editTaskVm.branches.filter(function (item, index) {
                        return item.id === editTaskVm.editedBranchId;
                    })[0];

                    branchToEdit.name = editTaskVm.branchName;
                    branchToEdit.description = editTaskVm.branchDescription;

                    editTaskVm.isBranchEditModeEnabled = false;
                    editTaskVm.isAddBranchPanelVisible = false;

                    editTaskVm.branchName = '';
                    editTaskVm.branchDescription = '';

                    return;
                }

                editTaskVm.branches.sort(function (a, b) {
                    return a.id > b.id;
                });

                var newId = editTaskVm.branches[editTaskVm.branches.length - 1].id + 1;

                editTaskVm.branches.push({
                    id: newId,
                    name: editTaskVm.branchName,
                    description: editTaskVm.branchDescription
                });

                editTaskVm.branchName = '';
                editTaskVm.branchDescription = '';
            }
        };

        editTaskVm.deleteBranch = function (branchId) {
            for (var i = 0; i < editTaskVm.branches.length; i++) {
                if (editTaskVm.branches[i].id === branchId) {
                    editTaskVm.branches.splice(i, 1);
                    break;
                }
            }
        };

        editTaskVm.editBranch = function (branchId) {
            var branchToEdit = editTaskVm.branches.filter(function (item, index) {
                return item.id === branchId;
            })[0];

            editTaskVm.branchName = branchToEdit.name;
            editTaskVm.branchDescription = branchToEdit.description;

            editTaskVm.editedBranchId = branchToEdit.id;
            editTaskVm.isBranchEditModeEnabled = true;
            editTaskVm.isAddBranchPanelVisible = true;
        };

        editTaskVm.cancelEditBranch = function () {
            editTaskVm.branchName = "";
            editTaskVm.branchDescription = "";
            editTaskVm.isBranchEditModeEnabled = false;
        };

        editTaskVm.addTask = function (form) {
            if (form.$valid) {
                editTask();
            }
        };

        function editTask() {
            var request = {
                id: editTaskVm.taskId,
                name: editTaskVm.taskName,
                description: editTaskVm.taskDescription,
                isActive: editTaskVm.isTaskActive,
                editedLanguage: editTaskVm.selectedLanguage.Id,
                branches: editTaskVm.branches
            };

            return tasksService.editTask(request);
        }
    }
})();