angular.module("app").controller("MonitoramentoController", function ($scope, $cookies, $location, $interval, $http, dialogService, $timeout, NgMap, formatDateService, $uibModal, MyConst,$rootScope ) {

    if (typeof $cookies.get("myFavorite") === 'undefined') {
        $location.path("/");
    }

    Breakpoints();
    var Site = window.Site;
    Site.run();
    
    $scope.isDisabled = true;
    var geocoder = new google.maps.Geocoder();
    $scope.neighborhood = "";
    $scope.bairro = "";
    $scope.cidade = "";
    $scope.estado = "";
      
    var vm = this;
    vm.max = new Date();
    vm.positions = [];
    vm.Dados = [];
    vm.path = [];
    vm.path2 = [];
    vm.pathIcon = [];
    vm.pathIcon2 = [];
    vm.modalInstance;
    vm.ClearGetVeiculo = null;
    vm.ClearGetVeiculoDetalhes = null;
    vm.url = "";

    NgMap.getMap().then(function (map) {

        $timeout(function () {

        //console.log('map', map);

        vm.map = map;

        if (vm.positions.length > 0) {

            var myLatLng = new google.maps.LatLng(vm.positions[0].lat, vm.positions[0].lng);
            map.setCenter(myLatLng);

            map.setZoom((vm.positions.length > 1 ? 8 : (vm.positions.length == 0 ? 4 : 16)));

        }

        }, 2000);

    });

    $scope.GetVeiculos = function () {

        vm.positions = [];
        vm.path = [];
        vm.path2 = [];
        vm.pathIcon = [];
        vm.pathIcon2 = [];

        $interval.cancel(vm.ClearGetVeiculo);
        $interval.cancel(vm.ClearGetVeiculoDetalhes);

        $http.defaults.headers.common['Authorization'] = $cookies.get("myFavorite");
        var config = {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=UTF-8";',
            }
        };

        $http.get(MyConst.url + formatDateService.FormateDate(new Date('2016/09/16')), config)
            .success(function (data, status, headers, config) {

                vm.positions = [];
                vm.path = [];
                vm.path2 = [];
                vm.pathIcon = [];
                vm.pathIcon2 = [];

                for (i = 0; i < data.length; i++) {

                    if (data[i].DadosVeiculo.length > 0) {

                        vm.positions.push({ id: data[i].IMEI, modelo: data[i].ds_modelo, placa: data[i].ds_placa, lat: data[i].DadosVeiculo[0].latitude, lng: data[i].DadosVeiculo[0].longitude, data: formatDateService.FormateHour(data[i].DadosVeiculo[0].data), vel: data[i].DadosVeiculo[0].speed });
                    }
                }

                vm.Dados = vm.positions;

                if (vm.positions.length > 1) {
                    $("#veiculo").css('display', '');
                    $scope.isDisabled = true;
                    $(".bloqueio a").css("color", "#63B8FF").css("cursor", "not-allowed");
                    
                    vm.ClearGetVeiculo = $interval($scope.GetVeiculos, MyConst.tempo);
                } else {
                    $("#veiculo").css('display', 'none');
                    $scope.GetVeiculoDetalhe(vm.positions[0].id);
                }
           
            })
        .error(function (data, status, header, config) {
            if (status == 401) {
                $interval.cancel(vm.ClearGetVeiculo);
                dialogService.alertRedirect("Sua sessão expirou.", "/");
            } else {
                dialogService.alert(data.ExceptionMessage);
            }
        });       
    };

    $scope.GetVeiculoDetalhe = function (IMEI) {

        $interval.cancel(vm.ClearGetVeiculo);
        $interval.cancel(vm.ClearGetVeiculoDetalhes);
        $scope.isDisabled = false;
        MyConst.id = IMEI;
        $(".bloqueio a").css("color", "rgba(202, 208, 212, 0.9)").css("cursor", "pointer");;
       
        $http.defaults.headers.common['Authorization'] = $cookies.get("myFavorite");
        var config = {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=UTF-8";',
            }
        };

        $http.get(MyConst.url + '16-09-2016/' + IMEI, config)
            .success(function (data, status, headers, config) {

                vm.positions = [];
                vm.path = [];
                vm.pathIcon = [];
                vm.path2 = [];
                vm.pathIcon2 = [];

                if (data.DadosVeiculo.length > 0) {

                    vm.positions.push({ id: data.IMEI, modelo: data.ds_modelo, placa: data.ds_placa, lat: data.DadosVeiculo[0].latitude, lng: data.DadosVeiculo[0].longitude, data: formatDateService.FormateHour(data.DadosVeiculo[0].data), vel: data.DadosVeiculo[0].speed });

                    for (var i = 0; i < data.DadosVeiculo.length; i++) {
                  
                        vm.path2.push([data.DadosVeiculo[i].latitude, data.DadosVeiculo[i].longitude]);

                        vm.pathIcon2.push({ id: data.DadosVeiculo[i].id_dados.toString(), lat: data.DadosVeiculo[i].latitude, lng: data.DadosVeiculo[i].longitude, data: formatDateService.FormateHour(data.DadosVeiculo[i].data), modelo: data.ds_modelo, placa: data.ds_placa, vel: data.DadosVeiculo[i].speed })
                    }

                    var myLatLng = new google.maps.LatLng(vm.positions[0].lat, vm.positions[0].lng);

                    if (vm.map != undefined) {
                        vm.map.setCenter(myLatLng);
                        vm.map.setZoom((vm.positions.length > 1 ? 6 : (vm.positions.length == 0 ? 4 : 16)));
                    }                   
                }
                vm.ClearGetVeiculoDetalhes = $interval(function () { $scope.GetVeiculoDetalhe(IMEI) }, MyConst.tempo);
            })
            .error(function (data, status, header, config) {
                if (status == 401) {
                    $interval.cancel(vm.ClearGetVeiculoDetalhes);
                    dialogService.alertRedirect("Sua sessão expirou.", "/");
                } else {
                    dialogService.alert(data.ExceptionMessage);
                }
        });     
    };

    $scope.Funcional = function (str) {

        dialogService.confirn("Deseja realmente " + (str == "S" ? "bloquear" : "desbloquear") + (vm.Dados.length > 1 ? " o veículo de placa: " + vm.positions[0].placa + " ?" : " o veículo ?"), $scope, str);
    };

    $scope.start = function (caracter) {

        $http.defaults.headers.common['Authorization'] = $cookies.get("myFavorite");
        var config = {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=UTF-8";',
            }
        };
        $http.put(MyConst.path + 'veiculo/Alterar/' + caracter + "/" + vm.positions[0].id, config)
            .success(function (data, status, headers, config) {
                dialogService.alert(data);

            })
        .error(function (data, status, header, config) {
            dialogService.alert(data.ExceptionMessage);
        });


    }

    vm.showDetail = function (e, shop) {

        var latlng = new google.maps.LatLng(shop.lat, shop.lng);
        
        geocoder.geocode({ 'latLng': latlng, 'region': 'BR' }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {

                    $scope.neighborhood = results[0].address_components[1].long_name + ", " + results[0].address_components[0].long_name;
                    $scope.bairro = results[0].address_components[2].long_name;
                    $scope.cidade = results[0].address_components[3].long_name;
                    $scope.estado = results[0].address_components[5].short_name;
                    vm.pos = shop;

                    vm.map.showInfoWindow('foo-iw', shop.id);
                    
                }
        });      
    };

    vm.GetBuscarNome = function (e, shop) {

        var latlng = new google.maps.LatLng(shop.lat, shop.lng);

        geocoder.geocode({ 'latLng': latlng, 'region': 'BR' }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {

                $scope.neighborhood = results[0].address_components[1].long_name + ", " + results[0].address_components[0].long_name;
                $scope.bairro = results[0].address_components[2].long_name;
                $scope.cidade = results[0].address_components[3].long_name;
                $scope.estado = results[0].address_components[5].short_name;
                vm.pos = shop;

                vm.map.showInfoWindow('foo-iws', shop.id);
            }

        });     
    };

    vm.IndicadoresEnable = function () {

        if (vm.pathIcon.length == 0) {

            vm.pathIcon =  vm.pathIcon2;

        } else {

            for (var i = 0; i < vm.pathIcon.length; i++) {
                vm.map.markers[vm.pathIcon[i].id].setVisible(true);
            }
        }
       
        vm.map.shapes.foo.setMap(null);
    }

    vm.IndicadoresDisable = function () {

        $timeout(function () {

            for (var i = 0; i < vm.pathIcon.length; i++) {
                vm.map.markers[vm.pathIcon[i].id].setVisible(false);
            }

            vm.map.shapes.foo.setMap(vm.map);

        }, 3000);
    }

    $scope.Getpath = function () {

        if (!$scope.isDisabled) {

            if (vm.path.length == 0) {

                vm.path = vm.path2;
                vm.map.shapes.foo.setMap(vm.map);
            } else {

                if (vm.map.shapes.foo.getMap(vm.map) == null) {
                    vm.map.shapes.foo.setMap(vm.map);
                } else {
                    vm.map.shapes.foo.setMap(null);
                }

            }
        }
        
    }

    $scope.Logout = function () {

        $interval.cancel(vm.ClearGetVeiculo);
        $interval.cancel(vm.ClearGetVeiculoDetalhes);
        $cookies.remove("myFavorite");
        $location.path("/");
    }

    $scope.addClass = function (str) {
        $("#veiculo").addClass('open');
        $("#veiculo ul").each(function () {
            $(this).find('li').removeClass('active');
            if ($(this).find('li').attr('id') == str) {
                $(this).find('li').addClass('active');
            }
        });
    }

    $scope.open = function (size, template) {

        if (!$scope.isDisabled) {

            $rootScope.modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: template || 'myHistory.html',
                controller: function ($scope) {

                    var date2 = new Date();

                    $scope.cancel = function () {
                        $rootScope.modalInstance.dismiss('cancel');
                    };

                    $scope.today = function () {
                        $scope.dt = new Date();
                    };

                    $scope.today();

                    $scope.dateOptions = {                      
                        maxDate: new Date(date2.getFullYear(), date2.getMonth(), date2.getDate()),
                        minDate: new Date( (date2.getMonth() == 0 ?  (date2.getFullYear() -1) : date2.getFullYear()) , (date2.getMonth() == 0 ? 11 :(date2.getMonth() - 1)), date2.getDate()),                       
                    };

                    $scope.select = function (date) {

                        var souceDate;
                        var atualizar = false;

                        if ((date.getFullYear() == date2.getFullYear()) && (date.getMonth() == date2.getMonth()) && (date.getDate() == date2.getDate())) {
                            souceDate = "16-09-2016";//formatDateService.FormateDate(date2);
                            atualizar = true;
                        } else {
                            souceDate = formatDateService.FormateDate(date);
                            atualizar = false;
                        }

                        $interval.cancel(vm.ClearGetVeiculo);
                        $interval.cancel(vm.ClearGetVeiculoDetalhes);

                        $http.defaults.headers.common['Authorization'] = $cookies.get("myFavorite");
                        var config = {

                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json; charset=UTF-8";',
                                }
                        };

                        $http.get(MyConst.url + souceDate + '/' + MyConst.id, config)
                            .success(function (data, status, headers, config) {
                                vm.positions = [];
                                vm.path = [];
                                vm.pathIcon = [];
                                vm.path2 = [];
                                vm.pathIcon2 = [];

                                if (data.DadosVeiculo.length > 0) {

                                    vm.positions.push({ id: data.IMEI, modelo: data.ds_modelo, placa: data.ds_placa, lat: data.DadosVeiculo[0].latitude, lng: data.DadosVeiculo[0].longitude, data: formatDateService.FormateHour(data.DadosVeiculo[0].data), vel: data.DadosVeiculo[0].speed });

                                    for (var i = 0; i < data.DadosVeiculo.length; i++) {

                                        vm.path2.push([data.DadosVeiculo[i].latitude, data.DadosVeiculo[i].longitude]);

                                        vm.pathIcon2.push({ id: data.DadosVeiculo[i].id_dados.toString(), lat: data.DadosVeiculo[i].latitude, lng: data.DadosVeiculo[i].longitude, data: formatDateService.FormateHour(data.DadosVeiculo[i].data), modelo: data.ds_modelo, placa: data.ds_placa, vel: data.DadosVeiculo[i].speed })
                                    }

                                    var myLatLng = new google.maps.LatLng(vm.positions[0].lat, vm.positions[0].lng);

                                    if (vm.map != undefined) {
                                        vm.map.setCenter(myLatLng);
                                        vm.map.setZoom((vm.positions.length > 1 ? 6 : (vm.positions.length == 0 ? 4 : 16)));
                                    }

                                    $scope.Getpath();

                                } else {

                                    vm.map.setZoom(5);
                                }
                            })
                            .error(function (data, status, header, config) {
                                if (status == 401) {
                                    $interval.cancel(vm.ClearGetVeiculoDetalhes);
                                    dialogService.alertRedirect("Sua sessão expirou.", "/");
                                } else {
                                    dialogService.alert(data.ExceptionMessage);
                                }
                            });

                        $scope.Getpath = function () {

                            if (vm.path.length == 0) {

                                vm.path = vm.path2;
                                vm.map.shapes.foo.setMap(vm.map);
                            } else {

                                if (vm.map.shapes.foo.getMap(vm.map) == null) {
                                    vm.map.shapes.foo.setMap(vm.map);
                                } else {
                                    vm.map.shapes.foo.setMap(null);
                                }

                            }


                        }

                        $scope.cancel();

                        if (atualizar) {

                            vm.ClearGetVeiculoDetalhes = $interval(function () { $scope.select(date) }, MyConst.tempo);
                        }
                    };
                },
                size: size
            });
        }
    };

    $scope.openBoleto = function (size, template) {

        $rootScope.modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: template || 'myBoleto.html',   
            controller: function ($scope) {

                $scope.url = "../boleto/" + $cookies.get("myHash") + "/";

                $scope.cancel = function () {
                    $rootScope.modalInstance.dismiss('cancel');
                };
            },
            size: size,
            backdrop: 'static',
        });
    };

    $scope.GetVeiculos();

});