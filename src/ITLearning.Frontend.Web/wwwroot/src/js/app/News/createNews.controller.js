(function () {

    angular
        .module('app.news')
        .controller('CreateNewsController', CreateNewsController);

    CreateNewsController.$inject = ['newsService'];

    function CreateNewsController(newsService) {

        var createNewsVm = this;

        createNewsVm.newsTitle = '';
        createNewsVm.newsContent = '';
        createNewsVm.newsTags = '';

        createNewsVm.flow = {};

        createNewsVm.init = init;
        createNewsVm.addNews = addNews;

        function init(model) {
            createNewsVm.newsTitle = model.Title;
            createNewsVm.newsContent = model.Content;
            createNewsVm.newsTags = model.TagsString;
        }

        function addNews(form) {
            if (form.$valid) {
                createNewsRequest();
            }
        };

        function createNewsRequest() {

            var request = {
                Title: createNewsVm.newsTitle,
                Content: createNewsVm.newsContent,
                TagsString: createNewsVm.newsTags,
                Image: createNewsVm.flow.files[0].file
            };

            return newsService.createNews(request);
        };
    };

})();