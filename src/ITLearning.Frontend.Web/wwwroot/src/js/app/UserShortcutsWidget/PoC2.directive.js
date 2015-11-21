(function(){
	
	angular
		.module('app.userShortcutsWidget')
		.directive('itlWidgetPocSecond', itlWidgetPocSecond)
		
	function itlWidgetPocSecond() {
		return {
			restrict: 'E',
			replace: true,
			scope: {
			    parentVm: '=',
			    id: '@',
                type: '@'
			},
			template: '<div><p>PoC2</p></br></br></br></br></br></br><h3>Test</h3></div>',
			controller: PocSecondController,
            link: link,
			controllerAs: 'vm',
    	    bindToController: true
		}

		function link(scope, elem, attr) {
		    scope.vm.parentVm.registerDirective(scope.vm);
		}
	}
	
	function PocSecondController() {
		
		var vm = this;
		vm.activateRequest = activateRequest;
		
		/////////////////////
		
		function activateRequest(){
		    //on tab click
		}
	}
	
})();