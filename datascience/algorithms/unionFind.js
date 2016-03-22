function unionFind(n){
	this.ids = [];
	//	List of length n
	for(var i=0;i<n;i++){
		this.ids.push(i);
	}
}
unionFind.prototype.union = function(p,q){
	this.ids = this.ids.map(function(current){
		if(current == p){
			return q;
		}
		return current;
	})
}
unionFind.prototype.find = 
	function(p){
		return this.ids.reduce(function(prev, current){
			if(current == this.ids[p]){
				prev.push(current);
			}
			return prev;
		}.bind(this), []);
	}
//	Print all connected groups.
unionFind.prototype.display = 
function(){
	var groups = {};
	this.ids.forEach(function(element, i){
		if(!!groups[String(element)]){
			groups[String(element)].push(i);
		}else{
			groups[String(element)] = new Array().push(i);
		}
	});
	console.log(groups);
}
unionFind.prototype.count = 
function(){
	
}
	
	// UF.prototype.connected = function(p, q){
		
	// }
	// UF.prototype.find = function(p){
		
	// }
	// UF.prototype.count = function(){
		
	// }
	// return this;
// }
