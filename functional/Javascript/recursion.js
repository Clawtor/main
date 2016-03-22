function reduce(arr, fn, initial){
	if(arr.length == 1){
		return fn(initial, arr[0]);
	}
	return fn(reduce(arr.slice(1), fn, initial), arr[0]); 
}

module.exports = reduce;