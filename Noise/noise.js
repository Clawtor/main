var width = 200;
var height = 200;

function init(){
	console.log('Noise');

	this.canvas = document.createElement('canvas');
	this.canvas.width = width;
	this.canvas.height = height;
	document.body.appendChild(this.canvas);
	this.ctx = this.canvas.getContext('2d');
	randomNoise(this.ctx);
}

 function randomNoise(ctx){
	var points = [];
	var frequency = 4;
	var amplitude = height/2;
	// for(var i=1;i<frequency;i++){
		// points.push({x:width/frequency*i, y: Math.random()*amplitude})
	// }
	
	points.push({x:200,y:0}); 
	
	
	ctx.beginPath();
	ctx.strokeStyle = "#000";
	ctx.moveTo(0, 0);  
	ctx.stroke();
	for(var i=0;i<points.length;i++){
		ctx.lineTo(points[i].x, points[i].y); 
	}
	ctx.stroke();
	// for(var i=0;i<points.length;i++){
		// ctx.lineTo(points.x,points.y); 
		// ctx.stroke();
	// } 
 }
 
 function oneDPerlinNoise(){
	 
 }
 
function drawCircle(x,y,radius, color){
	ctx.fillStyle = color;
	ctx.beginPath()
	ctx.arc(x,y,radius, 0,2*Math.PI, false);
	ctx.fill();
	ctx.closePath();
}

function toColor(value){
	if(value >= 255){
		return "rgb(255,255,255)"
	}
	if(value <=0){
		return "rgb(0,0,0)";
	}
	var v = Math.round(value);
	return "rgb(" + v + "," + v + "," + v + ")";
}