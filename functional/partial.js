var slice = Array.prototype.slice;

function logger(namespace){
	return function(msg){
		console.log(namespace + ' ' + msg);
	}
}
module.exports = logger;