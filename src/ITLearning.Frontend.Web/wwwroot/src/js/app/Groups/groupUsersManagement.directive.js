/**
* @desc Manage users in given group directive
* @example <itl-group-users-management group-id="5"></itl-group-users-management>
*/
(function () {

    angular
        .module('app.groups')
        .directive('itlGroupUsersManagement', itlGroupUsersManagement);

    function itlGroupUsersManagement() {

        var directive = {
            templateUrl: '/src/js/app/Groups/templates/group-users-management.html',
            restrict: 'E',
            transclude: true,
            scope: {
                groupId: '@'
            },
            controller: GroupUsersManagementController,
            controllerAs: 'groupUsersVm',
            bindToController: true
        };

        return directive;
    }

    GroupUsersManagementController.$inject = ['groupsService', 'loadingIndicatorService'];

    function GroupUsersManagementController(groupsService, loadingIndicatorService) {

        var groupUsersVm = this;

        var loadingMessage = "Pobieram dane...";

        groupUsersVm.users = [];
        groupUsersVm.loadingIndicator = loadingIndicatorService.getIndicator(loadingMessage);

        groupUsersVm.deleteUser = deleteUser;

        activate();

        /////////////////

        function activate() {
            getUsers();
        }

        function getUsers() {
            groupUsersVm.loadingIndicator.SetLoading(loadingMessage);

            var request = {
                GroupId: groupUsersVm.groupId
            };

            return groupsService
                .getUsersForGroupManagement(request)
                .then(function (data) {
                    if (data.IsSuccess) {
                        groupUsersVm.users = data.Item.Users.map(function (user) {
                            return {
                                id: user.Id,
                                name: user.Name
                            }
                        });

                        groupUsersVm.loadingIndicator.Hide();
                    } else {
                        groupUsersVm.loadingIndicator.SetLoaded(data.ErrorMessage);
                    }
                });
        }

        function deleteUser(user, failureCallback) {

            var request = {
                UserId: user.id,
                GroupId: groupUsersVm.groupId
            };

            return groupsService
               .deleteUser(request)
               .then(function (data) {
                   if (data.IsSuccess) {
                       var indexOfUser = groupUsersVm.users.indexOf(user);
                       groupUsersVm.users.splice(indexOfUser, 1);
                   } else {
                       if (typeof (failureCallback) === "function") {
                           failureCallback();
                       }
                   }
               });

        }

    }

})();