function getShortMessages(mesgs){
	return mesgs.filter(function(msg){
		return msg.message.length < 50;
	}).map(function(msg){
		return msg.message;
	})
}

module.exports = getShortMessages;