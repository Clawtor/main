//	create heap
function BinaryHeap = function(){
	this.root = null;
}

BinaryHeap.prototype.Insert = function(value){
	if(this.root == null){
		this.root = new Node(value);
		return;
	}
	
}

function Node(value){
	this.left = null;
	this.right = null;
}