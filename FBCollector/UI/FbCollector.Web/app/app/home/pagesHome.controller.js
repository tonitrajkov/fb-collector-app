"use strict";

fbcApp.controller("pagesHomeController",
    [
        "$scope", "$rootScope", "$window", "$mdSidenav", "mainService", "authHelper",
        function ($scope, $rootScope, $window, $mdSidenav, mainService, authHelper) {

            $scope.query = {
                CurrentPage: 1,
                ItemsPerPage: 15,
                SearchText: "",
                Importance: null
            };

            $scope.loadPages = function () {
                mainService
                    .getPagesFiltered($scope.query)
                    .then(function (result) {
                        if (result) {
                            $scope.pages = result.Items;
                        }
                    });
            };

            $scope.loadPages();

            $scope.$watchGroup(["query.SearchText", "query.Importance"],
             function (newValues, oldValues) {
                 if (newValues !== oldValues)
                     $scope.loadPages();
             });

        }
    ]);