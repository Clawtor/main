function stableMatching(){
	var size = 10;
	var men = [];
	var women = [];
	var temp = [];
	for(var i=0;i<size;i++){
		temp.push(i+1);
	}
	for(var i=0;i<size;i++){
		men.push({engaged: false, preference: shuffle(temp), proposed: null});
		women.push({engaged: false, preference: shuffle(temp)});
	}
	
	while(men.forEach(function(m){ if(!m.engaged || m.proposed < size-1){ return m}})){
		
	}
	
}

function algFinished(menArray){
	
}

function shuffle(array){
	for(var i=0;i<array.length){
		var j = Math.random() * array.length;
		var k = Math.random() * array.length;
		shuffle(array,j,k);
	}
}

function swap(array, i,j){
	var temp = array[i];
	array[i] = array[j];
	array[j] = temp;
}