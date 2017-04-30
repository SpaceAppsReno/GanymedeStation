(function () {
    'use strict';

    angular
        .module('ganymede')
        .controller('GanymedeController', GanymedeController);

    GanymedeController.$inject = ['$scope', 'BaseStationService', 'PodService'];

    function GanymedeController($scope, BaseStationService, PodService) {
        var vm = this;
        $scope.newBaseStation = new BaseStationService();
        $scope.newPod = new PodService();

        function successCallback(res) {
            Notification.success({ message: '<i class="glyphicon glyphicon-ok"></i> Saved successfully!' });
        }

        function errorCallback(res) {
            Notification.error({ message: res.data.message, title: '<i class="glyphicon glyphicon-remove"></i> Error!' });
        }
        $scope.updateBaseStation = function() {
            $scope.newBaseStation.createOrUpdate()
                .then(successCallback)
                .error(errorCallback);
        };
        $scope.updatePod = function () {
            $scope.newPod.createOrUpdate()
        };

    }
}());
