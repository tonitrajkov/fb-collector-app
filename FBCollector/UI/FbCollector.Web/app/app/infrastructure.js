"use strict";

fbcApp.factory("repository", ["$http", "$q", function ($http, $q) {
    return {
        promisePost: function (url, data) {
            var d = $q.defer();

            $http({
                method: "POST",
                url: baseUrl + url,
                async: false,
                cache: false,
                contentType: "application/json; charset=utf-8",
                mode: "queue",
                data: data,
                dataType: "json"
            }).then(function (response) {

                d.resolve(response.data);
            },
                function (response) {
                    d.reject(response);
                });

            return d.promise;
        },

        syncAjax: function (url, data, success, error) {
            $.ajax({
                mode: "queue",
                url: url,
                async: false,
                cache: false,
                contentType: "application/json; charset=utf-8",
                type: "POST",
                data: data,
                dataType: "json",
                success: function (d, s, x) {
                    if (success) {
                        if (d.hasOwnProperty("d")) {
                            var response = JSON.parse(d.d);
                        } else {
                            response = d;
                        }
                        success(response);
                    }
                },
                error: function (r, p, x) {
                    if (error) {
                        error(r, p, x);
                    }
                }
            });
        }
    };
}]);