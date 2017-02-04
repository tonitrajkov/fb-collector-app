"use strict";

fbcApp.controller("pagesController",
    ["$scope", "$mdEditDialog", "$mdDialog", "$timeout", "configService",
    function ($scope, $mdEditDialog, $mdDialog, $timeout, configService) {

        $scope.options = {
            rowSelection: true,
            multiSelect: true,
            autoSelect: true,
            decapitate: false,
            largeEditDialog: false,
            boundaryLinks: false,
            limitSelect: true,
            pageSelect: true
        };

        $scope.limitOptions = [5, 10, 15, 25, 50];

        $scope.query = {
            CurrentPage: 1,
            ItemsPerPage: 15,
            SearchText: ""
        };

        $scope.showFilters = false;
        $scope.searchLabel = TW.Utils.LocalizedString("SEARCH_BY_TITLE");

        $scope.loadPages = function () {
            configService
                .getPagesFiltered($scope.query)
                    .then(function (result) {
                        if (result) {
                            $scope.pages = result.Items;
                            $scope.totalRecords = result.TotalItems;
                        }
                    });
        }

        $scope.loadPages();

        $scope.toggleLimitOptions = function () {
            $scope.limitOptions = $scope.limitOptions ? undefined : [5, 10, 15, 25, 50];
        };

        $scope.onPaginate = function (page, limit) {
            $scope.loadPages();
        };

        $scope.openPageModal = function (ev, page) {
            $mdDialog.show({
                locals: { page: page },
                controller: "pageModalController",
                templateUrl: "app/partials/configuration/page-modal.html",
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: true,
                fullscreen: true // Only for -xs, -sm breakpoints.
            })
            .then(function () {
                $scope.reloadTable();
            });
        };

        $scope.deletePage = function (ev, page) {
         
        };

        $scope.reloadTable = function () {
            $scope.query = {
                CurrentPage: 1,
                ItemsPerPage: 15,
                SearchText: ""
            };

            $scope.loadPages();
        };

        $scope.$watch("query.SearchText",
            function (newValues, oldValues) {
                if (newValues !== oldValues)
                    $scope.loadPages();
            });

    }]);