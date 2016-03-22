var fs = require('fs')

var path = process.argv[2];
fs.readFile(path, function(err,data){ 
	console.log(data.toString().split('\n').length-1)
	}
);
