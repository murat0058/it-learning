(function () {

    angular
        .module('app.common')
        .factory('uiFeaturesService', uiFeaturesService);

    function uiFeaturesService() {

        var languageToColorMappings = {
            'C#': '#018ee0',
            'JAVA': '#ed4933',
            'JS': '#ffbc00'
        };

        var service = {
            languageToColorMappings: languageToColorMappings
        };

        return service;
    }

})();