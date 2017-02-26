"use strict";

fbcApp.controller("pageModalController", [
    "$scope", "$mdDialog", "toastFactory", "page", "levels", "configViewModels", "configService",
    function ($scope, $mdDialog, toastFactory, page, levels, configViewModels, configService) {

        $scope.model = new configViewModels.PageModel();
        $scope.modalTitle = TW.Utils.LocalizedString("PAGE_ADD_MODAL_TITLE");
        $scope.levels = levels;

        if (page) {
            $scope.model = angular.copy(page);
            $scope.modalTitle = TW.Utils.LocalizedString("PAGE_EDIT_MODAL_TITLE");
        }

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.confirm = function () {
            $scope.ClearErrors();

            if (page) {
                configService
                    .updatePage($scope.model)
                    .then(function (result) {
                        if (result) {
                            $mdDialog.hide();
                        }
                    }, function (error) {
                        if (error.data.Errors != undefined && error.data.Errors.length !== 0) {
                            TW.Utils.parseErrors($scope.model, error.data);
                        } else {
                            $scope.ValidationErrors = TW.Utils.parseErrors($scope.model, error.data);
                            toastFactory.simple($scope.ValidationErrors);
                        }
                    });
            } else {
                configService
                    .createPage($scope.model)
                    .then(function (result) {
                        if (result) {
                            $mdDialog.hide();
                        }
                    }, function (error) {
                        if (error.data.Errors != undefined && error.data.Errors.length !== 0) {
                            TW.Utils.parseErrors($scope.model, error.data);
                        } else {
                            $scope.ValidationErrors = TW.Utils.parseErrors($scope.model, error.data);
                            toastFactory.simple($scope.ValidationErrors);
                        }
                    });
            }
        };

        $scope.ClearErrors = function () {
            TW.Utils.clearErrors($scope.model);
            $scope.ValidationErrors = "";
        };
    }
]);