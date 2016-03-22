var http = require('http'),
	fs = require('fs');
	
var port = process.argv[2];
var path = process.argv[3];
var server = http.createServer(function(request, response){
	var file = fs.createReadStream(path);
	file.pipe(response);
});

server.listen(port);