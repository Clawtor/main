var http = require('http');

var url = process.argv[2];

var collect = [];
var first = false;
http.get(url, function(response){
	response.setEncoding();
	response.on("data", function(data){
		collect.push(data);
	})
	response.on("end", function(){
		console.log(collect.toString().length)
		console.log(collect.toString());
	})
})