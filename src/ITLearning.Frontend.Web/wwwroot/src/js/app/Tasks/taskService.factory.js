(function () {

    angular
        .module('app.tasks')
        .factory('taskService', taskService);

    taskService.$inject = ['$http'];

    function taskService($http) {

        var service = {
            getLatestTasks: getLatestTasks
        };

        return service;
        ////////////////////////////

        function getLatestTasks() {
            
        }
    }

})();