(function(){
	
	angular
		.module('app.userShortcutsWidget')
		.controller('UserShortcutsWidgetController', userShortcutsWidgetController)
		
	function userShortcutsWidgetController(){
		
	    var vm = this;

	    vm.typeToColorMappings = {
	        'tab-default': '#fafafa',
            'tab-primary': '#018ee0',
            'tab-danger': '#ed4933',
            'tab-success': '#21b2a6',
            'tab-warning': '#ff8d00',
            'tab-dark': '#2e3842',
            'tab-lime': '#47FF5E',
        };
		
		vm.directives = [];
		vm.activeDirectiveId = null;
		vm.contentStyle = {};
		vm.registerDirective = registerDirective;
		vm.activateDirective = activateDirective;
		
		///////////////
		
		function registerDirective(directiveVm){
			vm.directives.push(directiveVm);
		}
		
		function activateDirective(directiveId) {

		    if (vm.activeDirectiveId === directiveId) {
		        vm.activeDirectiveId = null;

		        vm.contentStyle = {
		            'height': '0px',
		            'padding': '0px'
		        };

		    } else {
		        vm.activeDirectiveId = directiveId;

		        var directive = vm.directives.filter(function (d) {
		            return d.id === directiveId;
		        })[0];

		        vm.contentStyle = {
		            'background-color': vm.typeToColorMappings[directive.type],
                    'height': '100%',
                    'padding': '5px'
		        };

		        directive.activateRequest();
		    }
		}
	}
})();