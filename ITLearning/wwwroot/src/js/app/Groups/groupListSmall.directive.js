/**
* @desc Group list common directive
* @example <itl-group-list-small></itl-group-list-small>
*/
(function () {

    angular
        .module('app.groups')
        .directive('itlGroupListSmall', itlGroupListSmall);

    function itlGroupListSmall() {

        var directive = {
            templateUrl: 'src/js/app/Groups/templates/group-list-small.html',
            restrict: 'E',
            transclude: true,
            controller: GroupsController,
            controllerAs: 'groupListVm',
            bindToController: true
        };

        return directive;
    }

    GroupsController.$inject = ['groupService'];

    function GroupsController(groupService) {

        var groupListVm = this;

        groupListVm.isLoadingIndicatorVisible = true;
        groupListVm.groups = [];

        activate();

        ////////////////////////////

        function activate() {

            groupListVm.groups = [
                {
                    id: 0,
                    name: '.NET ATH',
                    noOfUsers: 12
                },
                {
                    id: 1,
                    name: 'JAVA ATH',
                    noOfUsers: 12
                },
                {
                    id: 2,
                    name: 'DASIFJSDIFSDJIFDJSIFSDJIFSDJFISDJIFSDJ',
                    noOfUsers: 12
                },
            ];

        };
    }

})();