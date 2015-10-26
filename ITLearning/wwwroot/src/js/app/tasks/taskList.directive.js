(function () {

    /// <itl-task-list no-of-tasks="5"></itl-task-list>

    angular
        .module('app.tasks')
        .directive('itlTaskList', itlTaskList);

    function itlTaskList() {
        var directive = {
            templateUrl: 'src/js/app/tasks/templates/task-list.html',
            restrict: 'E',
            scope: {
                noOfTasks: '@'
            },
            controller: TasksController,
            controllerAs: 'vm',
            bindToController: true,
            link: link
        };
        return directive;

        function link(scope, element, attrs) {

            //TODO tooltip

        };
    };

    function TasksController() {

        var vm = this;

        vm.tasks = [
            {
                taskId: 0,
                groupId: 1,
                language: 'C#',
                taskName: 'Zadanie testowe',
                groupName: 'Grupa testowa',
                isTaskCompleted: true,
                statusTooltip: 'Zadanie wykonane',
                style: {
                    'background-color': '#ff8d00'
                }
            },
            {
                taskId: 1,
                groupId: 1,
                language: 'C#',
                taskName: 'Zadanie testowe',
                groupName: 'Grupa testowa',
                isTaskCompleted: false,
                statusTooltip: 'Zadanie wykonane',
                style: {
                    'background-color': '#21b2a6'
                }
            },
            {
                taskId: 2,
                groupId: 1,
                language: 'JAVA',
                taskName: 'Zadanie testowe',
                groupName: 'Grupa testowa',
                isTaskCompleted: true,
                statusTooltip: 'Zadanie wykonane',
                style: {
                    'background-color': '#018ee0'
                }
            }
        ];
    };

})();