(function(){
	
	angular
		.module('app.userShortcutsWidget')
		.directive('itlWidgetPocFirst', itlWidgetPocFirst)
		
	function itlWidgetPocFirst() {
		return {
			restrict: 'E',
			replace: true,
			scope: {
				parentVm: '='
			},
			template: '<p>PoC1</p>',
			controller: PocFirstController,
			controllerAs: 'vm',
    	    bindToController: true
		}
	}
	
	function PocFirstController() {
		
		var vm = this;
		vm.id = 0;
		vm.type = 'tab-primary';
		vm.activateRequest = activateRequest;
		
		activate();
		/////////////////////
		
		function activate(){
			
			vm.parentVm.registerDirective(vm);
			console.log('PoC1 directive activated!');	
		}
		
		function activateRequest(){
			console.log('PoC2 settings directive activated by controller!');	
		}
	}
	
})();