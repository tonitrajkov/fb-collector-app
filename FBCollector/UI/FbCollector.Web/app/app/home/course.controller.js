"use strict";

fbcApp.controller("courseController",
    [
        "$scope", "$rootScope", "$window", "$mdSidenav", "mainService", "authHelper",
        function ($scope, $rootScope, $window, $mdSidenav, mainService, authHelper) {
           
            //mainService
            //    .getLoggedUserCourses()
            //        .then(function (result) {
            //            $scope.courses = result;
            //        });


            $scope.getStyle = function (index) {
                return {
                    'z-index': (20 - (1 * index))
                }
            };

        }
    ]);