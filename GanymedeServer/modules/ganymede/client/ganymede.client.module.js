(function (app) {
  'use strict';

  app.registerModule('ganymede', []);
  app.registerModule('ganymede.services');
  app.registerModule('ganymede.routes', ['ui.router', 'ganymede.services']);
}(ApplicationConfiguration));
