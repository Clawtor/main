function drawingArea(width, height,drawingFunction){ 
	this.drawingFunction = drawingFunction;
	
	this.canvas = document.createElement('canvas');
	this.canvas.width = width;
	this.canvas.height = height;
	document.body.appendChild(this.canvas);
	this.ctx = this.canvas.getContext('2d');
} 
var width = height = 100; 

var lookup = {sin: {}, cos: {}}
var resolution = 100;

for(var i=0;i<Math.PI*2*resolution;i++){
	//lookup.sin[]
}

var drawingAreas = [];
// drawingAreas.push(new drawingArea(100,100, function(xindex,yindex,time){
	// var color = Math.floor(Math.abs(Math.sin(xindex*Math.PI*2 + time) * 255));	 
	// return color;
// }));

// drawingAreas.push(new drawingArea(100,100, function(xindex,yindex,time){
	// var color = Math.floor(Math.abs(Math.sin(yindex*Math.PI*2 + time) * 255));	 
	// return color;
// }));

// drawingAreas.push(new drawingArea(100,100, function(xindex,yindex,time){//
	// var color = Math.floor(
					// Math.abs(
						 // Math.sin(xindex*Math.PI*2 + time) + Math.sin(yindex*Math.PI*2 + time) 
					// ) * 255
				// );	 
	// return color;
// }))

// document.body.appendChild(document.createElement('br'));
// document.body.appendChild(document.createElement('br'));


// drawingAreas.push(new drawingArea(100,100, function(xindex,yindex,time){
	// var color = Math.floor( 
					// Math.abs(
						// ( (1-xindex) * Math.sin(time/2) + (1-yindex) * Math.sin(time/2)) * 255
					// )
				// );	 
	// return color;
// }));

// drawingAreas.push(new drawingArea(100,100, function(xindex,yindex,time){
	// var color = Math.floor( 
					// Math.abs(
						// ( xindex * Math.sin(time/2) + (1-yindex) * Math.sin(time/2)) * 255
					// )
				// );	 
	// return color;
// }));

// document.body.appendChild(document.createElement('br'));

// drawingAreas.push(new drawingArea(100,100, function(xindex,yindex,time){
	// var color = Math.floor( 
					// Math.abs(
						// ( (1-xindex) * Math.sin(time/2) + yindex * Math.sin(time/2)) * 255
					// )
				// );	 
	// return color;
// }));

// drawingAreas.push(new drawingArea(100,100, function(xindex,yindex,time){
	// var color = Math.floor(  
						// (xindex * Math.sin(time) + yindex * Math.sin(time)) * 255
				// );	 
	// return color;
// }));
 
document.body.appendChild(document.createElement('br'));

drawingAreas.push(new drawingArea(100,100, function(xindex,yindex,time){
	var color = Math.floor( 
					Math.abs(
						( 
							(Math.sin(xindex*Math.PI*2 + time) + Math.sin(yindex*Math.PI*2 + time))  + 
							( (1-xindex) * Math.sin(time/2) + yindex * Math.sin(time/2))
						) * 255
					)
				);	 
	return color;
}));
 
document.body.appendChild(document.createElement('br'));

drawingAreas.push(new drawingArea(100,100, function(xindex,yindex,time){
	var color = Math.floor( 
					Math.abs(
						( 
							Math.sin(time) * Math.sin(xindex * Math.PI*2)
						) * 255
					)
				);	 
	return color;
}));

drawingAreas.push(new drawingArea(100,100, function(xindex,yindex,time){
	var color = Math.floor( 
					Math.abs(
						( 
							Math.sqrt((xindex-0.5)*(xindex-0.5)+(yindex-0.5) * (yindex-0.5))
						) * time * 100
					)
				);	 
	return color;
}));
 
var time = 0;
function draw(){ 
	for(var i=0;i<width;i++){
		for(var j=0;j<height;j++){
			  
			var xindex = i/width;
			var yindex = j/height;
			
			//var color = Math.floor(Math.sin(xindex) * time * 255 ); 
			//Math.floor((xindex) * 255);	// Vertical static line
			//var color = Math.floor((xindex * Math.sin(time)) * 255);	//	Looks like a vertical wave coming in
			//var color = Math.floor((xindex * Math.cos(time)) * 255);	//	Same as above but different starting position?
			for(var d = 0;d<drawingAreas.length;d++){
				var color = drawingAreas[d].drawingFunction(xindex, yindex, time);
				
				drawingAreas[d].ctx.fillStyle = "rgba(" + color +"," + color +"," + color + ",255)";
				drawingAreas[d].ctx.fillRect(i,j,1,1);
			}
		}
	}
	time += 0.1 % (Math.PI);
}

window.setInterval(function(){
	draw();
}
, 200);