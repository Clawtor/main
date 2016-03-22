var http = require('http');
var url = require('url');
var port = process.argv[2];

var server = http.createServer(function(request, response){
	var parsed = url.parse(request.url,true);
	var pathname = parsed.pathname;
	var query = parsed.query;
	if(pathname === "/api/parsetime"){
		console.log(query)
		if('iso' in query){
			response.writeHead(200, {'Content-Type':'application/json'});
			var now = new Date();
			var time = {hour: now.getHours(), minute: now.getMinutes(), second: now.getSeconds()}
			response.write(JSON.stringify(time));
		}
	}
})

server.listen(8000);