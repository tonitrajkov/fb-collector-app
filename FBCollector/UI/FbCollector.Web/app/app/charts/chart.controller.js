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

            $scope.labels2 = ["Download Sales", "In-Store Sales", "Mail-Order Sales", "Tele Sales", "Corporate Sales"];
            $scope.data2 = [300, 500, 100, 40, 120];

            $scope.series3 = ['Series A', 'Series B'];

            $scope.data3 = [[{ x: 40, y: 10, r: 20 }], [{ x: 10, y: 40, r: 50 }]];

            $scope.labels4 = ["Download Sales", "In-Store Sales", "Mail-Order Sales", "Tele Sales", "Corporate Sales"];
            $scope.data4= [300, 500, 100, 40, 120];
            $scope.type4 = 'polarArea';

            $scope.toggle = function () {
                $scope.type4 = $scope.type4 === 'polarArea' ?
                  'pie' : 'polarArea';
            };

            $scope.colors5 = ['#45b7cd', '#ff6384', '#ff8e72'];

            $scope.labels5 = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];
            $scope.data5 = [
              [65, -59, 80, 81, -56, 55, -40],
              [28, 48, -40, 19, 86, 27, 90]
            ];
            $scope.datasetOverride = [
              {
                  label: "Bar chart",
                  borderWidth: 1,
                  type: 'bar'
              },
              {
                  label: "Line chart",
                  borderWidth: 3,
                  hoverBackgroundColor: "rgba(255,99,132,0.4)",
                  hoverBorderColor: "rgba(255,99,132,1)",
                  type: 'line'
              }
            ];
        }
    ]);