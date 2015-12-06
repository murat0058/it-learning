(function () {

    angular
        .module('app.tasks')
        .controller('OwnerSingleTaskViewController', OwnerSingleTaskViewController);

    OwnerSingleTaskViewController.$inject = ['uiFeaturesService'];

    function OwnerSingleTaskViewController(uiFeaturesService) {
        var ownerSingleTaskVM = this;

        ownerSingleTaskVM.Language;
        ownerSingleTaskVM.BackgroundColor;
        ownerSingleTaskVM.BackgroundImage;

        ownerSingleTaskVM.init = function (language) {
            ownerSingleTaskVM.Language = uiFeaturesService.languageEnumDisplayName[language];

            ownerSingleTaskVM.BackgroundImage = { 'background-image': "url('/common/images/tasks/" + uiFeaturesService.languageToTaskHeaderBackgroundImage[ownerSingleTaskVM.Language] + "')" };
            ownerSingleTaskVM.BackgroundColor = { 'background-color': uiFeaturesService.languageToColorMappings[ownerSingleTaskVM.Language] };
        };
    }
})();