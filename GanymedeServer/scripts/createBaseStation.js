'use strict';

var http = require("http");

var requestJSON = {
    name: 'Ben',
    flow: '9000',
    voltage: '3.3',
    valves: [
        "valve1",
        "valve2"
    ]
};

var options = {
    hostname: 'localhost',
    port: 3000,
    path: '/api/' + requestJSON.name,
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
// write data to request body
req.write(JSON.stringify(requestJSON));
req.end();