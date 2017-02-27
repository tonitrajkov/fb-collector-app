"use strict";

var fbcApp = angular.module("fbcApp",
    ["ngMaterial",
     "ngMdIcons",
     "ui.router",
     "md.data.table",
     "ngImgCrop",
     "materialCalendar",
     "mdPickers",
     "ngScrollbars",
     "ngStorage"])
    .config(["$stateProvider", "$urlRouterProvider", "$mdThemingProvider", "$httpProvider",
        function ($stateProvider, $urlRouterProvider, $mdThemingProvider, $httpProvider) {

            // $httpProvider.interceptors.push("UnauthorizedExceptionInterceptor");

            //theming
            $mdThemingProvider.theme("default")
                .primaryPalette("red")
                .accentPalette("pink");

            $urlRouterProvider.otherwise("/home");

            $urlRouterProvider.when("/configuration", "/configuration/pages");

            //States
            $stateProvider
                .state("home", {
                    url: "/home",
                    views: {
                        'mainView': {
                            templateUrl: "app/partials/home/home.html",
                            controller: "pagesHomeController"
                        }
                    },
                    data: {
                        displayName: "HOME"
                    }
                })
                .state("pageDetails", {
                    url: "/page/{pageId:int}",
                    views: {
                        'mainView': {
                            templateUrl: "app/partials/page/page-details.html",
                            controller: "pageDetailsController"
                        }
                    },
                    data: {
                        displayName: "PAGE"
                    }
                })
                .state("configuration", {
                    url: "/configuration",
                    views: {
                        'mainView': {
                            templateUrl: "app/partials/configuration/configuration.html",
                            controller: "configController"
                        }
                    },
                    data: {
                        displayName: "CONFIGURATION"
                    }
                })
                .state("cfgUsers", {
                    url: "/users",
                    views: {
                        'configView': {
                            templateUrl: "app/partials/configuration/users.html",
                            controller: "usersController"
                        }
                    },
                    parent: "configuration",
                    data: {
                        displayName: "USERS"
                    }
                })
                .state("cfgPages", {
                    url: "/pages",
                    views: {
                        'configView': {
                            templateUrl: "app/partials/configuration/pages.html",
                            controller: "pagesController"
                        }
                    },
                    parent: "configuration",
                    data: {
                        displayName: "COURSES_TAB_LABEL"
                    }
                })
             .state("notFound", {
                 url: "/not-found",
                 views: {
                     'mainView': {
                         templateUrl: "app/partials/home/not-found-page.html"
                     }
                 }
             })
             .state("noPermission", {
                 url: "/not-authorized",
                 views: {
                     'mainView': {
                         templateUrl: "app/partials/home/no-permission-page.html"
                     }
                 }
             });
        }]);

fbcApp.factory("toastFactory", [
    "$mdToast", function ($mdToast) {
        return {
            simple: function (message, delay) {
                if (!delay)
                    delay = 5000;

                var toast = $mdToast.simple()
                    .textContent(message)
                    .action(TW.Utils.LocalizedString("CLOSE_TOASTER"))
                    .highlightAction(true)
                    .highlightClass("md-accent")
                    .position("top right")
                    .hideDelay(delay);

                $mdToast.show(toast);
            }
        };
    }
]);

fbcApp.factory("modalFactory", [
    "$mdDialog", function ($mdDialog) {
        return {
            confrimation: function (ev, title, message, callback) {
                $mdDialog.show({
                    locals: { title: title, msg: message },
                    controller: "confirmationModalController",
                    templateUrl: "app/partials/home/confrimation-modal.html",
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    clickOutsideToClose: true,
                    fullscreen: true // Only for -xs, -sm breakpoints.
                }).then(callback);
            }
        };
    }
]);


fbcApp.factory("UnauthorizedExceptionInterceptor",
[
    "$rootScope", "$location", "$log", "$q",
    function ($rootScope, $location, $log, $q) {
        var unauthorizedExceptionInterceptor = {
            request: function (config) {
                $rootScope.posting = new Date().getTime();
                return $q.resolve(config);
            },
            response: function (response) {
                $rootScope.posting = false;
                return $q.resolve(response);
            },
            responseError: function (rejection) {
                if (rejection && rejection.status === 409 && rejection.data && rejection.data.Message === "ITEM_DOESNT_EXIST") {
                    $location.path("/not-found");
                }

                return $q.reject(rejection);
            },
            error: function (rejection) {
                if (rejection.status === 401 || rejection.status === 409) {
                    // $location.path('/not-authorized');
                    return false;
                }

                return $q.reject(rejection);
            }
        };

        return unauthorizedExceptionInterceptor;
    }
]);

fbcApp.run(["$rootScope", "$window",
  function ($rootScope, $window) {

      $rootScope.user = {};

      $window.fbAsyncInit = function () {
          FB.init({
              appId: "1938500843049334",
              status: true,
              cookie: true,
              xfbml: true,
              version: "v2.4"
          });
      };

      (function (d) {
          // load the Facebook javascript SDK

          var js,
          id = "facebook-jssdk",
          ref = d.getElementsByTagName("script")[0];

          if (d.getElementById(id)) {
              return;
          }

          js = d.createElement("script");
          js.id = id;
          js.async = true;
          js.src = "//connect.facebook.net/en_US/sdk.js";

          ref.parentNode.insertBefore(js, ref);

      }(document));

  }]);