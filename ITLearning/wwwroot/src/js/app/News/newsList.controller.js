(function () {

    angular
        .module('app.news')
        .controller('NewsListController', NewsListController);

    NewsListController.$inject = ['newsService']

    function NewsListController(newsService) {
        var newsListVm = this;

        newsListVm.filters = {
            query: '',
            tags: [],
            authors: [],
            tagsNotEmpty: false,
            authorsNotEmpty: false
        };

        newsListVm.tags = [];
        newsListVm.authors = [];
        newsListVm.isLoadingIndicatorVisible = false;

        newsListVm.activate = activate;
        newsListVm.search = search;
        newsListVm.toggleTagFilter = toggleTagFilter;
        newsListVm.toggleAuthorFilter = toggleAuthorFilter;

        newsListVm.formatTag = formatTag;
        newsListVm.getNewsStyle = getNewsStyle;

        //////////////
        function activate(model) {
            newsListVm.news = model.News;
            newsListVm.tags = model.Tags;
            newsListVm.authors = model.Authors;
        }

        function search() {
            getNews();
        }

        function getNews() {
            newsListVm.isLoadingIndicatorVisible = true;

            var request = {
                Query: newsListVm.filters.query,
                Tag: newsListVm.filters.tag,
                Author: newsListVm.filters.author
            };

            return newsService
                    .getNewsList(request)
                    .then(function (data) {
                        newsListVm.news = data;
                        newsListVm.isLoadingIndicatorVisible = false;
                    });
        }

        function toggleTagFilter(tag) {
            genericFilterToggle(tag, "tags");
        }

        function toggleAuthorFilter(author) {
            genericFilterToggle(author, "authors");
        }

        function genericFilterToggle(filterItem, propertyName) {
            var index = newsListVm.filters[propertyName].indexOf(filterItem);

            if (index > -1) {
                newsListVm.filters[propertyName].splice(index, 1);
            } else {
                newsListVm.filters[propertyName].push(filterItem);
            }

            var filterNotEmptyPropertyName = propertyName + "NotEmpty";
            newsListVm.filters[filterNotEmptyPropertyName] = newsListVm.filters[propertyName].length > 0;
        }

        function formatTag(tag) {
            return '#' + tag;
        }

        function getNewsStyle(news) {
            return {
                'background-image': "url('" + news.ImagePath + "')"
            };
        }
    };

})();