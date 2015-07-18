//	FIFO
function Stack(){
	this.head = null;
	this.end = null;
}

Stack.prototype.Push = function(value){
	var temp = this.head;
	this.head = new Node(value);
	this.head.next = temp;
}

Stack.prototype.Pop = function(){
	if(this.head == null){
		return null;
	}
	var node = this.head;
	if(node.next == null){
		this.head = null;
	}else{
		this.head = node.next;
	}
	return node;
}

function Node(value){
	this.value = value;
	this.next = null;
}