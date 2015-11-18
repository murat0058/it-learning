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

    TasksController.$inject = ['taskService', 'uiFeaturesService'];

    function TasksController(taskService, uiFeaturesService) {

        var taskListVm = this;

        taskListVm.isLoadingIndicatorVisible = true;
        taskListVm.tasks = [];

        activate();

        ////////////////////////////

        function activate() {

            taskListVm.tasks = [
                {
                    id: 0,
                    name: 'Test 1',
                    groupId: 99,
                    language: 'C#',
                    groupName: 'Grupa testowa 123',
                    isCompleted: true,
                    style: {
                        'background-color': uiFeaturesService.languageToColorMappings['C#']
                    }
                },
                {
                    id: 1,
                    name: 'Test 2',
                    groupId: 99,
                    language: 'JAVA',
                    groupName: 'Grupa testowa 345',
                    isCompleted: false,
                    style: {
                        'background-color': uiFeaturesService.languageToColorMappings['JAVA']
                    }
                },
                {
                    id: 3,
                    name: 'Test 3',
                    groupId: 99,
                    language: 'JS',
                    groupName: 'Grupa testowa 654',
                    isCompleted: true,
                    style: {
                        'background-color': uiFeaturesService.languageToColorMappings['JS']
                    }
                }
            ];

        };
    }

})();