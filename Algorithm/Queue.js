function Queue(){
	this.head = null;
	this.end = null;
}

Queue.prototype.Enqueue = function(value){
	//	Find end.
	var temp = this.head;
	this.head = new Node(value);
	this.head.next = temp;
}

Queue.prototype.Dequeue = function(){
	var node = this.head;
	var prev = null;
	if(node == null){
		return null;
	}
	if(node.next == null){
		var temp = this.head;
		this.head = null;
		return temp;
	}
	while(node.next != null){
		prev = node;
		node = node.next;
	}
	prev.next = null;
	return node;
}

function Node(value){
	this.value = value;
	this.next = null;
}