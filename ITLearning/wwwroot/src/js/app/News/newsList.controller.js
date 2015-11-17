(function () {

    angular
        .module('app.news')
        .controller('NewsListController', NewsListController);

    NewsListController.$inject = ['newsService']

    function NewsListController(newsService) {
        var newsListVm = this;

        newsListVm.filters = {
            query: '',
            tag: '',
            author: ''
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
            newsListVm.filters.tag = newsListVm.filters.tag === tag ? '' : tag;
        }

        function toggleAuthorFilter(author) {
            newsListVm.filters.author = newsListVm.filters.author === author ? '' : author;
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