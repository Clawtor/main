function Spy(target, method){
	var spy = {count:0};
	var original = target[method];
	this.count = 1;
	target[method] = function(){
		spy.count++;
		return original.apply(this, arguments);
	}
	return spy;
}
module.exports = Spy;