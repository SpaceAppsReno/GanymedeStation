(function () {
    'use strict';

    angular
        .module('ganymede')
        .controller('PodController', PodController);

    PodController.$inject = ['$scope', 'podResolve'];

    function PodController($scope, pod) {
        var vm = this;

        vm.pod = pod;
    }
}());
