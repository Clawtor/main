var Q = require('q');

var json = process.argv[2];


function parsePromised(json){
	var def = Q.defer(); 
	var result;
	try{
		result = JSON.parse(json);
	}catch(e){
		def.reject(e);
	}
	def.resolve(result);
	return def.promise;
}

parsePromised(process.argv[2]).then(null, console.log);