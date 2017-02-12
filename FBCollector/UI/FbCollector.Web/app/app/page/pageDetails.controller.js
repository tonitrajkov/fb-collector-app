"use strict";

fbcApp.controller("pageDetailsController",
    [
        "$scope", "$rootScope", "$mdDialog", "$stateParams", "toastFactory", "pageViewModels", "fbService", "pageService",
        function ($scope, $rootScope, $mdDialog, $stateParams, toastFactory, pageViewModels, fbService, pageService) {

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

            $scope.synchronizeWithFacebook = function (ev) {
                fbService.getPageFeed($scope.page.UrlId)
                   .then(function (response) {
                       if (response.data) {
                           savePageFeed(response.data);

                           if (response.paging && response.paging.next !== "undefined") {
                               fbService.getPageFeedPaging(response.paging.next, callbackFn);
                           } else {
                               $scope.lastRun = true;
                           }
                       }
                   });
            };

            function savePageFeed(feeds) {
             //   var list = [];

                for (var i = 0; i < feeds.length; i++) {
                    var feed = new pageViewModels.PageFeedModel(feeds[i], $scope.page.UrlId);
                    try {
                        feed.Message = feeds[i].message;
                    } catch (e) {
                        feed.Message = null;
                    }

                    $scope.list.push(feed);
                }

                //var data = {
                //    feeds: list
                //};
                //pageService
                //    .createPageFeed(data)
                //     .then(function (result) {
                //        if ($scope.lastRun)
                //            toastFactory.simple("DONE");
                //    });
            };

            function callbackFn(response) {
                savePageFeed(response.data);

                if (response.paging && response.paging.next !== "undefined") {
                    fbService.getPageFeedPaging(response.paging.next, callbackFn);
                } else {
                    $scope.lastRun = true;
                    var data = {
                        feeds: $scope.list
                    };
                    pageService
                        .createPageFeed(data)
                         .then(function (result) {
                            if ($scope.lastRun)
                                toastFactory.simple("DONE");
                        });
                }
            }

            $scope.$watchGroup(["query.SearchText"],
               function (newValues, oldValues) {
                   if (newValues !== oldValues)
                       $scope.loadFeeds();
               });
        }
    ]);