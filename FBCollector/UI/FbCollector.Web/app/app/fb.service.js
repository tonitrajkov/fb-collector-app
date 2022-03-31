"use strict";

fbcApp.factory("fbService", [
    "$q", function ($q) {
        return {
            getMyLastName: function () {
                var deferred = $q.defer();
                FB.api("/me", function (response) {
                    if (!response || response.error) {
                        deferred.reject("Error occured");
                    } else {
                        deferred.resolve(response);
                    }
                });
                return deferred.promise;
            },

            getPageFeed: function (pageUrlId) {
                var deferred = $q.defer();
                var url = pageUrlId + "/feed";
                FB.api(url,
                    "GET",
                    { "fields": "message,id,link,type,full_picture,name,shares,updated_time,created_time", "limit": "100", "show_expired": "true" },
                    function (response) {
                        if (!response || response.error) {
                            deferred.reject("Error occured");
                        } else {
                            deferred.resolve(response);
                        }
                    }
                );

                return deferred.promise;
            },

            getPageFeedPaging: function (paginUrl, callback) {
                FB.api(paginUrl, callback);
            }
        }
    }
]);

