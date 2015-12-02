(function () {

    angular
        .module('app.groups')
        .controller('EditGroupController', EditGroupController);

    function EditGroupController() {

        var editGroupVm = this;

        editGroupVm.IsPrivate = false;
    };

})();