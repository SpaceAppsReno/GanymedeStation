'use strict';

// Declare app level module which depends on views, and components
angular.module('Ganymede', [
  'ngRoute',
  'Ganymede.GanymedeClient'
]).
config(['$locationProvider', '$routeProvider', function($locationProvider, $routeProvider) {
  $locationProvider.hashPrefix('!');

  $routeProvider.otherwise({redirectTo: '/GanymedeClient'});
}]);
