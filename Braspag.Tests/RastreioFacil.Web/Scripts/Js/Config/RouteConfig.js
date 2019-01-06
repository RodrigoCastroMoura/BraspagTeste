angular.module("app").config(function ($routeProvider, $locationProvider) {

    $routeProvider
    .when("/default", {
        cache: false,
        templateUrl: "Views/Account/Login.html",
        controller: "AccountController"
    });

    $routeProvider
    .when("/Monitoramento", {
        cache: false,
        templateUrl: "Views/Monitoramento/Monitoramento.html",
    });

    $routeProvider
   .otherwise({  // This is when any route not matched => error  
       templateUrl: "Views/Account/Login.html",
       controller: "AccountController"
   })
});