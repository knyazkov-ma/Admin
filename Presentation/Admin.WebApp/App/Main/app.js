(function () {
    'use strict';
    
    var app = angular.module('app', [
        'ngAnimate',
        'ngSanitize',
        'ngRoute',
        'ui.router',
        'ui.bootstrap',
        'ui.jq',
        'ui.keypress',
        'ui.event',
        'angular-loading-bar'
    ]);

    //Configuration for Angular UI routing.
    //culture - глобальная переменная, определена в \App\Main\views\layout\layout.cshtml
    app.config([
        '$stateProvider', '$urlRouterProvider', '$httpProvider',
        function ($stateProvider, $urlRouterProvider, $httpProvider, fileUploadProvider, $state) {
            
            $urlRouterProvider.otherwise('/');
            $stateProvider
                
                .state('users', {
                    url: '/',
                    templateUrl: '/' + culture + '/AngularTemplate/Users',
                    menu: 'users'
                });

            delete $httpProvider.defaults.headers.common['X-Requested-With'];
            
        }
    ]);

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push(function ($rootScope, $q) {
            return {
                request: function (config) {
                    config.timeout = 10000;
                    return config;
                },
                responseError: function (rejection) {
                    switch (rejection.status) {
                        case 408:
                            alert('connection timed out');
                            break;
                    }
                    return $q.reject(rejection);
                }
            }
        })
    });

    app.filter('timezone', function () {

        return function (val, offset) {
            if (val != null && val.length > 16) {
                return val.substring(0, 16)
            }
            return val;
        };
    });

})();