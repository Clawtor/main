//	Cellular automata.

function Canvas(width, height){
	this.width = width;
	this.height = height;
	this.gridSize = 10;
	
	this.canvas = document.createElement('canvas');
	this.canvas.width = width;
	this.canvas.height = height;
	document.body.appendChild(this.canvas);
	
	this.ctx = this.canvas.getContext('2d');
}

Canvas.prototype.drawGrid = function(startx,starty,width,height, cellSize){
	for(var y=0;y<this.height;y+=cellSize){
		this.drawLine(startx, y, width, y);
	}
	for(var x=0;x<this.width;x+=cellSize){
		this.drawLine(x, starty, x, height);
	}
}

Canvas.prototype.drawLine = function(x1,y1,x2,y2){
	this.ctx.beginPath();
	this.ctx.moveTo(x1,y1);
	this.ctx.lineTo(x2,y2);
	this.ctx.stroke();
}

function CellularLogic(){
	this.
	this.Cells = [];
	
	
}


function Rule(){
	//	input is cell state and neighbours.
	//	
}

Rule.prototype.computeNextState = function(state, neighbours){
	
}


