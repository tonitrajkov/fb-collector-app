"use strict";

fbcApp.controller("fansModalController", [
    "$scope", "$mdDialog", "toastFactory", "fansData",
    function ($scope, $mdDialog, toastFactory, fansData) {
        $scope.fans = [];

        if (fansData.data != null && fansData.data.length > 0) {
            var data = fansData.data[0];
            if (data.values != null && data.values.length > 0) {
                var len = data.values.length;
                var values = null;

                for (var i = len - 1; i >= 0; i--) {
                    var pomValues = data.values[i];
                    if (pomValues.hasOwnProperty("value")) {
                        values = pomValues;
                        break;
                    }
                }
                if (!values) {
                    console.log("No values was founded");
                    return;
                }

                var list = [];
                angular.forEach(values.value, function (value, key) {
                    var obj = {
                        name: key,
                        number: value
                    };

                    list.push(obj);
                });

                list.sort(function(a, b) {
                     return b.number - a.number;
                });
                
                $scope.fans = list;
            }
        }

        $scope.confirm = function() {
            $mdDialog.cancel();
        };
    }
]);