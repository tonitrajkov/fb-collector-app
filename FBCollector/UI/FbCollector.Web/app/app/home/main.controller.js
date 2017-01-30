"use strict";

fbcApp.controller("mainController",
    [
        "$scope", "$rootScope", "$window", "$mdSidenav", "mainService", "mainViewModels",
        function ($scope, $rootScope, $window, $mdSidenav, mainService, mainViewModels) {
            $rootScope.userInfo = {};

            //mainService.getLanguage(function (result) {
            //    if (result) {
            //        TW.Language = result;
            //    }
            //});

            //mainService.getSupportedLanguages(function (result) {
            //    if (result) {
            //        $scope.languages = result;

            //        angular.forEach($scope.languages, function (language) {
            //            if (language.CurrentLanguage)
            //                $scope.currentLanguage = language;
            //            else if (language.Default)
            //                $scope.currentLanguage = language;
            //        });
            //    }
            //});

            //mainService.getLoggedInUserInfo(function (result) {
            //    $rootScope.userInfo = result;
            //});

            /// $scope.isAdmin = authHelper.isAdmin();

            //$rootScope.$on("$stateChangeStart",
            //    function (event, toState, toParams, fromState, fromParams, options) {
            //        if (toState && (toState.name === "configuration" || toState.name === "cfgUsers"
            //            || toState.name === "cfgCourses") && !authHelper.isAdmin()) {
            //            $state.go("noPermission");
            //            event.preventDefault();
            //        } 
            //    });

            $scope.getLastName = function () {
                mainService.getPageInfo()
                .then(function (response) {
                    if (response.data) {
                        var list = [];

                        for (var i = 0; i < response.data.length; i++) {
                            var feed = new mainViewModels.PageFeedModel(response.data[i]);
                           try {
                               feed.Message = response.data[i].message;
                           } catch (e) {
                               feed.Message = null;
                           }

                            list.push(feed);
                        }

                        var data = {
                            feeds: list
                        };
                        mainService
                            .createPageFeed(data)
                             .then(function (result) {
                                    alert("okej");
                                }
                        );
                    }
                }
              );
            };

            // Header ---------------------------
            $scope.openSearch = function () {
                angular.element("#header").addClass("search-toggled");
            };

            $scope.closeSearch = function () {
                angular.element("#header").removeClass("search-toggled");
            };

            $scope.changeLanguage = function (language) {
                $scope.currentLanguage = language;

                var data = {
                    langName: language.Code
                };
                mainService
                    .changeLanguage(data)
                     .then(function (result) {
                         if (result === true) {
                             $window.location.reload();
                         }
                     }
                );
            };

            // Sidenav ----------------------------
            $scope.toggleSidenav = function () {
                return $mdSidenav("left").toggle();
            };

            $scope.redirectTo = function (url) {
                $window.location.href = baseUrl + url;
            };

            $scope.userMenuIsOpen = false;

            $scope.logOut = function () {
                //mainService
                //   .logOut()
                //       .then(function (result) {
                //           if (result) {
                //               $window.location.reload();
                //           }
                //       });
            };

        }
    ]);