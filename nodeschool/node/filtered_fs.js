var fs = require('fs')
var path = require('path')
module.exports = function(dirpath, extension, callback){
	fs.readdir(dirpath, function(err, list){
		if(err){
			callback(err,null);
		}
		var files = [];
		list.forEach(function(item){
			if(path.extname(item).slice(1) === extension){
				files.push(item);
			}
		})
		callback(null, files);
	});
}
