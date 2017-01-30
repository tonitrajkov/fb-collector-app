"use strict";

fbcApp.controller("configController",
[
    "$scope", "$rootScope", "$window", "$mdSidenav", "mainService",
    function ($scope, $rootScope, $window, $mdSidenav, mainService) {

        $scope.activeTab = [false, false];

        $scope.userTabLabel = TW.Utils.LocalizedString("USERS_TAB_LABEL");
        $scope.courseTabLabel = TW.Utils.LocalizedString("COURSES_TAB_LABEL");

        $rootScope.$on('$stateChangeSuccess',
            function(event, toState, toParams, fromState, fromParams) {
                var a = 2;
            });
    }
]);