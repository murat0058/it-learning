(function(){
	
	angular
		.module('app.userShortcutsWidget')
		.controller('UserShortcutsWidgetController', userShortcutsWidgetController)
		
	function userShortcutsWidgetController(){
		
		var vm = this;
		
		vm.directives = [];
		vm.registerDirective = registerDirective;
		vm.activateDirective = activateDirective;
		
		///////////////
		
		function registerDirective(directiveVm){
			vm.directives.push(directiveVm);
		}
		
		function activateDirective(directiveId){
			
			var directive = vm.directives.filter(function(d) { 
				return d.vm.id === directiveId; 
			})[0];
			
			directive.activateRequest();
		}
	}
})();