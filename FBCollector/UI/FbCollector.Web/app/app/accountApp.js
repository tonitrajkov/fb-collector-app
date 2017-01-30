"use strict";

angular.module("accountApp", ["ngMaterial", "ngMdIcons"])
    .config([
        "$mdThemingProvider",
        function ($mdThemingProvider) {

            //theming
            $mdThemingProvider.theme("default")
                .primaryPalette("light-blue")
                .accentPalette("amber");
        }
    ])
    .factory("toastFactory", ["$mdToast", function ($mdToast) {
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
    }])
    .controller("accountController",
    [
        "$scope", "$window", "toastFactory", "accountService", function ($scope, $window, toastFactory, accountService) {

            accountService.getLanguage(function (result) {
                if (result) {
                    TW.Language = result;
                }
            });

            accountService.getSupportedLanguages(function (result) {
                if (result) {
                    $scope.languages = result;

                    angular.forEach($scope.languages, function (language) {
                        if (language.CurrentLanguage)
                            $scope.currentLanguage = language;
                        else if (language.Default)
                            $scope.currentLanguage = language;
                    });
                }
            });

            $scope.loginHeaderTxt = TW.Utils.LocalizedString("LOGIN_HEADER_TEXT");
            $scope.userNameLabelTxt = TW.Utils.LocalizedString("USERNAME_LABEL");
            $scope.passwordLabelTxt = TW.Utils.LocalizedString("PASSWORD_LABEL");
            $scope.loginBtnTxt = TW.Utils.LocalizedString("LOGIN_BUTTON");
            $scope.forgotPassLinkTxt = TW.Utils.LocalizedString("FORGOT_PASSWORD_LINK");
            $scope.rememberMeTxt = TW.Utils.LocalizedString("REMEMBER_ME");
            $scope.emailTxt = TW.Utils.LocalizedString("EMAIL_LABEL");
            $scope.sendRequestTxt = TW.Utils.LocalizedString("SEND_REQUEST_BUTTON");
            $scope.backTooltip = TW.Utils.LocalizedString("BACK_LABEL");
            $scope.newPasswordLabel = TW.Utils.LocalizedString("NEW_PASSWORD_LABEL");
            $scope.retypePassword = TW.Utils.LocalizedString("RETYPE_NEW_PASSWORD_LABEL");
            $scope.changePasswordBtnTxt = TW.Utils.LocalizedString("CHANGE_PASSWORD");

            $scope.forgotPassword = false;
            if ($window.location.href.indexOf("ForgotPassword") !== -1)
                $scope.forgotPassword = true;

            $scope.backToLogin = function () {
                $window.location.href = baseUrl + "Account/Login";
            }

            $scope.changeLanguage = function (language) {
                $scope.currentLanguage = language;

                var data = {
                    langName: language.Code
                };
                accountService
                    .changeLanguage(data)
                     .then(function (result) {
                         if (result === true) {
                             $window.location.reload();
                         }
                     }
                );
            };

            //show error msg
            if (errorMsg) {
                toastFactory.simple(TW.Utils.LocalizedString(errorMsg));
            }
           
        }
    ])
    .factory("accountService", ["$http", "$q", function ($http, $q) {
        return {
            getLanguage: function (callBack) {
                var url = "/Base/LoadLanguage";
                $.ajax({
                    mode: "queue",
                    url: baseUrl + url,
                    async: false,
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    data: null,
                    dataType: "json",
                    success: function (d, s, x) {
                        if (callBack) {
                            if (d.hasOwnProperty("d")) {
                                var response = JSON.parse(d.d);
                            } else {
                                response = d;
                            }
                            callBack(response);
                        }
                    },
                    error: function (r, p, x) {
                        if (error) {
                            error(r, p, x);
                        }
                    }
                });
            },

            changeLanguage: function (data) {
                var url = "/Base/ChangeLanguage";
                var d = $q.defer();

                $http({
                    method: "POST",
                    url: baseUrl + url,
                    async: false,
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    mode: "queue",
                    data: data,
                    dataType: "json"
                }).then(function (response) {

                    d.resolve(response.data);
                },
                    function (response) {
                        d.reject(response);
                    });

                return d.promise;
            },

            getSupportedLanguages: function (callBack) {
                var url = "/Base/GetSupportedLanguages";
                $.ajax({
                    mode: "queue",
                    url: baseUrl + url,
                    async: false,
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    data: null,
                    dataType: "json",
                    success: function (d, s, x) {
                        if (callBack) {
                            if (d.hasOwnProperty("d")) {
                                var response = JSON.parse(d.d);
                            } else {
                                response = d;
                            }
                            callBack(response);
                        }
                    },
                    error: function (r, p, x) {
                        if (error) {
                            error(r, p, x);
                        }
                    }
                });
            }
        };
    }]);