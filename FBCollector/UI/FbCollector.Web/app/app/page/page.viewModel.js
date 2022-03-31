"use strict";

fbcApp.factory("pageViewModels",
[
    function () {
        var modelRef = this;

        modelRef.PageFeedModel = function (fbmodel, urlId) {
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
            self.Shares = (fbmodel.shares && fbmodel.shares.count) ? fbmodel.shares.count : 0;
            self.PageId = urlId;
            self.DateImported = new Date();
            self.IsUsed = false;
            self.DateUsed = null;
        };

        return modelRef;
    }
]);