"use strict";

fbcApp.controller("coursesController",
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
            SemesterType: null,
            State: null
        };

        $scope.showFilters = false;
        $scope.searchLabel = TW.Utils.LocalizedString("SEARCH_BY_TITLE");

        $scope.loadCourses = function () {
            configService
                .getAllCoursesFiltered($scope.query)
                    .then(function (result) {
                        if (result) {
                            $scope.courses = result.Items;
                            $scope.totalRecords = result.TotalItems;
                        }
                    });
        }

        $scope.loadCourses();

        // load semester types
        configService
            .getAllSemesterTypes()
                .then(function (result) {
                    $scope.semesterTypes = result;
                }, function (error) { });

        // load states
        var data = {
            objectType: 1
        };
        configService
            .getAllStates(data)
                .then(function (result) {
                    $scope.states = result;
                }, function (error) { });

        $scope.toggleLimitOptions = function () {
            $scope.limitOptions = $scope.limitOptions ? undefined : [5, 10, 15, 25, 50];
        };

        $scope.onPaginate = function (page, limit) {
            $scope.loadCourses();
        };

        $scope.openCourseModal = function (ev, course) {
            $mdDialog.show({
                locals: { course: course, semesterTypes: $scope.semesterTypes, states: $scope.states },
                controller: "courseModalController",
                templateUrl: "app/partials/configuration/course-modal.html",
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: true,
                fullscreen: true // Only for -xs, -sm breakpoints.
            })
            .then(function () {
                $scope.reloadTable();
            });
        };

        $scope.openMembersModal = function (ev, course) {
            $mdDialog.show({
                locals: { course: course },
                controller: "courseMembersModalController",
                templateUrl: "app/partials/configuration/course-members-modal.html",
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: true,
                fullscreen: true // Only for -xs, -sm breakpoints.
            })
            .then(function () {
                $scope.reloadTable();
            });
        };

        $scope.deleteCourse = function (ev, course) {
         
        };

        $scope.reloadTable = function () {
            $scope.query = {
                CurrentPage: 1,
                ItemsPerPage: 15,
                SearchText: "",
                SemesterType: null,
                State: null
            };

            $scope.loadCourses();
        };

        $scope.$watchGroup(["query.SearchText", "query.SemesterType", "query.State"],
            function (newValues, oldValues) {
                if (newValues !== oldValues)
                    $scope.loadCourses();
            });

    }]);