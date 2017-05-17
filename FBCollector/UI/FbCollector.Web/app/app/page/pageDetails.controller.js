"use strict";

fbcApp.controller("pageDetailsController",
    [
        "$scope", "$rootScope", "$mdDialog", "$stateParams", "$localStorage", "toastFactory", "pageViewModels", "fbService", "pageService",
        function ($scope, $rootScope, $mdDialog, $stateParams, $localStorage, toastFactory, pageViewModels, fbService, pageService) {

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
                ItemsPerPage: 50,
                PageUrlId: "",
                SearchText: "",
                IsUsed: null,
                Type: null,
                DateFrom: null,
                DateTo: null,
                OrderDescending: false,
                SharesNumber: null,
                Year: null
            };

            $scope.showFilters = false;
            $scope.pageLikes = 0;

            var data = { pageId: $stateParams.pageId };
            pageService
              .getPageById(data)
                .then(function (result) {
                    $scope.page = result;

                    $scope.loadPageDetails();

                    $scope.query.PageUrlId = result.UrlId;
                    $scope.loadFeeds();
                    $scope.loadFansByCountry();
                }, function (error) {
                    $scope.ValidationErrors = TW.Utils.parseErrors($scope.model, error.data);
                    toastFactory.simple($scope.ValidationErrors);
                });

            $scope.loadFeeds = function () {
                pageService
                    .getPageFeedsFiltered($scope.query)
                    .then(function (result) {
                        if (result) {
                            $scope.feeds = result.Items;
                            $scope.totalRecords = result.TotalItems;
                        }
                    }, function (error) {
                        $scope.ValidationErrors = TW.Utils.parseErrors($scope.model, error.data);
                        toastFactory.simple($scope.ValidationErrors);
                    });
            };

            $scope.loadPageDetails = function () {
                var data = {
                    pageUrlId: $scope.page.UrlId
                };

                pageService
                    .getPageDetails(data)
                       .then(function (result) {
                           $scope.fbPageDetails = result;
                       }, function (error) {
                           $scope.ValidationErrors = TW.Utils.parseErrors($scope.model, error.data);
                           toastFactory.simple($scope.ValidationErrors);
                       });
            };

            $scope.loadFansByCountry = function () {
                var data = {
                    pageUrlId: $scope.page.UrlId
                };

                pageService
                  .getPageFansByCounty(data)
                        .then(function (result) {
                            $scope.fansData = angular.fromJson(result);

                            if ($scope.fansData.data != null && $scope.fansData.data.length > 0) {
                                var data = $scope.fansData.data[0];
                                if (data.values != null && data.values.length > 0) {
                                    var len = data.values.length;
                                    var values = null;

                                    for (var i = len - 1; i >= 0; i--) {
                                        var pomValues = data.values[i];
                                        if (pomValues.hasOwnProperty("value")) {
                                            values = pomValues;
                                            break;
                                        }
                                    }
                                    if (!values) {
                                        console.log("No values was founded");
                                        return;
                                    }
                                    var labels = [];
                                    var chartData = [];
                                    var obj = values.value;
                                    var fansNumber = 0;

                                    for (var name in obj) {
                                        labels.push(name);
                                        var value = obj[name];
                                        chartData.push(value);
                                        fansNumber += value;
                                    }

                                    $scope.labels = labels;
                                    $scope.data = chartData;
                                    $scope.pageLikes = fansNumber;
                                }
                            }

                        }, function (error) {
                            $scope.ValidationErrors = TW.Utils.parseErrors($scope.model, error.data);
                            toastFactory.simple($scope.ValidationErrors);
                        });
            };

            $scope.syncPageFeed = function () {
                var data = {
                    pageUrlId: $scope.page.UrlId
                };

                pageService
                    .syncPageFeed(data)
                       .then(function (result) {
                           $scope.reloadTable();
                       }, function (error) {
                           $scope.ValidationErrors = TW.Utils.parseErrors($scope.model, error.data);
                           toastFactory.simple($scope.ValidationErrors);
                       });
            };

            $scope.fansModal = function (ev) {
                $mdDialog.show({
                    locals: { fansData: $scope.fansData },
                    controller: "fansModalController",
                    templateUrl: "app/partials/page/fans-modal.html",
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    clickOutsideToClose: true,
                    fullscreen: true // Only for -xs, -sm breakpoints.
                })
              .then(function () {
                  // dismiss
              });
            };

            $scope.openFeedModal = function (ev, feed) {
                $mdDialog.show({
                    locals: { feed: feed },
                    controller: "feedDetailsModalController",
                    templateUrl: "app/partials/page/feed-details-modal.html",
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    clickOutsideToClose: true,
                    fullscreen: true // Only for -xs, -sm breakpoints.
                })
                    .then(function () {
                        $scope.reloadTable();
                    });
            };

            $scope.onPaginate = function (page, limit) {
                $scope.loadFeeds();
            };

            $scope.reloadTable = function () {
                //$scope.query = {
                //    CurrentPage: 1,
                //    ItemsPerPage: 50,
                //    PageUrlId: $scope.page.UrlId,
                //    SearchText: "",
                //    IsUsed: null,
                //    Type: null,
                //    DateFrom: null,
                //    DateTo: null,
                //    OrderDescending: true,
                //    SharesNumber: null
                //};

                $scope.loadFeeds();
            };

            $scope.$watchGroup(["query.SearchText", "query.IsUsed", "query.Type", "query.DateFrom",
                "query.DateTo", "query.OrderDescending", "query.SharesNumber"],
               function (newValues, oldValues) {
                   if (newValues !== oldValues)
                       $scope.loadFeeds();
               });
        }
    ]);