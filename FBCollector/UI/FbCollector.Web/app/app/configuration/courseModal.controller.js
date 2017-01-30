"use strict";

fbcApp.controller("courseModalController", [
    "$scope", "$mdDialog", "toastFactory", "course", "semesterTypes", "states", "configViewModels", "configService",
    function ($scope, $mdDialog, toastFactory, course, semesterTypes, states, configViewModels, configService) {

        $scope.model = new configViewModels.CourseModel();
        $scope.semesterTypes = semesterTypes;
        $scope.modalTitle = TW.Utils.LocalizedString("COURSE_ADD_MODAL_TITLE");

        if (course) {
            $scope.model = angular.copy(course);
            $scope.modalTitle = TW.Utils.LocalizedString("COURSE_EDIT_MODAL_TITLE");
            $scope.states = states;
        }

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.confirm = function () {
            $scope.ClearErrors();

            if (course) {
                configService
                    .updateCourse($scope.model)
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
                    .createCourse($scope.model)
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