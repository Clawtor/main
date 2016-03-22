var net = require('net');

var portNumber = process.argv[2];
var server = net.createServer(function(socket){
	var date = new Date();
	var month = date.getMonth() + 1;
	var day = date.getDate();
	socket.end(date.getFullYear() + "-" + 
				(month > 9 ? month : "0" + month) + "-" + 
				(day > 9 ? day : "0" + day) + " " +
				(date.getHours() > 9 ? date.getHours() : "0" + date.getHours()) + ":" +
				 date.getMinutes() + "\n");
	 
});
server.listen(portNumber);