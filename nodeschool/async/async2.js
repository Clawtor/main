var async = require('async');

var square = function(num, doneCallback){
	return doneCallback(null, num*num);
};

async.map([1,2,3,4], square, function(err, results){
	console.log("Finished!");
  console.log(results);
})