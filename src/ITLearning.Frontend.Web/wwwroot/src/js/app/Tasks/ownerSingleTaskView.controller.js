(function () {

    angular
        .module('app.tasks')
        .controller('OwnerSingleTaskViewController', OwnerSingleTaskViewController);

    OwnerSingleTaskViewController.$inject = ['tasksService', 'uiFeaturesService'];

    function OwnerSingleTaskViewController(tasksService, uiFeaturesService) {
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

        ownerSingleTaskVM.showRepositoryLink = function (user, text) {
            var message = "Link do repozytorium użytkownika " + user + ". Ctrl+C, Enter";
            window.prompt(message, text);
        };

        ownerSingleTaskVM.setCodeReviewModel = function (userName) {
            var selected = ownerSingleTaskVM.taskInstances.filter(function (item) {
                return item.User.UserName === userName;
            })[0];

            var startDate = new Date(selected.StartDate);
            var finishDate = new Date(selected.FinishDate);
            var interval = (finishDate - startDate) / (1000 * 60 * 60 * 24);
            interval = interval == 0 ? 1 : interval;

            ownerSingleTaskVM.codeReview.UserName = selected.User.UserName;
            ownerSingleTaskVM.codeReview.NumberOfActivityDays = interval;
            ownerSingleTaskVM.codeReview.Branches = selected.Branches != null ? selected.Branches : [];
            ownerSingleTaskVM.codeReview.ArchitectureRate = selected.CodeReview != null ? parseInt(selected.CodeReview.ArchitectureRate) : 0;
            ownerSingleTaskVM.codeReview.OptymizationRate = selected.CodeReview != null ? parseInt(selected.CodeReview.OptymizationRate) : 0;
            ownerSingleTaskVM.codeReview.CleanCodeRate = selected.CodeReview != null ? parseInt(selected.CodeReview.CleanCodeRate) : 0;
            ownerSingleTaskVM.codeReview.Comment = selected.CodeReview != null ? selected.CodeReview.Comment : "";
        };

        ownerSingleTaskVM.saveCodeReview = function () {
            var selected = ownerSingleTaskVM.taskInstances.filter(function (item) {
                return item.User.UserName === ownerSingleTaskVM.codeReview.UserName;
            })[0];

            selected.CodeReviewExist = true;
            selected.CodeReview = {};
            selected.CodeReview.ArchitectureRate = ownerSingleTaskVM.codeReview.ArchitectureRate;
            selected.CodeReview.OptymizationRate = ownerSingleTaskVM.codeReview.OptymizationRate;
            selected.CodeReview.CleanCodeRate = ownerSingleTaskVM.codeReview.CleanCodeRate;
            selected.CodeReview.Comment = ownerSingleTaskVM.codeReview.Comment;

            var requestData = {
                TaskInstanceId: selected.Id,
                ArchitectureRate: ownerSingleTaskVM.codeReview.ArchitectureRate,
                OptymizationRate: ownerSingleTaskVM.codeReview.OptymizationRate,
                CleanCodeRate: ownerSingleTaskVM.codeReview.CleanCodeRate,
                Comment: ownerSingleTaskVM.codeReview.Comment,
            };

            tasksService.createCodeReview(requestData);
        };
    }
})();