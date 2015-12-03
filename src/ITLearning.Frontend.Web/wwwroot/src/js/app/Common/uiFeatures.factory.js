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

        var languageEnumDisplayName = {
            'CSharp': 'C#',
            'JavaScript': 'JS',
            'Other': 'Brak'
        };

        var service = {
            languageToColorMappings: languageToColorMappings,
            languageEnumDisplayName: languageEnumDisplayName
        };

        return service;
    }

})();