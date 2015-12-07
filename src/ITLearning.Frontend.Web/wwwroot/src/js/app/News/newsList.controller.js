(function () {

    angular
        .module('app.news')
        .controller('NewsListController', NewsListController);

    NewsListController.$inject = ['$scope', 'newsService', 'loadingIndicatorService']

    function NewsListController($scope, newsService, loadingIndicatorService) {
        var newsListVm = this;

        var loadingMessage = "Ładuję newsy...";

        newsListVm.filters = {
            query: '',
            tags: [],
            authors: [],
            tagsNotEmpty: false,
            authorsNotEmpty: false
        };

        newsListVm.tags = [];
        newsListVm.authors = [];
        newsListVm.loadingIndicator = loadingIndicatorService.getIndicator(loadingMessage);

        newsListVm.activate = activate;
        newsListVm.toggleTagFilter = toggleTagFilter;
        newsListVm.toggleAuthorFilter = toggleAuthorFilter;

        newsListVm.formatTag = formatTag;
        newsListVm.getNewsStyle = getNewsStyle;

        $scope.$watch('newsListVm.filters.query',
            function handleQueryFilterChange(newValue, oldValue) {
                if (oldValue || newValue) {
                    getNews();
                }
            }
        );

        //////////////

        function activate(model) {

            newsListVm.tags = model.Tags;
            newsListVm.authors = model.Authors;

            if (model.FilterRequest.Tags) {
                newsListVm.filters.tags = model.FilterRequest.Tags;
                newsListVm.filters.tagsNotEmpty = true;
            }

            if (model.FilterRequest.Authors) {
                newsListVm.filters.authors = model.FilterRequest.Authors;
                newsListVm.filters.authorsNotEmpty = true;
            }

            getNews();
        }

        function getNews() {
            newsListVm.loadingIndicator.SetLoading(loadingMessage);

            var request = {
                Query: newsListVm.filters.query,
                Tags: newsListVm.filters.tags,
                Authors: newsListVm.filters.authors
            };

            return newsService
                .getNewsList(request)
                .then(function (data) {
                    newsListVm.news = data;
                    newsListVm.loadingIndicator.Hide();
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

            getNews();
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