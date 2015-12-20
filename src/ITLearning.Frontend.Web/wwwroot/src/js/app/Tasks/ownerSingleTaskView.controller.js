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
        ownerSingleTaskVM.IsDescriptionVisible;

        ownerSingleTaskVM.taskInstances;

        ownerSingleTaskVM.codeReview = {};
        ownerSingleTaskVM.codeReview.UserName = "";
        ownerSingleTaskVM.codeReview.NumberOfActivityDays = "";
        ownerSingleTaskVM.codeReview.ArchitectureRate = 0;
        ownerSingleTaskVM.codeReview.OptymizationRate = 0;
        ownerSingleTaskVM.codeReview.CleanCodeRate = 0;
        ownerSingleTaskVM.codeReview.Comment = "";
        ownerSingleTaskVM.codeReview.Branches = [];

        ownerSingleTaskVM.init = function (language, taskInstances) {
            ownerSingleTaskVM.IsDescriptionVisible = true;

            ownerSingleTaskVM.Language = uiFeaturesService.languageEnumDisplayName[language];
            ownerSingleTaskVM.taskInstances = taskInstances;

            ownerSingleTaskVM.BackgroundImage = { 'background-image': "url('/common/images/tasks/" + uiFeaturesService.languageToTaskHeaderBackgroundImage[ownerSingleTaskVM.Language] + "')" };
            ownerSingleTaskVM.BackgroundColor = { 'background-color': uiFeaturesService.languageToColorMappings[ownerSingleTaskVM.Language] };
        };

        ownerSingleTaskVM.edit = function (language) {
            
        };

        ownerSingleTaskVM.delete = function (language) {
        
        };

        ownerSingleTaskVM.setCodeReviewModel = function (userName) {
            var selected = ownerSingleTaskVM.taskInstances.filter(function (item) {
                return item.User.UserName === userName;
            })[0];

            ownerSingleTaskVM.codeReview.UserName = selected.User.UserName;
            ownerSingleTaskVM.codeReview.NumberOfActivityDays = selected.CodeReview.NumberOfActivityDays;
            ownerSingleTaskVM.codeReview.Branches = selected.CodeReview.Branches;
            ownerSingleTaskVM.codeReview.ArchitectureRate = parseInt(selected.CodeReview.ArchitectureRate);
            ownerSingleTaskVM.codeReview.OptymizationRate = parseInt(selected.CodeReview.OptymizationRate);
            ownerSingleTaskVM.codeReview.CleanCodeRate = parseInt(selected.CodeReview.CleanCodeRate);
            ownerSingleTaskVM.codeReview.Comment = selected.CodeReview.Comment;
        };

        ownerSingleTaskVM.saveCodeReview = function () {
            var selected = ownerSingleTaskVM.taskInstances.filter(function (item) {
                return item.User.UserName === ownerSingleTaskVM.codeReview.UserName;
            })[0];

            selected.CodeReviewExist = true;
            selected.CodeReview.ArchitectureRate = ownerSingleTaskVM.codeReview.ArchitectureRate;
            selected.CodeReview.OptymizationRate = ownerSingleTaskVM.codeReview.OptymizationRate;
            selected.CodeReview.CleanCodeRate = ownerSingleTaskVM.codeReview.CleanCodeRate;
            selected.CodeReview.Comment = ownerSingleTaskVM.codeReview.Comment;

            //TODO
        };
    }
})();