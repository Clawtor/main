function vector_add(v,w){
	return v.map(function(value, index){
		return v[index] + w[index];
	})
}

function vector_subtract(v,w){
	return v.map(function(value, index){
		return v[index] - w[index];
	})
}

function vector_sum(vectors){
	result = vectors[0];
	vectors.forEach(function(value, index){
		if(index > 0){
			result = vector_add(result, value);
		} 
	})
	return result;
}

function scalar_multiply(c,v){
	return v.map(function(value){
		return value * c;
	})
}

function vector_mean(vectors){
	var n = vectors.length;
	return scalar_multiply(1/n, vector_sum(vectors));
}