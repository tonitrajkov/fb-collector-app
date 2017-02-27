"use strict";

fbcApp.controller("syncFbModalController", [
    "$scope", "$mdDialog", "toastFactory", "pageUrlId", "pageService",
    function ($scope, $mdDialog, toastFactory, pageUrlId, pageService) {

        $scope.pageUrlId = pageUrlId;
        $scope.accessToken = "";
        $scope.ValidationErrors = "";

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.confirm = function () {
            $scope.ClearErrors();

            var data = {
                pageUrlId: $scope.pageUrlId,
                accessToken: $scope.accessToken
            };

            pageService
                .syncPageFeed(data)
                   .then(function (result) {
                       if (result) {
                           $mdDialog.hide($scope.accessToken);
                       }
                   }, function (error) {
                       $scope.ValidationErrors = TW.Utils.parseErrors($scope.model, error.data);
                       toastFactory.simple($scope.ValidationErrors);
                   });
        };

        $scope.ClearErrors = function () {
            $scope.ValidationErrors = "";
        };
    }
]);