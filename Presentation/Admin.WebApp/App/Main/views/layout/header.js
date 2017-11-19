(function () {
    
    var controllerId = 'app.views.layout.header';
    angular.module('app').controller(controllerId, [
        '$rootScope', '$state', '$location', '$interval', 'localizedMenuService',
        function ($rootScope, $state, $location, $interval, localizedMenuService) {

            
            var vm = this;
            vm.errorMsg = null;

           
            vm.menu = {};
            localizedMenu = localizedMenuService.get();
            vm.menu.items = [];
            for (var propertyName in localizedMenu) {
                vm.menu.items.push({ displayName: localizedMenu[propertyName], name: propertyName })
            }
                        
            vm.isActive = function(item) {
                return (item.name == $state.current.name);
            };

            vm.go = function(stateName) {
                localStorage.setItem('stateName', stateName);
                $state.go(stateName);
            };
           
            $rootScope.$on('error', function (event, data) {
                vm.errorMsg = data.errorMsg;
            });
            
            //чтобы при перезагрузке страницы возвращаться в текущее меню
            var stateName = localStorage.getItem('stateName');
            
            if (stateName && localizedMenu[stateName])
                vm.go(stateName);
            else
                vm.go('users');

            
        }
    ]);
})();