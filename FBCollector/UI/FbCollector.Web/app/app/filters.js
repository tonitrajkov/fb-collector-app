"use strict";

fbcApp.filter("translate", [
        function () {
            return function (text) {
                return TW.Utils.LocalizedString(text);
            };
        }
])
.filter("formatJsonDate", [
    "$filter",
    function ($filter) {
        return function (value) {
            if (!!!value) {
                return "/";
            }
            var dt = new Date(parseInt(value.substr(6)));
            dt = $filter("date")(dt, "dd.MM.yyyy");
            return dt;
        };
    }
])
.filter("formatJsonDateTime", [
    "$filter",
    function ($filter) {
        return function (value) {
            if (!!!value) {
                return "/";
            }
            var dt = new Date(parseInt(value.substr(6)));
            dt = $filter("date")(dt, "dd.MM.yyyy HH:mm");
            return dt;
        };
    }
])
.filter("truncateTitle", function () {
    return function (value, length) {

        if (!value)
            return "-";

        if (length === 0)
            return value;

        if (value.length >= length)
            return value.substr(0, length) + "...";

        return value;
    };
})
.filter("weekDayFromJsonDateTime", [
    function () {
        return function (value) {
            if (!!!value) {
                return "/";
            }
            var days = ["SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY"];
            var dt = new Date(parseInt(value.substr(6)));

            return TW.Utils.LocalizedString(days[dt.getDay()]);
        };
    }
]);