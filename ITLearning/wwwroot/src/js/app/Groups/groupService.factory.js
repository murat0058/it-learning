(function () {

    angular
        .module('app.groups')
        .factory('groupService', groupService);

    taskService.$inject = ['$http'];

    function groupService($http) {

        var service = {
            getUserGroups: getUserGroups
        };

        return service;
        ////////////////////////////

        function getUserGroups() {
            
        }
    }

})();