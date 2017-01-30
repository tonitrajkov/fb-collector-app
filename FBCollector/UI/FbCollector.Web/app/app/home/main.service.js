"use strict";

fbcApp.factory("mainService", [
    "repository", "$q", function (repository, $q) {
        return {
            logOut: function () {
                var url = "/Account/LogOut";
                var result = repository.promisePost(url, null);
                return result;
            },

            getLanguage: function (callBack) {
                //var url = "/Base/LoadLanguage";
                //repository.syncAjax(baseUrl + url, null, function (response) {
                //    callBack(response);
                //}, function (response) {
                //    console.log(response);
                //});
            },

            changeLanguage: function (data) {
                //var url = "/Base/ChangeLanguage";
                //var result = repository.promisePost(url, data);
                //return result;
            },

            getSupportedLanguages: function (callBack) {
                //var url = "/Base/GetSupportedLanguages";
                //repository.syncAjax(baseUrl + url, null, function (response) {
                //    callBack(response);
                //}, function (response) {
                //    console.log(response);
                //});
            },

            getLoggedInUserInfo: function (callBack) {
                //var url = "/Home/GetLoggedInUserInfo";
                //repository.syncAjax(baseUrl + url, null, function (response) {
                //    callBack(response);
                //}, function (response) {
                //    console.log(response);
                //});
            },

            getLoggedUserCourses: function () {
                //var url = "/Course/GetLoggedUserCourses";
                //var result = repository.promisePost(url, null);
                //return result;
            },

            // Notifications
            getAllNotificationsForLoggedUser: function (data) {
                //var url = "/Home/GetAllNotificationsForLoggedUser";
                //var result = repository.promisePost(url, data);
                //return result;
            },

            checkForUnseenNotifications: function () {
                //var url = "/Home/CheckForUnseenNotifications";
                //var result = repository.promisePost(url, null);
                //return result;
            },

            createPageFeed: function (data) {
                var url = "/PageFeed/CreatePageFeed";
                var result = repository.promisePost(url, data);
                return result;
            },

            getMyLastName: function () {
                var deferred = $q.defer();
                FB.api('/me', function (response) {
                    if (!response || response.error) {
                        deferred.reject('Error occured');
                    } else {
                        deferred.resolve(response);
                    }
                });
                return deferred.promise;
            },

            getPageInfo: function () {
                var deferred = $q.defer();
                FB.api(
                      '/DIYeverythings/feed',
                      'GET',
                      { "fields": "message,id,link,type,full_picture,name,shares,updated_time,created_time", "limit": "100", "show_expired": "true" },
                      function (response) {
                          if (!response || response.error) {
                              deferred.reject('Error occured');
                          } else {
                              deferred.resolve(response);
                          }
                      }
                    );

                return deferred.promise;
            }
        }
    }
]).factory("authHelper", [
    "$rootScope",
    function ($rootScope) {
        return {
            isAdmin: function () {
                try {
                    if ($rootScope.userInfo.IsAdmin)
                        return true;
                } catch (e) {
                    return false;
                }

                return false;
            },

            isTeacher: function () {
                try {
                    if ($rootScope.userInfo.IsTeacher)
                        return true;
                } catch (e) {
                    return false;
                }

                return false;
            },

            isStudent: function () {
                try {
                    if ($rootScope.userInfo.IsStudent)
                        return true;
                } catch (e) {
                    return false;
                }

                return false;
            },

            isAssistant: function () {
                try {
                    if ($rootScope.userInfo.IsAssistant)
                        return true;
                } catch (e) {
                    return false;
                }

                return false;
            },

            isTeamLead: function (project) {
                try {
                    var member = null;

                    angular.forEach(project.Members, function (value, key) {
                        if (value.Id === $rootScope.userInfo.Id) {
                            member = value;
                        }
                    });

                    if (member != null)
                        return member.IsTeamLead;

                } catch (e) {
                    return false;
                }

                return false;
            },

            canEditProject: function (project) {
                try {
                    if ($rootScope.userInfo.IsAdmin)
                        return true;

                    if ($rootScope.userInfo.IsTeacher &&
                        $rootScope.userInfo.Id === project.CreatedBy.Id &&
                        project.State.Id === 4)
                        return true;

                } catch (e) {
                    return false;
                }

                return false;
            },

            canManageTasks: function (project) {
                try {
                    if ($rootScope.userInfo.IsAdmin)
                        return true;

                    if (($rootScope.userInfo.IsTeacher &&
                        $rootScope.userInfo.Id === project.CreatedBy.Id ||
                        this.isTeamLead(project)) &&
                        project.State.Id === 5)
                        return true;

                } catch (e) {
                    return false;
                }

                return false;
            },

            canEditDeleteTask: function (task) {
                try {

                    if ($rootScope.userInfo.IsAdmin)
                        return true;

                    if (($rootScope.userInfo.IsTeacher && $rootScope.userInfo.Id === task.Project.CreatedBy.Id) &&
                        task.Project.State.Id === 5)
                        return true;

                    if (task.State.Id === 11)
                        return false;

                    if (this.isTeamLead(task.Project) && task.Project.State.Id === 5)
                        return true;

                } catch (e) {
                    return false;
                }

                return false;
            },

            canChangeTaskState: function (task) {
                try {
                    if ($rootScope.userInfo.IsAdmin)
                        return true;

                    if (($rootScope.userInfo.IsTeacher && $rootScope.userInfo.Id === task.Project.CreatedBy.Id) &&
                        task.Project.State.Id === 5)
                        return true;

                    if (task.State.Id === 11)
                        return false;

                    if (this.isTeamLead(task.Project) && task.Project.State.Id === 5)
                        return true;

                    try {
                        var member = null;

                        angular.forEach(task.Members, function (value, key) {
                            if (value.Id === $rootScope.userInfo.Id) {
                                member = value;
                            }
                        });

                        if (member != null)
                            return true;

                    } catch (e) {
                        return false;
                    }
                } catch (e) {
                    return false;
                }

                return false;
            }
        };
    }
]);

fbcApp.factory("mainViewModels",
[
    function () {
        var modelRef = this;
        modelRef.PageFeedModel = function (fbmodel) {
            var self = this;
            self.PostId = fbmodel.id;
            self.Link = fbmodel.link;
            self.PostPicture = fbmodel.full_picture;
            self.Message = null;
            self.Type = fbmodel.type;
            self.PostName = fbmodel.name;
            self.FbCreatedTime = fbmodel.created_time;
            self.FbUpdatedTime = fbmodel.updated_time;
            self.TimeCreaded = new Date(fbmodel.created_time);
            self.TimeUpdated = new Date(fbmodel.updated_time);
            self.Shares = fbmodel.shares.count;
            self.PageId = "DIYeverythings";
            self.DateImported = new Date();
            self.IsUsed = false;
            self.DateUsed = null;
        };

        return modelRef;
    }
]);