(function () {

    angular
        .module('app.news')
        .factory('newsService', newsServiceFactory);

    newsServiceFactory.$inject = ['$http'];

    function newsServiceFactory($http) {

        var service = {
            getNewsList: getNewsList
        };

        return service;

        /////////////

        function getNewsList(requestData) {

            return $http.post('/News/List', requestData)
                .then(getNewsListComplete)
                .catch(getNewsListFailed);

            function getNewsListComplete(response) {
                return response.data;
            }

            function getNewsListFailed(error) {
                console.log('Request failed for getNewsList method.' + error.data);
            }
        }
    };

})();