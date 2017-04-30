'use strict';

var http = require("http");

var baseStationData = {
    name: 'BaseStation1',
    flow: '9000',
    voltage: '3.3',
    valves: [
        "valve1",
        "valve2"
    ]
};

if(process.argv.length > 2) {
    baseStationData.name = process.argv[2];
}

var options = {
    hostname: 'localhost',
    port: 3000,
    path: '/api/' + baseStationData.name,
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
});
req.on('error', function(e) {
    console.log('problem with request: ' + e.message);
});

var json_string = JSON.stringify(baseStationData);
console.log ('Writing JSON to path ' + options.path + ': \n' + json_string + '\n');
// write data to request body
req.write(json_string);
req.end();