var express = require('express');
var http = require('http');
var app = express();

app.use('/', express.static('public/GanymedeClient/app'));

var server = http.createServer(app);

server.listen(3000);

