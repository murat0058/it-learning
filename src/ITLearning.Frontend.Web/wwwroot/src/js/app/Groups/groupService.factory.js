(function () {

    angular
        .module('app.groups')
        .factory('groupsService', groupsService);

    groupsService.$inject = ['$http'];

    function groupsService($http) {

        var service = {
            getGroups: getGroups,
            getUserGroupsBasicData: getUserGroupsBasicData,
            getUsersForGroup: getUsersForGroup,
            getUsersForGroupManagement: getUsersForGroupManagement,
            deleteUser: deleteUser
        };

        return service;

        ////////////////////////////

        function getGroups(requestData) {
            return $http.post('/Groups/GetGroupsList', requestData)
                .then(getGroupsComplete)
                .catch(getGroupsFailed);

            function getGroupsComplete(response) {
                return response.data;
            }

            function getGroupsFailed(error) {
                console.log('Request failed for getGroups method.' + error.data);
            }
        }

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

            function getUsersForGroupComplete(response) {
                return response.data;
            }

            function getUsersForGroupFailed(error) {
                console.log('Request failed for getUsersForGroup method.' + error.data);
            }
        }

        function getUsersForGroupManagement(requestData) {
            return $http.post('/Groups/GetUsersForGroupManagement', requestData)
                .then(getUsersForGroupComplete)
                .catch(getUsersForGroupFailed);

            function getUsersForGroupComplete(response){
                return response.data;
            }

            function getUsersForGroupFailed(error) {
                console.log('Request failed for getUsersForGroupManagement method.' + error.data);
            }
        }

        function deleteUser(requestData) {
            return $http.post('/Groups/DeleteUser', requestData)
                .then(deleteUserComplete)
                .catch(deleteUserFailed);

            function deleteUserComplete(response) {
                return response.data;
            }

            function deleteUserFailed(error) {
                console.log('Request failed for deleteUser method.' + error.data);
            }
        }
    }

})();