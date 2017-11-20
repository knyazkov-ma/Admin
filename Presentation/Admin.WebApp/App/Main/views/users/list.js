(function () {
    var app = angular.module('app');

    var controllerId = 'app.views.users.list';
    app.controller(controllerId, ['$rootScope', '$scope', '$state', '$http', '$timeout', 'userService', 'modalService',
    function ($rootScope, $scope, $state, $http, $timeout, userService, modalService) {

        var vm = this;
        vm.showAlert = true;

        vm.orderInfo = { propertyName: "Name", asc: false };
        
        vm.filter = {
            Name: null,
            Email: null,
            UserTypes: []
        };
        
        extendColumnHeader(vm);
        
        userService.getListUserType().then(function (results) {
            vm.filterUserTypes = results.data.data;
        }, function (error) {
            $rootScope.$broadcast("error", { errorMsg: error.data.Message });
        });
        
        vm.loaded = false;
        vm.refresh = function () {
            vm.loaded = false;
            
            userService.getList(vm.filter, vm.orderInfo, { currentPage: vm.currentPage - 1, pageSize: vm.pageSize }).then(function (results) {
                vm.recs = results.data.data;
                vm.totalCount = results.data.totalCount;
                vm.count = results.data.count;
                vm.loaded = true;
            }, function (error) {
                $rootScope.$broadcast("error", { errorMsg: error.data.Message });
            });
        };
   
        vm.delete = function (id) {
            if (confirm(Admin.WebApp.Resources.ConfirmDeleteMessage)) {
                userService.delete(id).then(function (results) {
                    vm.errors = {};
                    vm.showAlert = true;
                    if (results.data.success === false) {
                        vm.errors = results.data.errors;
                    }
                    else {
                        vm.refresh();
                    }

                }, function (error) {
                    $rootScope.$broadcast("error", { errorMsg: error.data.Message });
                });
            }
       }

        vm.edit = function (id) {
            modalService.showModal({
                templateUrl: baseUrl + "/AngularTemplate/UserCard",
                controller: "app.views.users.card as vm",
                inputs: {
                    params: { Id: id }
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {

                    if (result.cancel)
                        return;
                    vm.refresh();
                });
            });
        }

        
        vm.closeAlert = function () {
            vm.showAlert = false;
        }
        
        vm.refresh();

    }

    ]);
})();