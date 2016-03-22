function generateData(length, min, max){
	var arr = [];
	for(var i=0;i<length;i++){
		arr.push(Math.round(Math.random() * (max-min)) + min)
	}
	return arr;
}

function mean(data){
	return data.reduce(function(prev, current){
		return prev + current;
	}, 0)/data.length;
}

function median(data){
	return data.sort()[Math.floor(data.length/2)]
}

function fastMedian_binning(data){
	if(data.length <= 5){
		return data.sort()[Math.floor(data.length/2)];
	}
	var groups = divideArray(data, 5);
	var medians = groups.reduce(function(prev,current){
		if(current.length > 1){
			prev.push(current.sort()[Math.floor(current.length/2)]);
			return prev;
		}else{
			prev.push(current[0]);
			return prev;
		}
	},[]);
	return fastMedian_binning(medians);
}

function speedTest(fn, data){
	var before = new Date();
	console.log("Results: " + fn(data));
	console.log((new Date() - before)/1000);
}

function divideArray(array, size){
	var groups = [];
	for(var i=0;i<array.length;i+=size){
		var arr = [];
		for(var j=0;j<size;j++){
			arr.push(array[i+j]);
		}
		groups.push(arr);
	}
	return groups;
}

function partition(data){
	var pivotIndex = data[Math.floor(Math.random() * data.length-1)];
	var pivot = data[pivotIndex];
	var head = [];
	var tail = [];
	data.forEach(function(d,i){
		if(i == pivotIndex){
			return;
		}
		if(d > pivot){
			tail.push(d);
		}else{
			head.push(d);
		}
	});
	return {head, tail};
}


function dispersion(data){
	max(data) - min(data);
}

function max(data){
	return data.reduce(function(prev, current){
		if(current > prev){
			return current;
		}else{
			return prev;
		}
	}, data[0])
}

function min(data){
	return data.reduce(function(prev, current){
		if(current < prev){
			return current;
		}else{
			return prev;
		}
	}, data[0])
}