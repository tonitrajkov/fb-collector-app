"use strict";

if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != "undefined"
              ? args[number]
              : match
            ;
        });
    };
}

var TW = {
    Language: { Items: [] },
    Utils: {
        LocalizedString: function(key) {
            try {
                for (var i = 0; i < TW.Language.Items.length; i++) {
                    var item = TW.Language.Items[i];
                    if (item.Key == key) {
                        return item.Value;
                    }
                }
                return key;
            } catch (e) {
                return key;
            }
        },
        OverrideLocalizationValues: function(localLanguage) {
            angular.forEach(localLanguage, function(value, key) {
                if (!localLanguage[key]) {
                    delete localLanguage[key];
                } else if (angular.isArray(localLanguage[key])) {
                    localLanguage[key].length = localLanguage[key].length;
                }
            });
            angular.forEach(localLanguage, function(value, key) {
                if (angular.isArray(localLanguage[key]) || angular.isObject(localLanguage[key])) {
                    if (!localLanguage[key]) {
                        localLanguage[key] = angular.isArray(localLanguage[key]) ? [] : {};
                    }
                    TW.Utils.OverrideLocalizationValues(localLanguage[key], localLanguage[key]);
                } else {
                    localLanguage[key] = TW.Utils.LocalizedString(localLanguage[key]);
                }
            });
        },
        WebServiceResponse: function(data) {
            var parsedData;

            if (data.hasOwnProperty("d")) {
                try {
                    parsedData = JSON.parse(data.d);
                } catch (e) {
                    parsedData = data.d;
                }
            } else {
                parsedData = data;
            }

            return parsedData;
        },
        parseErrors: function(model, data) {
            if (!(typeof data === "object")) {
                return data;
            }
            if (data == null) {
                return "";
            }

            var parsedData;
            if (data.hasOwnProperty("d")) {
                try {
                    parsedData = JSON.parse(data.d);
                } catch (e) {
                    parsedData = data.d;
                }
            } else {
                parsedData = data;
            }

            if (parsedData.hasOwnProperty("Message")) {
                return TW.Utils.LocalizedString(parsedData.Message);
            }

            if (parsedData.hasOwnProperty("SystemError")) {
                return parsedData.SystemError;
            }

            // ReSharper disable once QualifiedExpressionMaybeNull
            if (!parsedData.hasOwnProperty("Errors")) {
                return parsedData;
            }
            var result = "";
            $.each(parsedData.Errors, function(index, item) {

                var lookupItem = {
                    Key: "",
                    Value: ""
                };

                if (item.hasOwnProperty("Key")) {

                    lookupItem.Key = item.Key;
                }

                if (item.hasOwnProperty("Name")) {

                    lookupItem.Key = item.Name;
                }

                if (item.hasOwnProperty("Value")) {
                    lookupItem.Value = item.Value;
                }

                if (item.hasOwnProperty("Message")) {
                    lookupItem.Value = TW.Utils.LocalizedString(item.Message);
                }


                if (!model.hasOwnProperty("Errors")) {
                    model.Errors = {};
                };

                console.log(lookupItem.Key);

                if (model.hasOwnProperty(lookupItem.Key)) {
                    model.Errors[lookupItem.Key] = lookupItem.Value;
                } else {

                    //proveri dali ima tocka vo klucot
                    var arr = lookupItem.Key.split(".");
                    if (arr.length > 0) {
                        if (model.hasOwnProperty(arr[0])) {
                            var inItem = model[arr[0]];
                            if (!inItem.hasOwnProperty("Errors")) {
                                inItem.Errors = {};
                            }
                            if (inItem.hasOwnProperty(arr[1])) {
                                inItem.Errors[arr[1]] = lookupItem.Value;
                            }
                        }
                    }
                }


                var itemResult = "<li>";
                if (lookupItem.Key) {
                    itemResult += TW.Utils.LocalizedString(lookupItem.Key.toUpperCase()) + " : ";
                }
                itemResult += TW.Utils.LocalizedString(lookupItem.Value);
                itemResult += "</li>";
                result += itemResult;

            });
            return "<ul>" + result + "</ul>";
        },
        clearErrors: function(model) {
            if (model.hasOwnProperty("Errors")) {
                model.Errors = {};
            }

            for (var propertyName in model) {
                var inner = model[propertyName];
                if (!!inner && inner.hasOwnProperty("Errors")) {
                    inner.Errors = {};
                }
            }

        },
        convertJSONDate: function(model) {

            for (var propertyName in model) {
                var inner = model[propertyName];

                if (typeof inner === "string" || inner instanceof String) {
                    if (inner.indexOf("/Date") !== -1) {
                        model[propertyName] = new Date(parseInt(inner.substr(6)));;
                    }
                }
                else if ((typeof inner === "object" || inner instanceof Object) && inner) {
                    this.convertJSONDate(inner);
                }
            }
        }
    },
    initialize: function () {

        String.format = function () {
            var s = arguments[0];
            for (var i = 0; i < arguments.length - 1; i++) {
                var reg = new RegExp("\\{" + i + "\\}", "gm");
                s = s.replace(reg, arguments[i + 1]);
            }
            return s;
        };
    }
};

Array.prototype.move = function (old_index, new_index) {
    if (new_index >= this.length) {
        var k = new_index - this.length;
        while ((k--) + 1) {
            this.push(undefined);
        }
    }
    this.splice(new_index, 0, this.splice(old_index, 1)[0]);
    return this; // for testing purposes
};

TW.initialize();