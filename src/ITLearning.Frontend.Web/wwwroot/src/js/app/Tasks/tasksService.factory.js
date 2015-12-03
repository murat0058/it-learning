(function () {

    angular
        .module('app.tasks')
        .factory('tasksService', tasksService);

    tasksService.$inject = ['$http'];

    function tasksService($http) {

        var service = {
            getLatestTasks: getLatestTasks,
            createTask: createTask
        };

        return service;
        ////////////////////////////

        function createTask(requestData) {
            return $http.post('/Tasks/Create', requestData);
        }

        function getLatestTasks(requestData) {
            return $http.post('/Tasks/GetLatest', requestData)
                .then(getLatestTasksCompleted)
                .catch(getLatestTasksFailed);

            function getLatestTasksCompleted(response) {
                return response.data;
            }

            function getLatestTasksFailed(error) {
                console.log('Request failed for getLatestTasks method.' + error.data);
            }
        }
    }
})();