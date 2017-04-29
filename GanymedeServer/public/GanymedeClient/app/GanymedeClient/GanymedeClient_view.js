'use strict';

angular.module('Ganymede.GanymedeClient', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/GanymedeClient', {
    templateUrl: 'GanymedeClient/GanymedeClient_view.html',
    controller: 'GanymedeClientCtrl',
    controllerAs: "ctrl"
  });
}])

.controller('GanymedeClientCtrl', ['$scope', function($scope) {
}]);