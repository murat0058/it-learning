/**
* @desc Manage users in given group directive
* @example <itl-group-users group-id="5"></itl-group-users>
*/
(function () {

    angular
        .module('app.groups')
        .directive('itlGroupUsers', itlGroupUsers);

    function itlGroupUsers() {

        var directive = {
            templateUrl: '/src/js/app/Groups/templates/group-users.html',
            restrict: 'E',
            transclude: true,
            scope: {
                groupId: '@'
            },
            controller: GroupUsersController,
            controllerAs: 'groupUsersVm',
            bindToController: true
        };

        return directive;
    }

    GroupUsersController.$inject = ['groupsService', 'loadingIndicatorService'];

    function GroupUsersController(groupsService, loadingIndicatorService) {

        var groupUsersVm = this;

        var loadingMessage = "Pobieram dane...";

        groupUsersVm.loadingIndicator = loadingIndicatorService.getIndicator(loadingMessage);

    }

})();