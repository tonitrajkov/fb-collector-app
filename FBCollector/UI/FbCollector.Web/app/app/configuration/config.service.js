"use strict";

fbcApp.factory("configService", ["repository", function (repository) {
    return {
        // Users
        getAllUsersFiltered: function (data) {
            //var url = "/Config/GetAllUsersFiltered";
            //var result = repository.promisePost(url, data);
            //return result;
        },

        createUser: function (data) {
            //var url = "/User/CreateUser";
            //var result = repository.promisePost(url, data);
            //return result;
        },

        updateUser: function (data) {
            //var url = "/User/UpdateUser";
            //var result = repository.promisePost(url, data);
            //return result;
        },

        toggleActiveUser: function (data) {
            //var url = "/User/ToggleActiveUser";
            //var result = repository.promisePost(url, data);
            //return result;
        },

        getActiveUsers: function (data) {
            //var url = "/User/GetActiveUsersWithoutAdmins";
            //var result = repository.promisePost(url, data);
            //return result;
        },

        // Roles
        getAllRolesByType: function (data) {
            //var url = "/Config/GetAllRolesByType";
            //var result = repository.promisePost(url, data);
            //return result;
        },

        // Courses
        getAllCoursesFiltered: function (data) {
            //var url = "/Course/GetAllCoursesFiltered";
            //var result = repository.promisePost(url, data);
            //return result;
        },

        createCourse: function (data) {
            //var url = "/Course/CreateCourse";
            //var result = repository.promisePost(url, data);
            //return result;
        },

        updateCourse: function (data) {
            //var url = "/Course/UpdateCourse";
            //var result = repository.promisePost(url, data);
            //return result;
        },

        deleteCourse: function (data) {
            //var url = "/Course/DeleteCourse";
            //var result = repository.promisePost(url, data);
            //return result;
        },

        getAllCourseMembersIds: function (data) {
            //var url = "/Course/GetAllCourseMembersIds";
            //var result = repository.promisePost(url, data);
            //return result;
        },

        saveCourseMembers: function (data) {
            //var url = "/Course/SaveCourseMembers";
            //var result = repository.promisePost(url, data);
            //return result;
        },

        // Semester types
        getAllSemesterTypes: function () {
            //var url = "/Config/GetAllSemesterTypes";
            //var result = repository.promisePost(url, null);
            //return result;
        },

        // States
        getAllStates: function (data) {
            //var url = "/Config/GetAllStates";
            //var result = repository.promisePost(url, data);
            //return result;
        }
    };
}]);