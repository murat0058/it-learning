(function () {

    angular
        .module('app.common')
        .factory('uiFeaturesService', uiFeaturesService);

    function uiFeaturesService() {

        var languageToColorMappings = {
            'C#': '#018ee0',
            'JAVA': '#ed4933',
            'JS': '#ffbc00',
            'Brak': '#15D4C5'
        };

        var languageEnumDisplayName = {
            'CSharp': 'C#',
            'JavaScript': 'JS',
            'Java': 'Java',
            'Other': 'Brak'
        };

        var languageToTaskHeaderBackgroundImage = {
            'C#': 'task-header-csharp.jpg',
            'JAVA': 'task-header-java.jpg',
            'JS': 'task-header-js.jpg',
            'Brak': 'task-header-default.jpg'
        };

        var service = {
            languageToColorMappings: languageToColorMappings,
            languageEnumDisplayName: languageEnumDisplayName,
            languageToTaskHeaderBackgroundImage: languageToTaskHeaderBackgroundImage
        };

        return service;
    }

})();