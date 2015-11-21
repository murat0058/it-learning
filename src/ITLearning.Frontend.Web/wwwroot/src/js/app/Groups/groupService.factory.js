(function () {

    angular
        .module('app.groups')
        .factory('groupsService', groupsService);

    groupsService.$inject = ['$http'];

    function groupsService($http) {

        var service = {
            getUserGroupsBasicData: getUserGroupsBasicData
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
    }

})();