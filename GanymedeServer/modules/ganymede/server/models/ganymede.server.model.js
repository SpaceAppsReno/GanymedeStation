'use strict';

/**
 * Module dependencies
 */
var mongoose = require('mongoose'),
    Schema = mongoose.Schema;

// Schema for Ganymede pod
var PodSchema = new Schema({
    name: String,
    baseStation: Schema.ObjectId,
    modified: Date,
    created: Date,
    voltage: Number,
    moisture: Number,
    temperature: Number,
    light: Number
});

// Schema for Ganymede water valve
var ValveSchema = new Schema({
    modified: Date,
    name: String,
    state: Boolean
});

// Schema for Ganymede base station
var BaseStationSchema = new Schema({
    created: Date,
    modified: Date,
    name: String,
    voltage: Number,
    flow: Number,
    pods: [String],
    valves: [String]
});

mongoose.model('Pod', PodSchema);
mongoose.model('Valve', ValveSchema);
mongoose.model('BaseStation', BaseStationSchema);

