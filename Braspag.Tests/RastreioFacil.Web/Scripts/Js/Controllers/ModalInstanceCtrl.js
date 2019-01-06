angular.module('app').controller('ModalInstanceCtrl', function ($scope, $http, cfpLoadingBar, dialogService, $uibModalInstance, modalFactory, MyConst) {

    $scope.ok = function () {

        if ($scope.envioSenhaCPF == undefined) {
            return false;
        }

        cfpLoadingBar.start();

        $uibModalInstance.dismiss('cancel');

        var config = {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=UTF-8";'
            }
        };

        $http.post(MyConst.path + 'cliente/EnviarSenha/' + $scope.envioSenhaCPF, config).success(function (data, status, headers, config) {           
            cfpLoadingBar.complete();
           
            dialogService.alert(data);
        })
        .error(function (data, status, header, config) {
            cfpLoadingBar.complete();
            dialogService.alert(data.ExceptionMessage);
        });
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});