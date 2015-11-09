/**
* @desc Loading indicator. 
*       Should be loaded into other directives via transclusion.
*       Requires 'isLoadingIndicatorVisible' property in PARENT scope.
* @example <itl-loading-indicator></itl-loading-indicator>
*/
(function () {

    angular
        .module('app.uiWidgets')
        .directive('itlLoadingIndicator', itlLoadingIndicator);

    function itlLoadingIndicator() {

        var directive = {
            templateUrl: 'src/js/app/UiWidgets/templates/loading-indicator.html',
            restrict: 'E',
            scope: {
                parentVm: '=',
                dotBackground: '@',
                dotSize: '@',
                loadingText: '@'
            },
            link: link
        };

        return directive;

        //////////////////////

        function link(scope, elem, attrs) {

            scope.loadingIndicatorVm = {
                loadingText: scope.loadingText,
                style: {
                    'background-color': scope.dotBackground,
                    'width': scope.dotSize,
                    'height': scope.dotSize
                }
            };
        }
    }

})();