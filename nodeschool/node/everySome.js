function checkUsersValid(goodUsers){
	return function allUsersValid(submittedUsers){
		submittedUsers.every(function(user){
			goodUsers.contains(user);
		})
	}
}

module.export = checkUsersValid;