(function () {
  'use strict';

  angular
    .module('ganymede')
    .controller('BaseStationController', BaseStationController);

  BaseStationController.$inject = ['$scope', 'baseStationResolve'];

  function BaseStationController($scope, baseStation) {
    var vm = this;

    vm.baseStation = baseStation;
  }
}());
