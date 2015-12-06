(function () {

    angular
        .module('app.groups')
        .factory('groupsService', groupsService);

    groupsService.$inject = ['$http'];

    function groupsService($http) {

        var service = {
            getUserGroupsBasicData: getUserGroupsBasicData,
            getUsersForGroup: getUsersForGroup
        };

        return service;
        ////////////////////////////

        function getUserGroupsBasicData(requestData) {
            return $http.post('/Groups/UserGroupsBasicData', requestData)
                .then(getUserGroupsBasicDataComplete)
                .catch(getUserGroupsBasicDataFailed);

            function getUserGroupsBasicDataComplete(response) {
                return response.data;
            }

            function getUserGroupsBasicDataFailed(error) {
                console.log('Request failed for getUserGroupsBasicData method.' + error.data);
            }
        }

        function getUsersForGroup(requestData) {
            return $http.post('/Groups/GetUsersForGroup', requestData)
                .then(getUsersForGroupComplete)
                .catch(getUsersForGroupFailed);

            function getUsersForGroupComplete(response){
                return response.data;
            }

            function getUsersForGroupFailed(error) {
                console.log('Request failed for getUsersForGroupFailed method.' + error.data);
            }
        }
    }

})();