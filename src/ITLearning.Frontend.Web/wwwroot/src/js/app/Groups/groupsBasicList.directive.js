﻿/**
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

    GroupsBasicListController.$inject = ['groupsService', 'loadingIndicatorService'];

    function GroupsBasicListController(groupsService, loadingIndicatorService) {

        var groupListVm = this,
            loadingMessage = "Ładuję twoje grupy...";

        groupListVm.groups = [];
        groupListVm.loadingIndicator = loadingIndicatorService.getIndicator(loadingMessage);

        activate();

        //////////////////

        function activate() {
            getGroups();
        };

        function getGroups() {
            groupListVm.loadingIndicator.SetLoading(loadingMessage);

            var request = {
                noOfGroups: groupListVm.noOfGroups
            };

            return groupsService
                .getUserGroupsBasicData(request)
                .then(function (data) {

                    if (data.IsSuccess) {
                        groupListVm.groups = data.Item;
                        groupListVm.loadingIndicator.Hide();
                    } else {
                        groupListVm.loadingIndicator.SetLoaded(data.ErrorMessage);
                    }
                });
        }
    }

})();