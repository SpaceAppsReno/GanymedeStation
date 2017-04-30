'use strict';

/**
 * Module dependencies
 */
var ganymedePolicy = require('../policies/ganymede.server.policy'),
    ganymede = require('../controllers/ganymede.server.controller');

module.exports = function (app) {
    // Ganymede routes
    app.route('/api').all(ganymedePolicy.isAllowed)
        .get(ganymede.list);
    app.route('/api/:baseStationName').all(ganymedePolicy.isAllowed)
        .put(ganymede.updateBaseStation)
        .get(ganymede.readBaseStation);
    app.route('/api/:baseStationName/:podName').all(ganymedePolicy.isAllowed)
        .put(ganymede.updatePod)
        .get(ganymede.readPod);

    // Finish by binding the article middleware
    app.param('baseStationName', ganymede.baseByName);
    app.param('podName', ganymede.podByName);
};
