angular.module("app").factory("formatDateService", function ($ngBootbox, $location, $window) {

    String.prototype.padLeft = function (n, pad) {
        t = '';
        if (n > this.length) {
            for (ix = 0; ix < n - this.length; ix++) {
                t += pad;
            }
        }
        return t + this;
    }

    String.prototype.padRight = function (n, pad) {
        t = this;
        if (n > this.length) {
            for (ix = 0; ix < n - this.length; ix++) {
                t += pad;
            }
        }
        return t;

    }

    var _FormateHour = function (date) {

        var data = new Date(date);

        return data.getHours().toString().padLeft(2, '0') + ":" + data.getMinutes().toString().padLeft(2, '0') + ":" + data.getSeconds().toString().padLeft(2, '0');
    };

    var _FormateDate = function (date) {

        var dia = new String(date.getDate());

        var mes = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
     
        return dia.padLeft(2, '0') + "-" + mes[date.getMonth()] + "-" + date.getFullYear();
    };

    return {
        FormateHour: _FormateHour,
        FormateDate: _FormateDate,
    };

});
