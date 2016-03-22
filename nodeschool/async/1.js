var http = require('http'),
    async = require('async');
	
async.waterfall([
	function(cb){
		var body = '';
		http.get('http://localhost:3131', function(res){
			res.on('data', function(chunk){
				body += chunk.toString();
			});
			res.on('end', function(){
				cb(null, body);
			});
		}).on('error', function(err){
			cb(err);
		})
	},
	function(body, cb){
		var port = JSON.parse(body).port;
		var body = '';
		http.get('http://localhost:" + port, function(res){
			res.on('data', function(chunk){
				body += chunk.toString();
			});
			res.on('end', function(){
				cb(null, body);
			});
		}).on('error', function(err){
			cb(err);
		})
	}
], 
	function(err, result){
		if(err) return console.error(err);
		console.log(result);
	})