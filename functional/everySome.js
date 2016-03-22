function checkUsersValid(goodUsers){
	return function allUsersValid(submittedUsers){
		var goodIds = goodUsers.map(function(user){
			return user.id;
		})
		return submittedUsers.every(function(user){
			return goodIds.indexOf(user.id) > -1;
		})
	}
}

module.exports = checkUsersValid;