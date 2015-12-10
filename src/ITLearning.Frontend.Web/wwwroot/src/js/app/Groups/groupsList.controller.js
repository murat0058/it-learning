(function () {

    angular
        .module('app.groups')
        .controller('GroupsListController', GroupsListController);

    GroupsListController.$inject = ['$scope', 'groupsService', 'loadingIndicatorService']

    function GroupsListController($scope, groupsService, loadingIndicatorService) {

        var groupsListVm = this;

        var loadingMessage = "Ładuję grupy...";

        groupsListVm.groups = [];

        groupsListVm.groupOwnerEnum = {
            all: 0,
            onlyMine: 1
        };

        groupsListVm.groupAccessEnum = {
            all: 0,
            privateOnly: 1,
            publicOnly: 2
        };

        groupsListVm.filters = {
            query: '',
            ownerType: groupsListVm.groupOwnerEnum.all,
            accessType: groupsListVm.groupAccessEnum.all
        };

        groupsListVm.loadingIndicator = loadingIndicatorService.getIndicator(loadingMessage);

        groupsListVm.activate = activate;

        $scope.$watch('groupsListVm.filters.query', handleFilterChange);
        $scope.$watch('groupsListVm.filters.ownerType', handleFilterChange);
        $scope.$watch('groupsListVm.filters.accessType', handleFilterChange);

        activate();

        //////////////

        function activate() {
            getGroups();
        }

        function getGroups() {
            groupsListVm.loadingIndicator.SetLoading(loadingMessage);

            var request = {
                Query: groupsListVm.filters.query,
                OwnerType: groupsListVm.filters.ownerType,
                AccessType: groupsListVm.filters.accessType
            };

            return groupsService
                .getGroups(request)
                .then(function (data) {
                    
                    if (data.IsSuccess) {
                        groupsListVm.groups = data.Item.Groups;
                        groupsListVm.loadingIndicator.Hide();
                    } else {
                        groupsListVm.groups = [];
                        groupsListVm.loadingIndicator.SetLoaded(data.ErrorMessage);
                    }

                });
        }

        function handleFilterChange(newValue, oldValue) {
            if (oldValue || newValue && oldValue !== newValue) {
                getGroups();
            }
        }
    };

})();