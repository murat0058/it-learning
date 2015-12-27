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
            scope: {
                userName: '@'
            },
            controller: TasksController,
            controllerAs: 'taskListVm',
            bindToController: true
        };

        return directive;
    }

    TasksController.$inject = ['tasksService', 'uiFeaturesService', 'loadingIndicatorService'];

    function TasksController(tasksService, uiFeaturesService, loadingIndicatorService) {
        var taskListVm = this,
            loadingMessage;

        taskListVm.tasks = [];
        taskListVm.loadingIndicator;

        activate();

        ////////////////////////////

        function activate() {
            loadingMessage = taskListVm.userName ? "Ładuję zadania..." : "Ładuję twoje zadania...";
            taskListVm.loadingIndicator = loadingIndicatorService.getIndicator(loadingMessage);

            getTasks(taskListVm.userName);  
        };

        function getTasks(userName) {
            taskListVm.loadingIndicator.SetLoading(loadingMessage);

            if (userName && userName !== "") {
                return tasksService
                    .getTasksForUser(userName)
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