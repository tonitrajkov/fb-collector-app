"use strict";

fbcApp.controller("usersController",
    ["$scope", "$mdEditDialog", "$mdDialog", "$timeout", "configService",
        function ($scope, $mdEditDialog, $mdDialog, $timeout, configService) {

            $scope.options = {
                rowSelection: true,
                multiSelect: true,
                autoSelect: true,
                decapitate: false,
                largeEditDialog: false,
                boundaryLinks: false,
                limitSelect: true,
                pageSelect: true
            };

            $scope.limitOptions = [5, 10, 15, 25, 50];

            $scope.query = {
                CurrentPage: 1,
                ItemsPerPage: 15,
                SearchText: "",
                Role: null,
                Active: null
            };

            $scope.showFilters = false;
            $scope.searchTextLabel = TW.Utils.LocalizedString("SEARCH_TEXT_LABEL");

            $scope.loadUsers = function () {
                configService
                    .getAllUsersFiltered($scope.query)
                    .then(function (result) {
                        if (result) {
                            $scope.users = result.Items;
                            $scope.totalRecords = result.TotalItems;
                        }
                    });
            };

            $scope.loadUsers();

            // load roles
            var data = {
                typeId: 1
            };
            configService
                .getAllRolesByType(data)
                    .then(function (result) {
                        $scope.userRoles = result;
                    }, function (error) {

                    });

            $scope.reloadTable = function () {
                $scope.query = {
                    CurrentPage: 1,
                    ItemsPerPage: 15,
                    SearchText: "",
                    Role: null,
                    Active: null
                };

                $scope.loadUsers();
            };

            $scope.toggleLimitOptions = function () {
                $scope.limitOptions = $scope.limitOptions ? undefined : [5, 10, 15, 25, 50];
            };

            $scope.onPaginate = function (page, limit) {
                $scope.loadUsers();
            };

            $scope.openUserModal = function (ev, user) {
                $mdDialog.show({
                    locals: { user: user, roles: $scope.userRoles },
                    controller: "userModalController",
                    templateUrl: "app/partials/configuration/user-modal.html",
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    clickOutsideToClose: true,
                    fullscreen: true // Only for -xs, -sm breakpoints.
                })
                    .then(function () {
                        $scope.reloadTable();
                    });
            };

            $scope.onChangeSwitch = function (user) {
                var data = {
                    userId: user.Id,
                    value: user.Active
                };

                configService
                    .toggleActiveUser(data)
                        .then(function (result) {
                            $scope.reloadTable();
                        }, function () {

                        });
            };

            $scope.$watchGroup(["query.SearchText", "query.Role", "query.Active"], function (newValues, oldValues) {
                if (newValues !== oldValues)
                    $scope.loadUsers();
            });

        }
    ]);