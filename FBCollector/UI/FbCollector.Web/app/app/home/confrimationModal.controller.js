"use strict";

fbcApp.controller("confirmationModalController", [
    "$scope", "$mdDialog", "title", "msg",
    function ($scope, $mdDialog, title, msg) {

        $scope.modalTitle = title;
        $scope.modalMessage = msg;

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.confirm = function () {
            $mdDialog.hide();
        };
    }
]);