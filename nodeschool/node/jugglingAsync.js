var http = require('http');

var url1 = process.argv[2];
var url2 = process.argv[3];
var url3 = process.argv[4];

var data1 = [];
var data2 = [];
var data3 = [];

var complete = 0;

http.get(url1, function(response){
	response.setEncoding();
	response.on("data", function(data){
		data1.push(data);
	})
	response.on("end", function(){latch()})
})

http.get(url2, function(response){
	response.setEncoding();
	response.on("data", function(data){
		data2.push(data);
	})
	response.on("end", function(){latch()})
})

http.get(url3, function(response){
	response.setEncoding();
	response.on("data", function(data){
		data3.push(data);
	})
	response.on("end", function(){latch()})
})

function latch(){
	complete++;
	if(complete === 3){
		console.log(data1.reduce(function(prev,current){
			return prev + current
		}))
		console.log(data2.reduce(function(prev,current){
			return prev + current
		}))
		console.log(data3.reduce(function(prev,current){
			return prev + current
		}))
	}
}