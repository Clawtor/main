function duckCount(){
	return [].reduce.call(arguments, function(a,b){
		if(Object.prototype.hasOwnProperty.call(b, 'quack')){
			return ++a;
		}
	},0)
}
module.exports = duckCount;