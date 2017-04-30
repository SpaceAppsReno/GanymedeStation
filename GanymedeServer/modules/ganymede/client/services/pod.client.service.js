(function () {
  'use strict';

  angular
    .module('ganymede.services')
    .factory('PodService', PodService);

    PodService.$inject = ['$resource', '$log'];

  function PodService($resource, $log) {
    var Pod = $resource('/api/:baseStationId/:podId', {
      baseStationId: '@baseStationId', id: '@_id'
    }, {
      update: {
        method: 'PUT'
      }
    });

    angular.extend(Pod.prototype, {
      createOrUpdate: function () {
        var pod = this;
        return createOrUpdate(pod);
      }
    });

    return Pod;

    function createOrUpdate(pod) {
      if (pod._id) {
        return pod.$update(onSuccess, onError);
      } else {
        return pod.$save(onSuccess, onError);
      }

      // Handle successful response
      function onSuccess(pod) {
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
