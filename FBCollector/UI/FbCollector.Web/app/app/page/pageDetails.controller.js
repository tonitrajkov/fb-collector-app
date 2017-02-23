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
                Type: null
            };

            $scope.lastRun = false;
            $scope.list = [];

            var data = { pageId: $stateParams.pageId };
            pageService
              .getPageById(data)
                .then(function (result) {
                    $scope.page = result;

                    $scope.query.PageUrlId = result.UrlId;
                    $scope.loadFeeds();
                }, function (error) {

                });

            $scope.loadFeeds = function () {
                pageService
                    .getPageFeedsFiltered($scope.query)
                    .then(function (result) {
                        if (result) {
                            $scope.feeds = result.Items;
                            $scope.totalRecords = result.TotalItems;
                        }
                    });
            };

            $scope.loadRecentReplies = function () {
                //var data = {
                //    objectId: $stateParams.courseId,
                //    objectType: 1
                //};

                //courseService
                //    .getRecentReplies(data)
                //    .then(function (result) {
                //        if (result) {
                //            $scope.courseRecentReplies = result;
                //        }
                //    });
            };

            //$scope.loadRecentReplies();

            $scope.syncFbModal = function (ev) {
                $mdDialog.show({
                    locals: { pageUrlId: $scope.page.UrlId },
                    controller: "syncFbModalController",
                    templateUrl: "app/partials/page/sync-fb-modal.html",
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    clickOutsideToClose: true,
                    fullscreen: true // Only for -xs, -sm breakpoints.
                })
              .then(function (accessToken) {
                  if (accessToken) {
                      $localStorage.accessToken = accessToken;
                      $scope.reloadTable();
                  }
              });
            };

            $scope.syncPageFeed = function () {
                var data = {
                    pageUrlId: $scope.page.UrlId,
                    accessToken: $localStorage.accessToken
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

            $scope.synchronizeWithFacebook = function(ev) {
                if ($localStorage.accessToken)
                    $scope.syncPageFeed();
                else
                    $scope.syncFbModal(ev);
            };

            $scope.reloadTable = function () {
                $scope.query = {
                    CurrentPage: 1,
                    ItemsPerPage: 15,
                    PageUrlId: $scope.page.UrlId,
                    SearchText: "",
                    IsUsed: null,
                    Type: null
                };

                $scope.loadFeeds();
            };

            $scope.$watchGroup(["query.SearchText"],
               function (newValues, oldValues) {
                   if (newValues !== oldValues)
                       $scope.loadFeeds();
               });
        }
    ]);