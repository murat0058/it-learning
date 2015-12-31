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
                .getUsersForGroup(request)
                .then(function (data) {
                    if (data.IsSuccess) {
                        groupUsersVm.users = data.Item.Users.map(function (user) {
                            return {
                                id: user.Id,
                                name: user.Name,
                                userName: user.UserName,
                                imagePath: user.ProfileImagePath
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