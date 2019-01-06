angular.module("app").factory('modalFactory', function ($uibModal) {

    return {
        open: function (size, template, params) {
            return $uibModal.open({
                animation: true,
                templateUrl: template || 'myModalContent.html',
                controller: 'ModalResultInstanceCtrl',
                size: size,
                resolve: {
                    params: function () {
                        return params;
                    }
                }
            });
        }
    };
});