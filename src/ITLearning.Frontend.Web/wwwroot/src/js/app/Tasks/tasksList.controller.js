(function () {

    angular
        .module('app.tasks')
        .controller('TasksListController', TasksListController);

    TasksListController.$inject = ['$scope', 'tasksService', 'loadingIndicatorService', 'uiFeaturesService']

    function TasksListController($scope, tasksService, loadingIndicatorService, uiFeaturesService) {

        var tasksListVm = this;

        var loadingMessage = "Ładuję zadania...";

        tasksListVm.tasks = [];

        tasksListVm.taskOwnerEnum = {
            all: 0,
            onlyMine: 1
        };

        tasksListVm.languageEnum = {
            all: 0,
            other: 1,
            csharp: 2,
            javaScript: 3,
            java: 4
        };

        tasksListVm.activityStatusEnum = {
            active: 0,
            notActive: 1
        };

        tasksListVm.filters = {
            query: '',
            ownerType: tasksListVm.taskOwnerEnum.all,
            language: tasksListVm.languageEnum.all,
            activityStatus: tasksListVm.activityStatusEnum.active
        };

        tasksListVm.loadingIndicator = loadingIndicatorService.getIndicator(loadingMessage);

        tasksListVm.activate = activate;

        $scope.$watch('tasksListVm.filters.query', handleFilterChange);
        $scope.$watch('tasksListVm.filters.ownerType', handleFilterChange);
        $scope.$watch('tasksListVm.filters.language', handleFilterChange);
        $scope.$watch('tasksListVm.filters.activityStatus', handleFilterChange);

        activate();

        //////////////

        function activate() {
            getTasks();
        }

        function getTasks() {
            tasksListVm.loadingIndicator.SetLoading(loadingMessage);

            var request = {
                Query: tasksListVm.filters.query,
                OwnerType: tasksListVm.filters.ownerType,
                Language: tasksListVm.filters.language,
                ActivityStatus: tasksListVm.filters.activityStatus
            };

            return tasksService
                    .getTasksList(request)
                    .then(function (data) {
                        if (data.IsSuccess) {
                            data.Item.forEach(function (item) {
                                item.Language = uiFeaturesService.languageEnumDisplayName[item.Language];
                                item.style = {
                                    'background-color': uiFeaturesService.languageToColorMappings[item.Language]
                                };
                            });
                            tasksListVm.tasks = data.Item;
                            tasksListVm.loadingIndicator.Hide();
                        } else {
                            tasksListVm.tasks = [];
                            tasksListVm.loadingIndicator.SetLoaded(data.ErrorMessage);
                        }
                    });
        }

        function handleFilterChange(newValue, oldValue) {
            if (oldValue || newValue && oldValue !== newValue) {
                getTasks();
            }
        }
    };
})();