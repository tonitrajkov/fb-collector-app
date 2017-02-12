"use strict";

fbcApp.factory("pageService", ["repository", function (repository) {
    return {

        getPageById: function (data) {
            var url = "/Page/GetPageById";
            var result = repository.promisePost(url, data);
            return result;
        },

        updatePage: function (data) {
            var url = "/Page/UpdatePage";
            var result = repository.promisePost(url, data);
            return result;
        },

        getPageFeedsFiltered: function (data) {
            var url = "/PageFeed/GetPageFeedsFiltered";
            var result = repository.promisePost(url, data);
            return result;
        },

        createPageFeed: function (data) {
            var url = "/PageFeed/CreatePageFeed";
            var result = repository.promisePost(url, data);
            return result;
        },

        setFeedAsUsed: function (data) {
            var url = "/PageFeed/SetFeedAsUsed";
            var result = repository.promisePost(url, data);
            return result;
        }
    };
}]);