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

        groupUsersVm.users = [];
        groupUsersVm.loadingIndicator = loadingIndicatorService.getIndicator(loadingMessage);

        activate();

        function activate() {
            groupUsersVm.users = [
                {
                    id: 0,
                    name: 'Przemek Smyrdek'
                },
                {
                    id: 1,
                    name: 'Janek Kowalski'
                },
                {
                    id: 2,
                    name: 'Tomek Frankowski'
                },
                {
                    id: 3,
                    name: 'Marek Nowak'
                },
                {
                    id: 4,
                    name: 'Janek Nowak'
                },
                {
                    id: 5,
                    name: 'Tomek Frankowski'
                },
                {
                    id: 6,
                    name: 'Tomek Frankowski'
                }
            ];

            groupUsersVm.loadingIndicator.Hide();
        }

    }

})();