/**
* @desc Group list common directive
* @example <itl-groups-basic-list no-of-groups="3"></itl-groups-basic-list>
*/
(function () {

    angular
        .module('app.groups')
        .directive('itlGroupsBasicList', itlGroupsBasicList);

    function itlGroupsBasicList() {

        var directive = {
            templateUrl: '/src/js/app/Groups/templates/groups-basic-list.html',
            restrict: 'E',
            transclude: true,
            scope: {
                noOfGroups: '@',
                userName: '@'
            },
            controller: GroupsBasicListController,
            controllerAs: 'groupListVm',
            bindToController: true
        };

        return directive;
    }

    GroupsBasicListController.$inject = ['groupsService', 'loadingIndicatorService'];

    function GroupsBasicListController(groupsService, loadingIndicatorService) {
        var groupListVm = this,
            loadingMessage;

        groupListVm.groups = [];
        groupListVm.loadingIndicator;

        activate();

        //////////////////

        function activate() {
            loadingMessage = groupListVm.userName ? "Ładuję grupy..." : "Ładuję twoje grupy...";
            groupListVm.loadingIndicator = loadingIndicatorService.getIndicator(loadingMessage);

            getGroups();
        };

        function getGroups() {
            groupListVm.loadingIndicator.SetLoading(loadingMessage);

            var request = {
                noOfGroups: groupListVm.noOfGroups,
                userName: groupListVm.userName
            };

            return groupsService
                .getUserGroupsBasicData(request)
                .then(function (data) {

                    if (data.IsSuccess) {
                        groupListVm.groups = data.Item.Groups;
                        groupListVm.loadingIndicator.Hide();
                    } else {
                        groupListVm.loadingIndicator.SetLoaded(data.ErrorMessage);
                    }
                });
        }
    }

})();