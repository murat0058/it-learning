/**
* @desc Task list for group directive
* @example <itl-group-task-list ng-init="taskListVm.activate(taskListJson)"></itl-group-task-list>
*/
(function () {

    angular
        .module('app.groups')
        .directive('itlGroupTaskList', itlGroupTaskList);

    function itlGroupTaskList() {

        var directive = {
            templateUrl: '/src/js/app/Tasks/templates/task-list.html',
            restrict: 'E',
            transclude: true,
            controller: GroupTasksController,
            controllerAs: 'taskListVm',
            bindToController: true
        };

        return directive;
    }

    GroupTasksController.$inject = ['uiFeaturesService', 'loadingIndicatorService'];

    function GroupTasksController(uiFeaturesService, loadingIndicatorService) {

        var taskListVm = this,
            loadingMessage = "Ładuję zadania...";

        taskListVm.tasks = [];
        taskListVm.loadingIndicator = loadingIndicatorService.getIndicator(loadingMessage);

        taskListVm.activate = activate;

        ////////////////////////////

        function activate(tasksList) {
            if (tasksList && tasksList.IsSuccess) {

                var groupTasks = tasksList.Item;

                angular.forEach(groupTasks, function (value, key) {
                    value.Language = uiFeaturesService.languageEnumDisplayName[value.Language];
                    value.style = {
                        'background-color': uiFeaturesService.languageToColorMappings[value.Language]
                    };
                });

                taskListVm.tasks = groupTasks;

                taskListVm.loadingIndicator.Hide();
            } else {
                taskListVm.loadingIndicator.SetLoaded(tasksList.ErrorMessage);
            }
        }
    }
})();