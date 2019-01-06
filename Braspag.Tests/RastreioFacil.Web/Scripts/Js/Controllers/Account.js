
angular.module("app").controller("AccountController", function ($scope, $http, $cookies, $window, $timeout, cfpLoadingBar, dialogService, $log, $uibModal, $location, MyConst) {
  
    $scope.open = function (size, template) {
        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: template || 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            size: size
        });
    };

    $scope.redirect = function () {
        $location.path("/Monitoramento");
    };

    $scope.SendData = function () {

        cfpLoadingBar.start();
        $scope.isDisabled = true;

        // use $.param jQuery function to serialize data from JSON 
        var data = $.param({
            grant_type: "password",
            username: $scope.inputCpf,
            password: $scope.inputSenha
        });

        var config = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
            }
        };

        $http.post('/bearerToken', data, config).success(function (data, status, headers, config)
        {
            MyConst.expireDate = new Date();
            var a = new Date(MyConst.expireDate.getTime() + 1000 * 60 * 60 * 24 * 365);
            MyConst.expireDate.setMinutes(MyConst.expireDate.getSeconds() + (data.expires_in - 1));
            $cookies.put('myHash', data.hash, { 'expires': MyConst.expireDate });
            $cookies.put('myFavorite', data.token_type + " " + data.access_token, { 'expires': MyConst.expireDate });

            if (data.trocarSenha == "True") {
                $scope.isSaving = false;
                $scope.isDisabled = true;
                MyConst.myFavorite = $cookies.get("myFavorite");
                $cookies.remove("myFavorite");
                return;
            }
            $scope.isDisabled = false;
            cfpLoadingBar.complete();
            $location.path("/Monitoramento");
          
        })
        .error(function (data, status, header, config) {
            $scope.isDisabled = false;
            dialogService.alert(data.error_description);
            
        });
    };

    $scope.TrocarSenha = function () {

        $http.defaults.headers.common['Authorization'] = MyConst.myFavorite;
        var config = {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=UTF-8";',               
            }
        };
        $http.put(MyConst.path + 'cliente/TrocarSenha/' + $scope.inputNovaSenha + "/" + $scope.inputConfirmSenha, config)
            .success(function (data, status, headers, config) {
                $cookies.put('myFavorite', MyConst.myFavorite, { 'expires': MyConst.expireDate });
                dialogService.alertRedirect(data, "#/Monitoramento");
               
        })
        .error(function (data, status, header, config) {
            alert("login error");
        });
    };

    $scope.VerificaToken = function () {

        if ($cookies.get("myFavorite") != undefined) {

            $http.defaults.headers.common['Authorization'] = $cookies.get("myFavorite");
            var config = {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json; charset=UTF-8";',
                }
            };


            $http.get(MyConst.path + 'cliente/VerificaToken/', config)
            .success(function (data, status, headers, config) {
                if (data) {
                    $location.path("/Monitoramento");
                }

            })
            .error(function (data, status, header, config) {
                if (status == 401) {
                   
                } else {

                }
            });

        }
    }

  
    $scope.isSaving = true;

    $scope.isDisabled = false;

    // Função submit press enter

    $scope.onKeyPress = function (keyEvent)
    {
        if (keyEvent.which === 13) {

            if (!$scope.isSaving) {
                
                if (!$scope.user.$invalid)
                    $scope.TrocarSenha();

            } else {

                if (!$scope.user.$invalid)
                    $scope.SendData();

            }            
        }
            
    };

    $scope.VerificaToken();

   
    // $window.location.href = 'http://www.google.com';

});