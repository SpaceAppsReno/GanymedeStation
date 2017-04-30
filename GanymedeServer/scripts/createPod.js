'use strict';

var http = require("http");
var async = require('async');

if(process.argv.length < 3) {
    console.log('Not enough arguments');
    process.exit(-1);
}

var podData = {
    name: 'Pod1',
    temperature: '16',
    moisture: '255',
    light: '50',
    voltage: '3.3'
};

var baseStationName = process.argv[2];
if(process.argv.length > 3){
    podData.name = process.argv[3];
}

var baseStation;
async.series([
    function(cb) {
        console.log('Querying ' + baseStationName);
        var resString = "";
        var req = http.request({
            hostname: 'localhost',
            port: 3000,
            path: '/api/' + baseStationName,
            method: 'GET'
        }, function(res) {
            res.on('data', function(chunk){
                resString += chunk;
            });
            res.on('end', function(){
                console.log('GET returned:\n' + resString);
                baseStation = JSON.parse(resString);
                cb();
            });
        });
        req.on('error', function(err){
            console.log('GET had an error: ' + err.message);
            cb (err);
        });
        req.end();
    }, function(cb) {
        console.log ('Creating Pod');
        var options = {
            hostname: 'localhost',
            port: 3000,
            path: '/api/' + baseStationName + '/' + podData.name,
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            }
        };
        var req = http.request(options, function(res) {
            console.log('Status: ' + res.statusCode);
            console.log('Headers: ' + JSON.stringify(res.headers));
            res.setEncoding('utf8');
            res.on('data', function (body) {
                console.log('Body: ' + body);
            });
            res.on('end', function () {
                cb ();
            });
        });
        req.on('error', function(e) {
            console.log('problem with request: ' + e.message);
            cb(e);
        });
        var json_string = JSON.stringify(podData);
        console.log ('Writing JSON to ' + options.path + '\n' + json_string + '\n');
        // write data to request body
        req.write(json_string);
        req.end();
    }
], function(err, cb){
    if(err) {
        console.log('Error running command: ' + err.message);
    } else {
        console.log('Success!');
    }
});

