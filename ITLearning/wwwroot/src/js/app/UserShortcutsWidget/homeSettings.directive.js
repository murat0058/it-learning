(function(){
	
	angular
		.module('app.userShortcutsWidget')
		.directive('itlHomeSettings', itlHomeSettings)
		
	function itlHomeSettings() {
		return {
			restrict: 'E',
			replace: true,
			scope: {
				parentVm: '='
			},
			template: '<p>Home settings</p>',
			controller: HomeSettingsController, 
			controllerAs: 'vm',
    	    bindToController: true
		}
	}
	
	function HomeSettingsController(){
		
		var vm = this;
		vm.id = 0;
		vm.activateRequest = activateRequest;
		
		activate();
		/////////////////////
		
		function activate(){
			
			vm.parentVm.registerDirective(vm);
			console.log('home settings directive activated!');	
		}
		
		function activateRequest(){
			console.log('home settings directive activated by controller!');	
		}
	}
	
})();