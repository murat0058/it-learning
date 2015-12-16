/**
* @desc Manage users in given group directive
* @example <itl-group-users-management-item user="user" delete-user="groupUsersVm.deleteUser"></itl-group-users-management-item>
*/
(function () {

    angular
        .module('app.groups')
        .directive('itlGroupUsersManagementItem', itlGroupUsersManagementItem);

    function itlGroupUsersManagementItem() {

        var directive = {
            templateUrl: '/src/js/app/Groups/templates/group-users-management-item.html',
            restrict: 'E',
            scope: {
                user: '=',
                deleteUser: '='
            },
            link: link
        };

        var deleteUserFunc = null;

        function link(scope, element, attrs) {

            scope.user.isDeletingInProgress = false;
            scope.user.isDeletingConfirmed = false;

            scope.toggleUserDeleting = toggleUserDeleting;
            scope.confirmUserDeleting = confirmUserDeleting;
            scope.cancelUserDeleting = cancelUserDeleting;

            deleteUserFunc = scope.deleteUser;
        }

        function toggleUserDeleting(user) {

            user.isDeletingInProgress = !user.isDeletingInProgress;
        }

        function confirmUserDeleting(user) {
            user.isDeletingConfirmed = true;

            deleteUserFunc(user, function () {
                user.isDeletingConfirmed = false;
            });
        }

        function cancelUserDeleting(user) {
            user.isDeletingInProgress = false;
        }

        return directive;
    }

})();