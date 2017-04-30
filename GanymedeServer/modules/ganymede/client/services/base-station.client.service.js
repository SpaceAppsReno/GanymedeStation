(function () {
  'use strict';

  angular
    .module('ganymede.services')
    .factory('BaseStationService', BaseStationService);

    BaseStationService.$inject = ['$resource', '$log'];

  function BaseStationService($resource, $log) {
    var BaseStation = $resource('/api/:baseStationId', {
      id: '@_id'
    }, {
      update: {
        method: 'PUT'
      }
    });

    angular.extend(BaseStation.prototype, {
      createOrUpdate: function () {
        var baseStation = this;
        return createOrUpdate(baseStation);
      }
    });

    return BaseStation;

    function createOrUpdate(baseStation) {
      if (baseStation._id) {
        return baseStation.$update(onSuccess, onError);
      } else {
        return baseStation.$save(onSuccess, onError);
      }

      // Handle successful response
      function onSuccess(baseStation) {
        // Any required internal processing from inside the service, goes here.
      }

      // Handle error response
      function onError(errorResponse) {
        var error = errorResponse.data;
        // Handle error internally
        handleError(error);
      }
    }

    function handleError(error) {
      // Log error
      $log.error(error);
    }
  }
}());
