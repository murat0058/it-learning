/**
* @desc Task list common directive
* @example <itl-task-list no-of-tasks="5"></itl-task-list>
*/
(function () {

    angular
        .module('app.tasks')
        .directive('itlTaskList', itlTaskList);

    function itlTaskList() {

        var directive = {
            templateUrl: 'src/js/app/Tasks/templates/task-list.html',
            restrict: 'E',
            transclude: true,
            scope: {
                noOfTasks: '@'
            },
            controller: TasksController,
            controllerAs: 'vm',
            bindToController: true
        };

        return directive;
    }

    TasksController.$inject = ['taskService'];

    function TasksController(taskService) {

        var vm = this;

        vm.isLoadingIndicatorVisible = true;

        activate();

        ////////////////////////////

        function activate() {
            vm.isLoadingIndicatorVisible = false;
        };
    }

})();