"use strict";

fbcApp.controller("courseMembersModalController", [
    "$scope", "$mdDialog", "course", "toastFactory", "configService",
    function ($scope, $mdDialog, course, toastFactory, configService) {

        $scope.items = [];
        $scope.selected = [];
        $scope.searchTxt = "";
        $scope.searchTextLabel = TW.Utils.LocalizedString("SEARCH_TEXT_LABEL");

        var dataForSend = {
            courseId: course.Id
        };
        configService
            .getAllCourseMembersIds(dataForSend)
                .then(function (result) {
                    $scope.selected = result;
                    loadUsers();
                }, function (error) { });

        function loadUsers() {
            var data = {
                searchText: $scope.searchTxt
            };
            configService
                .getActiveUsers(data)
                    .then(function (result) {
                        $scope.items = result;
                    }, function (error) { });
        };

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.confirm = function () {
            var data = {
                courseId: course.Id,
                members: $scope.selected
            };
            configService
                .saveCourseMembers(data)
                    .then(function (result) {
                        $mdDialog.cancel();
                    }, function (error) {
                        if (error.data) {
                            $scope.ValidationErrors = TW.Utils.parseErrors($scope.model, error.data);
                            toastFactory.simple($scope.ValidationErrors);
                        }
                    });
        };

        $scope.$watch("searchTxt", function(newVal, oldVal) {
            if (newVal !== oldVal)
                loadUsers();
        });

        $scope.toggle = function (item, list) {
            var idx = list.indexOf(item.Id);
            if (idx > -1) {
                list.splice(idx, 1);
            }
            else {
                list.push(item.Id);
            }
        };

        $scope.exists = function (item, list) {
            return list.indexOf(item.Id) > -1;
        };

        $scope.isIndeterminate = function () {
            return ($scope.selected.length !== 0 &&
                $scope.selected.length !== $scope.items.length);
        };

        $scope.isChecked = function () {
            return $scope.selected.length === $scope.items.length;
        };

        $scope.toggleAll = function () {
            if ($scope.selected.length === $scope.items.length) {
                $scope.selected = [];
            } else if ($scope.selected.length === 0 || $scope.selected.length > 0) {
                $scope.selected = $scope.items.map(function (a) { return a.Id; });
            }
        };
    }
]);