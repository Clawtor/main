var async = require('async');

var square = function(num, doneCallback){
	console.log(num*num);
	return doneCallback(null);
};

async.each([1,2,3,4], square, function(err){
	console.log("Finished");
})