'use strict';

/**
 * Module dependencies
 */
var path = require('path'),
    mongoose = require('mongoose'),
    Pod = mongoose.model('Pod'),
    Valve = mongoose.model('Valve'),
    BaseStation = mongoose.model('BaseStation');

/**
 * Show the current base station
 */
exports.readBaseStation = function (req, res) {
    // convert mongoose document to JSON
    if (!req.baseStation) {
        res.json({});
        return;
    }
    var baseStation = req.baseStation ? req.baseStation.toJSON() : {};
    res.json(baseStation);
};

/**
 * Update a base station
 */
exports.updateBaseStation = function (req, res) {
    var baseStation = req.baseStation;
    if(!baseStation) {
        baseStation = new BaseStation();
        baseStation.created = Date.now();
    }

    if(req.body.voltage) {
        baseStation.voltage = req.body.voltage;
    }
    if(req.body.flow) {
        baseStation.flow = req.body.flow;
    }
    if(req.body.name) {
        baseStation.name = req.body.name;
    }
    if(req.body.valves) {
        baseStation.valves = req.body.valves;
    }
    if(req.body.pods) {
        baseStation.pods = req.body.pods;
    }
    baseStation.modified = Date.now();

    baseStation.save(function (err) {
        if (err) {
            return res.status(422).send({
                message: err
            });
        } else {
            res.json(baseStation);
        }
    });
};

/**
 * Show the current base station
 */
exports.readPod = function (req, res) {
    // convert mongoose document to JSON
    var pod = req.pod ? req.pod.toJSON() : {};

    res.json(pod);
};

/**
 * Update a base station
 */
exports.updatePod = function (req, res) {
    var newPod = false;
    var pod = req.pod;
    if(!pod) {
        pod = new Pod();
        pod.created = Date.now();
        pod.baseStation = req.baseStation.name;
        newPod = true;
    }

    if(req.body.name) {
        pod.name = req.body.name;
    }
    if(req.body.voltage) {
        pod.voltage = req.body.voltage;
    }
    if(req.body.temperature) {
        pod.temperature = req.body.temperature;
    }
    if(req.body.moisture) {
        pod.moisture = req.body.moisture;
    }
    if(req.body.light) {
        pod.light = req.body.light;
    }
    req.body.modified = Date.now();

    pod.save(function (err) {
        if (err) {
            return res.status(422).send({
                message: err
            });
        } else {
            if (newPod) {
                req.baseStation.pods.push(pod.name);
                req.baseStation.save(function(err) {
                    if(err) {
                        return res.status(422).send({
                            message: err
                        });
                    } else {
                        res.json(pod);
                    }
                });
            } else {
                res.json(pod);
            }
        }
    });
};

/**
 * List of base stations
 */
exports.list = function (req, res) {
    BaseStation.find().select({name: 1, _id: 0}).exec(function (err, baseStations) {
        if (err) {
            return res.status(422).send({
                message: err
            });
        } else {
            res.json(baseStations);
        }
    });
};

/**
 * Base station middleware
 */
exports.baseByName = function (req, res, next, name) {
    BaseStation.findOne({'name': name}).exec(function(err, baseStation) {
        if (err) {
            return next(err);
        }
        req.baseStation = baseStation;
        next();
    });
};

/**
 * Pod middleware
 */
exports.podByName = function (req, res, next, name) {
    Pod.findOne({'name': name}).exec(function(err, pod) {
        if (err) {
            return next(err);
        }
        req.pod = pod;
        next();
    });
};