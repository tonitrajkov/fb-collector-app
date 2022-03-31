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

        // Pages
        getPagesFiltered: function (data) {
            var url = "/Page/GetPagesFiltered";
            var result = repository.promisePost(url, data);
            return result;
        },

        createPage: function (data) {
            var url = "/Page/CreatePage";
            var result = repository.promisePost(url, data);
            return result;
        },

        updatePage: function (data) {
            var url = "/Page/UpdatePage";
            var result = repository.promisePost(url, data);
            return result;
        },

        deletePage: function (data) {
            var url = "/Page/DeletePage";
            var result = repository.promisePost(url, data);
            return result;
        },

        getImportanceLevels: function () {
            var url = "/Base/GetImportanceLevels";
            var result = repository.promisePost(url, null);
            return result;
        }
    };
}]);