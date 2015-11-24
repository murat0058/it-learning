(function () {

    angular
        .module('app.groups')
        .controller('CreateGroupController', CreateGroupController);

    function CreateGroupController() {

        var createGroupVm = this;

        createGroupVm.IsPrivate = false;
    };

})();