var mod = require('./filtered_fs');

var dirpath = process.argv[2];
var extension = process.argv[3];

mod(dirpath, extension, 
	function(err, data){
		if(err){
			throw err;
		}
		
		data.forEach(function(d){
			console.log(d);
		})
	}
)