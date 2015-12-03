/**
* @desc Loading indicator. 
*       Should be loaded into other directives via transclusion.
* @example <itl-loading-indicator></itl-loading-indicator>
*/
(function () {

    angular
        .module('app.uiWidgets')
        .directive('itlLoadingIndicator', itlLoadingIndicator);

    function itlLoadingIndicator() {

        var directive = {
            templateUrl: '/src/js/app/UiWidgets/templates/loading-indicator.html',
            restrict: 'E',
            scope: {
                parentVm: '=',
                parentVmScoped: '@',
                dotBackground: '@',
                dotSize: '@'
            },
            link: link
        };

        return directive;

        //////////////////////

        function link(scope, elem, attrs) {

            scope.loadingIndicatorVm = {
                loadingIndicator: getIndicator(scope),
                style: {
                    'background-color': scope.dotBackground,
                    'width': scope.dotSize,
                    'height': scope.dotSize
                }
            };
        }

        function getIndicator(scope) {
            if (scope.parentVm) {
                return scope.parentVm.loadingIndicator;
            }

            if (scope.parentVmScoped) {
                return scope.$parent.$parent[scope.parentVmScoped].loadingIndicator;
            }
        }
    }

})();