/**
* @desc Manage users in given group directive
* @example <itl-group-users group-id="5"></itl-group-users>
*/
(function () {

    angular
        .module('app.groups')
        .directive('itlUsersListItem', itlUsersListItem);

    function itlUsersListItem() {

        var directive = {
            templateUrl: '/src/js/app/Groups/templates/users-list-item.html',
            restrict: 'E',
            scope: {
                user: '='
            },
            link: link
        };

        function link(scope, element, attrs) {

            scope.user.isDeletingInProgress = false;

            scope.toggleUserDeleting = toggleUserDeleting;
            scope.confirmUserDeleting = confirmUserDeleting;
            scope.cancelUserDeleting = cancelUserDeleting;
        }

        function toggleUserDeleting(user) {

            user.isDeletingInProgress = !user.isDeletingInProgress;
        }

        function confirmUserDeleting(user) {
            //TODO
        }

        function cancelUserDeleting(user) {
            user.isDeletingInProgress = false;
        }

        return directive;
    }

})();