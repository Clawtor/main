function dateDifference(before){
	console.log((new Date() - before)/1000 + " seconds.");
}

function generateData(length){
	var data = [];
	
	for(var i=0;i<length;i++){
		data.push(Math.random() * length);
	}
	return data;
}

function testFunctionSpeed(fn, params){
	var before = new Date(); 
	fn.apply(this, params);
	dateDifference(before);
}