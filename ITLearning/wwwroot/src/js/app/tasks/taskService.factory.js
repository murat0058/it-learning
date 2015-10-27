(function () {

    angular
        .module('app.tasks')
        .factory('taskService', taskService);

    taskService.$inject = ['$http'];

    function taskService($http) {

        var service = {
            getLatestTasks: getLatestTasks
        };

        return service;
        ////////////////////////////

        function getLatestTasks(noOfTasks) {
            return [
                {
                    taskId: 0,
                    groupId: 1,
                    language: 'C#',
                    taskName: 'Zadanie testowe',
                    groupName: 'Grupa testowa',
                    isTaskCompleted: true,
                    statusTooltip: 'Zadanie wykonane',
                    style: {
                        'background-color': '#ff8d00'
                    }
                },
                {
                    taskId: 1,
                    groupId: 1,
                    language: 'C#',
                    taskName: 'Zadanie testowe',
                    groupName: 'Grupa testowa',
                    isTaskCompleted: false,
                    statusTooltip: 'Zadanie wykonane',
                    style: {
                        'background-color': '#21b2a6'
                    }
                },
                {
                    taskId: 2,
                    groupId: 1,
                    language: 'JAVA',
                    taskName: 'Zadanie testowe',
                    groupName: 'Grupa testowa',
                    isTaskCompleted: true,
                    statusTooltip: 'Zadanie wykonane',
                    style: {
                        'background-color': '#018ee0'
                    }
                },
                {
                    taskId: 3,
                    groupId: 1,
                    language: 'JAVA',
                    taskName: 'Zadanie testowe',
                    groupName: 'Grupa testowa',
                    isTaskCompleted: true,
                    statusTooltip: 'Zadanie wykonane',
                    style: {
                        'background-color': '#018ee0'
                    }
                }
            ];
        };
    }

})();