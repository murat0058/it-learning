(function () {

    angular
        .module('app.tasks')
        .controller('InstanceSingleTaskViewController', InstanceSingleTaskViewController);

    InstanceSingleTaskViewController.$inject = ['uiFeaturesService'];

    function InstanceSingleTaskViewController(uiFeaturesService) {
        var instanceSingleTaskVM = this;

        instanceSingleTaskVM.Language;
        instanceSingleTaskVM.BackgroundColor;
        instanceSingleTaskVM.BackgroundImage;
        instanceSingleTaskVM.branches = [];

        instanceSingleTaskVM.min = 0;
        instanceSingleTaskVM.max = 0;

        instanceSingleTaskVM.init = function (language, branches) {
            instanceSingleTaskVM.Language = uiFeaturesService.languageEnumDisplayName[language];
            instanceSingleTaskVM.branches = branches;

            for (var i = 0; i < instanceSingleTaskVM.branches.length; i++) {
                instanceSingleTaskVM.branches[i].id = i + 1;
            }

            instanceSingleTaskVM.BackgroundImage =
                {
                    'background-image': "url('/common/images/tasks/" + uiFeaturesService.languageToTaskHeaderBackgroundImage[instanceSingleTaskVM.Language] + "')"
                };
            instanceSingleTaskVM.BackgroundColor =
                {
                    'background-color': uiFeaturesService.languageToColorMappings[instanceSingleTaskVM.Language]
                };
        };

        instanceSingleTaskVM.edit = function (language) {

        };

        instanceSingleTaskVM.delete = function (language) {

        };
    }
})();