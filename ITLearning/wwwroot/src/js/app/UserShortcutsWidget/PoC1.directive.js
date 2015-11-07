(function(){
	
	angular
		.module('app.userShortcutsWidget')
		.directive('itlWidgetPocFirst', itlWidgetPocFirst)
		
	function itlWidgetPocFirst() {
		return {
			restrict: 'E',
			replace: true,
			scope: {
			    parentVm: '=',
			    id: '@',
			    type: '@'
			},
			template: '<p>PoC1</p>',
            link: link,
			controller: PocFirstController,
			controllerAs: 'vm',
    	    bindToController: true
		}

		function link(scope, elem, attr) {
		    scope.vm.parentVm.registerDirective(scope.vm);
		}
	}
	
	function PocFirstController() {
		
		var vm = this;
		vm.activateRequest = activateRequest;
		
		/////////////////////
		
		function activateRequest(){
			console.log('PoC2 settings directive activated by controller!');	
		}
	}
	
})();