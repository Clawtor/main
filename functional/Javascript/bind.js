<html>
	<body>
		<script>
		var user = {
			data :[
				{name: "T.Woods", age:37},
				{name: "P.Mickelson", age:43}
			],
			f: function(){
				var randomNum = ((Math.random () * 2 | 0) + 1) - 1; // random number between 0 and 1​
		​
				// This line is adding a random person from the data array to the text field​
				console.log(this.data[randomNum].name + " " + this.data[randomNum].age); 
			}
		}

		user.f();
		</script>
	</body>
</html>