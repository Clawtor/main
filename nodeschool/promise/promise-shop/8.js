function throwMyGod(){
	throw new Error("OH NOES");
}

function iterate(arg){
	console.log(arg);
	return arg+1;
}

var Q = require('q');
Q.fcall(iterate, 1)
.then(iterate)
.then(iterate)
.then(iterate)
.then(iterate)
.then(throwMyGod)
.then(iterate)
.then(iterate)
.then(iterate)
.then(iterate)
.then(iterate)
.then(iterate).done(null, console.log);