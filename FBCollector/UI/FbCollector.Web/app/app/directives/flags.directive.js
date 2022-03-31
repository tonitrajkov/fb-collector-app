﻿"use strict";

fbcApp.directive("flag", [function () {
    return {
        restrict: "E",
        replace: true,
        template: '<span class="f{{ size }}"><span class="flags--item {{ country }}"></span></span>',
        scope: {
            country: "@",
            size: "@"
        },
        link: function (scope, elm, attrs) {
            // Default flag size
            scope.size = 16;

            scope.$watch("country", function (value) {
                scope.country = angular.lowercase(value);
            });

            scope.$watch("size", function (value) {
                scope.size = value;
            });
        }
    };
}]);