(function () {

    angular
        .module('app.tasks')
        .factory('tasksService', tasksService);

    tasksService.$inject = ['$http'];

    function tasksService($http) {

        var service = {
            getLatestTasks: getLatestTasks,
            createTask: createTask,
            editTask: editTask,
            addBranch: addBranch,
            deleteBranch: deleteBranch,
            createCodeReview: createCodeReview,
            getTasksList: getTasksList,
            getTasksForUser: getTasksForUser
        };

        return service;
        ////////////////////////////

        function createTask(requestData, callback) {
            return $http.post('/Tasks/Create', requestData).success(
                function (data, status, headers) {
                    callback(data);
                });
        }

        function editTask(requestData) {
            return $http.post('/Tasks/Edit', requestData);
        }

        function addBranch(taskId, branchName) {
            return $http.post('/Tasks/CreateBranch/' + taskId + '/' + branchName);
        }

        function deleteBranch(taskId, branchName) {
            return $http.post('/Tasks/DeleteBranch/' + taskId + '/' + branchName);
        }

        function createCodeReview(requestData) {
            return $http.post('/Tasks/CreateCodeReview/', requestData);
        }

        
        function getTasksForUser(userName) {
            return $http.post('/Tasks/GetTasksForUser/' + userName)
                .then(getTasksCompleted)
                .catch(getTasksFailed);

            function getTasksCompleted(response) {
                return response.data;
            }

            function getTasksFailed(error) {
                console.log('Request failed for getTasksForUser method.' + error.data);
            }
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

        function getTasksList(requestData) {
            return $http.post('/Tasks/GetList', requestData)
                .then(getTasksCompleted)
                .catch(getTasksFailed);

            function getTasksCompleted(response) {
                return response.data;
            }

            function getTasksFailed(error) {
                console.log('Request failed for getTasksList method.' + error.data);
            }
        }
    }
})();