module.exports = function arrayMap(arr, fn){
	return arr.reduce(function(prev, current){
		prev.push(fn(current));
	}, []);
}