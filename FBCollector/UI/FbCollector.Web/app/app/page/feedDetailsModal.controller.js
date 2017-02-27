"use strict";

fbcApp.controller("feedDetailsModalController", [
    "$scope", "$mdDialog", "toastFactory", "feed", "pageService",
    function ($scope, $mdDialog, toastFactory, feed, pageService) {
        $scope.feed = feed;

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.confirm = function () {
            var data = {
                feedId: $scope.feed.Id
            };
            pageService
                .setFeedAsUsed(data)
                    .then(function (result) {
                        if (result) {
                            $mdDialog.hide();
                        }
                    }, function (error) {
                        $scope.ValidationErrors = TW.Utils.parseErrors($scope.model, error.data);
                        toastFactory.simple($scope.ValidationErrors);
                    });
        };
    }
]);