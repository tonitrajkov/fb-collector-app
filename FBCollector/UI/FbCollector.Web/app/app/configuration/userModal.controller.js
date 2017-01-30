"use strict";

fbcApp.controller("userModalController", [
    "$scope", "$mdDialog", "user", "roles", "toastFactory", "configViewModels", "configService",
    function ($scope, $mdDialog, user, roles, toastFactory, configViewModels, configService) {

        $scope.model = new configViewModels.UserModel();
        $scope.uploadedImg = "";
        $scope.modalTitle = TW.Utils.LocalizedString("USER_ADD_MODAL_TITLE");
        $scope.roles = roles;

        if (user) {
            $scope.model = angular.copy(user);
            $scope.uploadedImg = user.ProfilePicture !== "" ? user.ProfilePicture : "";
            $scope.modalTitle = TW.Utils.LocalizedString("USER_EDIT_MODAL_TITLE");
        }

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.confirm = function () {
            $scope.ClearErrors();

            if (user) {
                configService
                    .updateUser($scope.model)
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
                    .createUser($scope.model)
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

        // Profile picture
        $scope.fileNameChanged = function (evt) {
            var file = evt.files[0];
            var reader = new FileReader();
            reader.onload = function (evt) {
                $scope.$apply(function ($scope) {
                    $scope.uploadedImg = evt.target.result;
                });
            };
            reader.readAsDataURL(file);
        };

        $scope.onChangeImgCrop = function ($dataURI) {
        };

        $scope.onLoadErrorImgCrop = function () {

        };
    }
]);