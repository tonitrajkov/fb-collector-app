"use strict";

fbcApp.controller("pagesController",
    ["$scope", "$mdEditDialog", "$mdDialog", "$timeout", "toastFactory", "modalFactory", "configService",
    function ($scope, $mdEditDialog, $mdDialog, $timeout, toastFactory, modalFactory, configService) {

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
            SearchText: "",
            Importance: null
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
        };

        $scope.loadPages();

        $scope.toggleLimitOptions = function () {
            $scope.limitOptions = $scope.limitOptions ? undefined : [5, 10, 15, 25, 50];
        };

        $scope.onPaginate = function (page, limit) {
            $scope.loadPages();
        };

        $scope.openPageModal = function (ev, page) {
            $mdDialog.show({
                locals: { page: page, levels: $scope.levels },
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
            var id = page.Id;

            modalFactory.confrimation(ev, "tets", "test msg",
                 function () {
                     var data = {
                         pageId: id
                     };
                     configService
                       .deletePage(data)
                           .then(function (result) {
                               if (result) {
                                   $scope.loadPages();
                               }
                           }, function (error) {
                               $scope.ValidationErrors = TW.Utils.parseErrors($scope.model, error.data);
                           toastFactory.simple($scope.ValidationErrors);
                       });
                 });
        };

        $scope.reloadTable = function () {
            $scope.query = {
                CurrentPage: 1,
                ItemsPerPage: 15,
                SearchText: ""
            };

            $scope.loadPages();
        };

        $scope.$watchGroup(["query.SearchText", "query.Importance"],
            function (newValues, oldValues) {
                if (newValues !== oldValues)
                    $scope.loadPages();
            });

        $scope.loadImportanceLevels = function () {
            configService
                .getImportanceLevels()
                .then(function (result) {
                    if (result) {
                        $scope.levels = result;
                    }
                });
        };

        $scope.loadImportanceLevels();

    }]);