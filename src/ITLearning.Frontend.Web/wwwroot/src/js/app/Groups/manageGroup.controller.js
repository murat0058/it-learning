(function () {

    angular
        .module('app.groups')
        .controller('ManageGroupController', ManageGroupController);

    ManageGroupController.$inject = ['loadingIndicatorService'];

    function ManageGroupController(loadingIndicatorService) {

        var manageGroupVm = this;

        manageGroupVm.groupDeleteManager = {
            isGroupDeletingInProgress: false,
            toggleGroupDeleting: toggleGroupDeleting,
            cancelGroupDeleting: cancelGroupDeleting
        };

        //////////////

        function toggleGroupDeleting() {
            manageGroupVm.groupDeleteManager.isGroupDeletingInProgress = !manageGroupVm.groupDeleteManager.isGroupDeletingInProgress;
        }

        function cancelGroupDeleting() {
            manageGroupVm.groupDeleteManager.isGroupDeletingInProgress = false;
        }
    };

})();