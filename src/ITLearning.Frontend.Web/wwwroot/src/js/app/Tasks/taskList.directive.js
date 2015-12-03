/**
* @desc Task list common directive
* @example <itl-task-list></itl-task-list>
*/
(function () {

    angular
        .module('app.tasks')
        .directive('itlTaskList', itlTaskList);

    function itlTaskList() {

        var directive = {
            templateUrl: '/src/js/app/Tasks/templates/task-list.html',
            restrict: 'E',
            transclude: true,
            controller: TasksController,
            controllerAs: 'taskListVm',
            bindToController: true
        };

        return directive;
    }

    TasksController.$inject = ['tasksService', 'uiFeaturesService', 'loadingIndicatorService'];

    function TasksController(tasksService, uiFeaturesService, loadingIndicatorService) {

        var taskListVm = this,
            loadingMessage = "Ładuję twoje zadania...";

        taskListVm.tasks = [];
        taskListVm.loadingIndicator = loadingIndicatorService.getIndicator(loadingMessage);

        activate();

        ////////////////////////////

        function activate() {
            getTasks();
        };

        function getTasks() {
            taskListVm.loadingIndicator.SetLoading(loadingMessage);

            return tasksService
                .getLatestTasks()
                .then(function (data) {
                    if (data.IsSuccess) {
                        data.Item.forEach(function (item) {
                            item.Language = uiFeaturesService.languageEnumDisplayName[item.Language];
                            item.style = {
                                'background-color': uiFeaturesService.languageToColorMappings[item.Language]
                            };
                        });
                        taskListVm.tasks = data.Item;
                        taskListVm.loadingIndicator.Hide();
                    } else {
                        taskListVm.loadingIndicator.SetLoaded(data.ErrorMessage);
                    }
                });
        }
    }
})();