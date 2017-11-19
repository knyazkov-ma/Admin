(function () {
    var app = angular.module('app');
    app.factory('userService', ['$http', 'localizedWebAPIService',
        function ($http, localizedWebAPIService) {

            var factory = {};

            var _getList = function (filter, orderInfo, pageInfo) {


                var s = (filter.Email != null && filter.Email != 'null') ? "&email=" + filter.Email : "";
                s += (filter.Name != null && filter.Name != 'null') ? "&name=" + filter.Name : "";

                for (var i = 0; i < filter.UserTypes.length; i++)
                    s += "&userTypeIds=" + filter.UserTypes[i].Id;


                return $http.get(localizedWebAPIService.get("User/GetList?propertyName=" + orderInfo.propertyName + "&asc=" + orderInfo.asc + "&currentPage=" + pageInfo.currentPage + "&pageSize=" + pageInfo.pageSize + s))
                    .then(function (results) {
                        return results;
                    });
            };

            var _save = function (user) {
                return $http.post(localizedWebAPIService.get("User/Save"), JSON.stringify(user)).then(function (results) {
                    return results;
                });
            };

            var _get = function (id) {
                return $http.get(localizedWebAPIService.get("User/Get?id=" + id))
                    .then(function (results) {
                        return results;
                    });
            };

            var _getListUserType = function () {

                var url = "User/GetListUserType";

                return $http.get(localizedWebAPIService.get(url))
                    .then(function (results) {
                        return results;
                    });
            };
                       
            var _delete = function (id) {
                return $http.delete(localizedWebAPIService.get("User/Delete/" + id)).then(function (results) {
                    return results;
                });
            };
            
            factory.getList = _getList;
            factory.save = _save;
            factory.get = _get;
            factory.getListUserType = _getListUserType;
            factory.delete = _delete;
            
            return factory;

        }
    ]);
})();