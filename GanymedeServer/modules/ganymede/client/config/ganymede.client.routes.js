(function () {
  'use strict';

  angular
    .module('ganymede.routes')
    .config(routeConfig);

  routeConfig.$inject = ['$stateProvider'];

  function routeConfig($stateProvider) {
    $stateProvider
      .state('ganymede', {
        abstract: true,
        url: '/ganymede',
        template: '<ui-view/>'
      })
      .state('ganymede', {
        url: '',
        templateUrl: '/modules/ganymede/client/views/ganymede.client.view.html',
        controller: 'GanymedeController',
        controllerAs: 'vm',
        data: {
          pageTitle: 'Ganymede'
        }
      });
  }

  getArticle.$inject = ['$stateParams', 'GanymedeService'];

  function getArticle($stateParams, GanymedeService) {
    return GanymedeService.get({
      articleId: $stateParams.articleId
    }).$promise;
  }
}());
