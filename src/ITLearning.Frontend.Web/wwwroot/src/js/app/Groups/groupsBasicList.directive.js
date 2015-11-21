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
                noOfGroups: '@'
            },
            controller: GroupsBasicListController,
            controllerAs: 'groupListVm',
            bindToController: true
        };

        return directive;
    }

    GroupsBasicListController.$inject = ['groupsService'];

    function GroupsBasicListController(groupsService) {

        var groupListVm = this;

        groupListVm.isLoadingIndicatorVisible = false;
        groupListVm.groups = [];

        activate();

        //////////////////

        function activate() {
            getGroups();
        };

        function getGroups() {
            groupListVm.isLoadingIndicatorVisible = true;

            var request = {
                noOfGroups: groupListVm.noOfGroups
            };

            return groupsService
                .getUserGroupsBasicData(request)
                .then(function (data) {
                    groupListVm.groups = data;
                    groupListVm.isLoadingIndicatorVisible = false;
                });
        }
    }

})();