/**
* @desc Manage users in given group directive
* @example <itl-group-users-item user="user"></itl-group-users-item>
*/
(function () {

    angular
        .module('app.groups')
        .directive('itlGroupUsersItem', itlGroupUsersItem);

    function itlGroupUsersItem() {

        var directive = {
            templateUrl: '/src/js/app/Groups/templates/group-users-item.html',
            restrict: 'E',
            scope: {
                user: '='
            },
            link: link
        };

        function link(scope, element, attrs) {

           
        }
        
        return directive;
    }

})();