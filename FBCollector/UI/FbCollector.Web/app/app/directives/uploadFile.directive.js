"use strict";

fbcApp.directive("uiUploadFile", [function () {
    return {
        restrict: "E",
        templateUrl: "app/partials/directives/upload-file.tpl.html",
        scope: {
            image: "="
        },
        link: function (scope, element, attrs) {

            scope.placeholder = TW.Utils.LocalizedString("NO_FILE_CHOOSEN");
            var input = $(element[0].querySelector("#fileInput"));
            var button = $(element[0].querySelector("#uploadButton"));
            var textInput = $(element[0].querySelector("#textInput"));


            if (input.length && button.length && textInput.length) {
                button.click(function (e) {
                    input.click();
                });
                textInput.click(function (e) {
                    input.click();
                });
            }

            input.on("change", function (e) {
                var files = e.target.files;
                var reader = new FileReader();

                if (files[0]) {
                    reader.onload = function (evt) {
                        scope.$apply(function (scope) {
                            scope.image = evt.target.result;
                        });
                    };
                    reader.readAsDataURL(files[0]);

                    scope.fileName = files[0].name;
                } else {
                    scope.fileName = null;
                    scope.image = "";
                }
                scope.$apply();
            });
        }
    };
}]);