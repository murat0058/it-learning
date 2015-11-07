(function(){
	
	angular
		.module('app.userShortcutsWidget')
		.directive('itlWidgetPocSecond', itlWidgetPocSecond)
		
	function itlWidgetPocSecond() {
		return {
			restrict: 'E',
			replace: true,
			scope: {
				parentVm: '='
			},
			template: '<p>PoC2</p>',
			controller: PocSecondController,
			controllerAs: 'vm',
    	    bindToController: true
		}
	}
	
	function PocSecondController() {
		
		var vm = this;
		vm.id = 0;
		vm.type = 'tab-primary';
		vm.activateRequest = activateRequest;
		
		activate();
		/////////////////////
		
		function activate(){
			
			vm.parentVm.registerDirective(vm);
			console.log('PoC2 directive activated!');	
		}
		
		function activateRequest(){
			console.log('PoC2 directive activated by controller!');	
		}
	}
	
})();