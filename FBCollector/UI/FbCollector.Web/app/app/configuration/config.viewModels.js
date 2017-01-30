"use strict";

fbcApp.factory("configViewModels",
[
    function () {
        var modelRef = this;
        modelRef.UserModel = function () {
            var self = this;
            self.UserName = "";
            self.FullName = "";
            self.Email = "";
            self.Address = "";
            self.City = "";
            self.State = "";
            self.ProfilePicture = "";
            self.Roles = [];
        };

        modelRef.RoleModel = function () {
            var self = this;
            self.Id = "";
            self.Description = "";
            self.AlreadyHave = false;
        };

        modelRef.CourseModel = function() {
            var self = this;
            self.Title = "";
            self.Year = "";
            self.State = null;
            self.SemesterType = null;
        };

        return modelRef;
    }
]);