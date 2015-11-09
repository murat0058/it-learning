/**
* @desc Task list common directive
* @example <itl-group-list></itl-group-list>
*/
(function () {

    angular
        .module('app.groups')
        .directive('itlGroupList', itlGroupList);

    function itlGroupList() {

        var directive = {
            templateUrl: 'src/js/app/Groups/templates/group-list.html',
            restrict: 'E',
            transclude: true,
            scope: {
                user: '@'
            },
            controller: GroupsController,
            controllerAs: 'vm',
            bindToController: true
        };

        return directive;
    }

    GroupsController.$inject = ['groupService'];

    function GroupsController(groupService) {

        var vm = this;

        vm.isLoadingIndicatorVisible = true;
        vm.groups = [];

        activate();

        ////////////////////////////

        function activate() {

        };
    }

})();