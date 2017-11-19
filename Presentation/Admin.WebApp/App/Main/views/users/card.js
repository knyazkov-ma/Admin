(function () {
    var app = angular.module('app');

    var controllerId = 'app.views.users.card';
    app.controller(controllerId, [
        '$rootScope', '$element', 'close','userService', 'params',
        function ($rootScope, $element, close, userService, params) {

            var vm = this;

            vm.user = { UserType : {} };
            vm.errors = {};
            vm.userTypes = [];
            

            vm.invalidForm = function () {
                return !(vm.user.UserType.Id &&
                    vm.user.Name &&
                    vm.user.Email &&
                    vm.user.Password);
            };

            
            vm.save = function () {
                                
                userService.save(vm.user).then(function (results) {
                    
                    vm.errors = {};

                    if (results.data.success === false) {
                        vm.errors = results.data.errors;
                    }
                    else {
                        //закрытие диалога, обновление гриды на родительской форме
                        $element.modal('hide');
                        close({ cancel: false }, 300);
                    }
                }, function (error) {
                    $rootScope.$broadcast("error", { errorMsg: error.data.Message });
                });

            };

            vm.cancel = function () {
                $element.modal('hide');
                close({ cancel: true }, 300);
            };

            var init = function () {
                userService.get(params.Id).then(function (results) {
                    vm.user = results.data.data;
                }, function (error) {
                    $rootScope.$broadcast("error", { errorMsg: error.data.Message });
                });

                userService.getListUserType().then(function (results) {
                    vm.userTypes = results.data.data;
                    if (vm.userTypes)
                        vm.user.UserType.Id = vm.userTypes[0].Id;
                }, function (error) {
                    $rootScope.$broadcast("error", { errorMsg: error.data.Message });
                });
                                
                vm.errors = {};
            }
            
            init();
     
        }
    ]);
})();