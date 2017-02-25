"use strict";

fbcApp.controller("feedDetailsModalController", [
    "$scope", "$mdDialog", "toastFactory", "feed", "configService",
    function ($scope, $mdDialog, toastFactory, feed, configService) {
        $scope.modalTitle = TW.Utils.LocalizedString("COURSE_EDIT_MODAL_TITLE");
        $scope.feed = feed;

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.confirm = function () {
            $scope.ClearErrors();

            //configService
            //    .updatePage($scope.model)
            //    .then(function (result) {
            //        if (result) {
            //            $mdDialog.hide();
            //        }
            //    }, function (error) {
            //        if (error.data.Errors != undefined && error.data.Errors.length !== 0) {
            //            TW.Utils.parseErrors($scope.model, error.data);
            //        } else {
            //            $scope.ValidationErrors = TW.Utils.parseErrors($scope.model, error.data);
            //            toastFactory.simple($scope.ValidationErrors);
            //        }
            //    });

        };

        $scope.ClearErrors = function () {
            TW.Utils.clearErrors($scope.model);
            $scope.ValidationErrors = "";
        };
    }
]);