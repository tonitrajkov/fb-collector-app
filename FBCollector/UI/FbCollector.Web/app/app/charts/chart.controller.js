"use strict";

fbcApp.controller("chartController",
    [
        "$scope", "$rootScope", "$window", "chartService",
        function ($scope, $rootScope, $window, chartService) {

            $scope.query = {
                CurrentPage: 1,
                ItemsPerPage: 50,
                PageUrlId: "",
                SearchText: "",
                IsUsed: null,
                Type: null,
                DateFrom: null,
                DateTo: null,
                OrderDescending: true,
                SharesNumber: null,
                Year: null
            };


            $scope.loadFeeds = function () {
                chartService
                    .pageFeedGroupedByHourAndType($scope.query)
                    .then(function (result) {
                        if (result) {
                            $scope.feeds = result.Items;
                            $scope.totalRecords = result.TotalItems;
                        }
                    });
            };

            $scope.loadFeeds();

            $scope.labels = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
            $scope.data = [300, 500, 100];

            $scope.labels1 = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11'
            , '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24'];
            $scope.series1 = ['photo', 'link'];//, 'video', 'status'];

            $scope.data1 = [
              [65, 59, 80, 81, 56, 55, 40, 65, 59, 80, 81, 56, 55, 40, 65, 59, 80, 81, 56, 55, 40, 56, 55, 40],
              [28, 48, 40, 19, 86, 27, 90, 28, 48, 40, 19, 86, 27, 90, 28, 48, 40, 19, 86, 27, 90, 86, 27, 90]//,
             // [18, 4, 10, 79, 56, 17, 60, 18, 4, 10, 79, 56, 17, 60, 18, 4, 10, 79, 56, 17, 60, 56, 17, 60],
               //[28, 48, 40, 19, 86, 27, 90, 28, 48, 40, 19, 86, 57, 90, 28, 48, 40, 19, 86, 27, 90, 86, 27, 90]
            ];

            $scope.colors11 = ['#f30000', '#18f300', '#f30085', '#0600f3'];

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