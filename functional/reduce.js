function arrayMap(arr, fn){
	return arr.reduce(function(prev, current){
		prev.push(fn(current));
		return prev;
	}, []);
}

module.exports = arrayMap;