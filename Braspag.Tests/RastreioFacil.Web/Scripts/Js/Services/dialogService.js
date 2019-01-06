angular.module("app").factory("dialogService", function ($ngBootbox, $location, $window) {

    var _alert = function (msg) {
        var options = {
            message: msg,
            title: 'Rastreio Fácil',
            buttons: {
                success: {
                    label: "Ok",
                    className: "btn-primary",                 
                }
            }
        };

        $ngBootbox.customDialog(options);
    };

    var _alertRedirect = function (msg,page) {
        var options = {
            message: msg,
            title: 'Rastreio Fácil',
            buttons: {
                success: {
                    label: "Ok",
                    className: "btn-primary",
                    callback: function () { $window.location.href = page; return; }
                }
            }
        };

        $ngBootbox.customDialog(options);
    };


    var _confirn = function (msg, scope, str) {
        var options = {
            message: msg,
            title: 'Rastreio Fácil',
            buttons: {
                success: {
                    label: "Ok",
                    className: "btn-primary",
                    callback: function () { scope.start(str) }
                },
                cancel: {
                    label: "Cancelar",
                    className: "btn-default",
             
                }
            }
        };

        $ngBootbox.customDialog(options);
    };


    return {

        alert: _alert,
        alertRedirect: _alertRedirect,
        confirn: _confirn
    };

});