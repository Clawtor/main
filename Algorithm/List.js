function List(array){
	this.head = null;
	this.end = null;
	
	if(array != null && typeof array === "array" && array.length > 0){
		this.head = new Node(array[0]);
		var last = head;
		for(var i=1;i<array.length;i++){
			last.setNext(new Node(array[i]));
		}
	}
}

List.prototype.traverse = function(fn){
	var node = this.head;
	var array = [];
	while(node != null){
		array.push(node.value);
		node = node.next;
	}
	return fn(array);
}

List.prototype.getTail = function(){
	var node = this.head;
	if(this.head == null){
		return null;
	}
	while(node.next != null){
		node = node.next;
	}
	return node;
}

List.prototype.Add = function(object){
	//	Add to end.
	if(this.head == null){
		this.head = new Node(object);
	}else{
		var tail = this.getTail();
		tail.next = new Node(object);
	}
}

List.prototype.AddAt = function(object, index){
	//	traverse to index 
	//	add at 0 -> find current node at 0 and make node.next node[0]
	//	add at 3 -> find node at 3 then node[2]->newNode->node[3]
	//	iterate till count = index and store previous and next.
	var count = 0;
	var last = null;
	var next = this.head;
	if(node == null){
		return;
	}
	if(index == 0){
		var temp = this.head;
		this.head = new Node(object);
		this.head.next = temp;
		return;
	}
	node = this.head;
	while(node.next != null && count < index){
		last = node;
		node = node.next;
		count++;
	}
	var node =  new Node(object);
	last.next = node;
	node.next = next;
}

List.prototype.Remove = function(){
	if(this.head == null){
		return;
	}else{
		var node = this.head;
		var last = null;
		while(node.next != null){
			last = node;
			node = node.next;
		}
		last.next = null;
	}
}

List.prototype.RemoveAt = function(index){
	
}

function Node(object){
	this.value = object;
	this.next = null;
}

Node.prototype.setNext = function(next){
	this.next = next;
}