(function () {

    angular
        .module('app.tasks')
        .controller('PublicSingleTaskViewController', PublicSingleTaskViewController);

    PublicSingleTaskViewController.$inject = ['uiFeaturesService'];

    function PublicSingleTaskViewController(uiFeaturesService) {
        var publicSingleTaskVM = this;

        publicSingleTaskVM.Language;
        publicSingleTaskVM.BackgroundColor;
        publicSingleTaskVM.BackgroundImage;

        publicSingleTaskVM.init = function (language) {
            publicSingleTaskVM.Language = uiFeaturesService.languageEnumDisplayName[language];

            publicSingleTaskVM.BackgroundImage = { 'background-image': "url('/common/images/tasks/" + uiFeaturesService.languageToTaskHeaderBackgroundImage[publicSingleTaskVM.Language] + "')" };
            publicSingleTaskVM.BackgroundColor = { 'background-color': uiFeaturesService.languageToColorMappings[publicSingleTaskVM.Language] };
        };
    }
})();