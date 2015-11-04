(function(){
	
	angular
		.module('app.userShortcutsWidget')
		.directive('itlProfileEditing', itlProfileEditing)
		
	function itlProfileEditing(){
		return {
			restrict: 'E',
			replace: true,
			scope: {
				parentVm: '='
			},
			template: '<p>Profile editing</p>',
			controller: ProfileEditingController, 
			controllerAs: 'vm',
    	    bindToController: true
		}
	}
	
	function ProfileEditingController(){
		
		var vm = this;
		vm.id = 1;
		vm.activateRequest = activateRequest;
		
		activate();
		/////////////////////
		
		function activate() {
			vm.parentVm.registerDirective(vm);
			console.log('profile editing directive activated!');
		}
		
		function activateRequest() {
			console.log('profile editing directive activated by controller!');
		}
	}
	
})();