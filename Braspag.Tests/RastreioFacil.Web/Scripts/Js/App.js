angular.module("app", ['ngCookies', 'ngBootbox', 'chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap', 'ui.mask', 'ngPassword', 'ng', 'ngRoute', 'ngMap'])
    .constant("MyConst", {
        "url": "/V1/api/veiculo/listar/",
        "path": '/V1/api/',
        "tempo": 60000,
        "myFavorite": "",
        "expireDate": "",
        "id": "",
        "hash": ""
    })
    .config(function (cfpLoadingBarProvider) {
        cfpLoadingBarProvider.includeSpinner = true;
    })

