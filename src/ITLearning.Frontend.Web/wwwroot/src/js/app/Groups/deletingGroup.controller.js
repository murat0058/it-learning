(function () {

    angular
        .module('app.groups')
        .controller('DeletingGroupController', DeletingGroupController);

    DeletingGroupController.$inject = ['loadingIndicatorService'];

    function DeletingGroupController(loadingIndicatorService) {

        var deletingGroupVm = this;

        deletingGroupVm.isGroupDeletingInProgress = false;
        deletingGroupVm.toggleGroupDeleting = toggleGroupDeleting;
        deletingGroupVm.cancelGroupDeleting = cancelGroupDeleting;

        //////////////

        function toggleGroupDeleting() {
            deletingGroupVm.isGroupDeletingInProgress = !deletingGroupVm.isGroupDeletingInProgress;
        }

        function cancelGroupDeleting() {
            deletingGroupVm.isGroupDeletingInProgress = false;
        }
    };

})();