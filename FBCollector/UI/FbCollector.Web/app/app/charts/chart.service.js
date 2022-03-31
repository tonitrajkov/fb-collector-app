"use strict";

fbcApp.factory("chartService", ["repository", function (repository) {
    return {

        pageFeedGroupedByHourAndType: function (data) {
            var url = "/PageFeed/PageFeedGroupedByHourAndType";
            var result = repository.promisePost(url, data);
            return result;
        }
    };
}]);