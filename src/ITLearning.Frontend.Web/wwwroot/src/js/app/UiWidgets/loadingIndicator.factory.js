(function () {

    angular
        .module('app.uiWidgets')
        .factory('loadingIndicatorService', loadingIndicatorService);

    function loadingIndicatorService() {

        var factory = {
            getIndicator: getIndicator
        };

        return factory;

        ////////////

        function getIndicator(text) {
            return new LoadingIndicator(text, true, true);
        }
    }

    function LoadingIndicator(loadingText, isVisible, isLoading) {
        this.isVisible = isVisible;
        this.isLoading = isLoading;
        this.loadingText = loadingText;
    };

    LoadingIndicator.prototype.SetLoading = function (text) {
        this.isVisible = true;
        this.isLoading = true;
        this.loadingText = text;
    };

    LoadingIndicator.prototype.SetLoaded = function (text) {
        this.isVisible = true;
        this.isLoading = false;
        this.loadingText = text;
    };

    LoadingIndicator.prototype.Hide = function () {
        this.isVisible = false;
    };

})();