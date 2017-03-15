"use strict";

fbcApp.controller("chartController",
    [
        "$scope", "$rootScope", "$window",
        function ($scope, $rootScope, $window) {
            
            $scope.labels = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
            $scope.data = [300, 500, 100];

            $scope.labels1 = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
            $scope.series1 = ['Series A', 'Series B'];

            $scope.data1 = [
              [65, 59, 80, 81, 56, 55, 40],
              [28, 48, 40, 19, 86, 27, 90]
            ];
        }
    ]);