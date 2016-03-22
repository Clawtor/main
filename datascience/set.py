class Set1:
	def __init__(self, values=None):
		s1 = Set()
		s2 = Set([1,2,3,4])
		
		self.dict = {}
		
		if values is not None:
			for value in values:
				self.add(value)
	def __repr__(self):
		return "Set: " + str(self.dict.keys())
		
	
	def add(self, value):
		self.dict[value] = True
	
	def contains(self, value):
		return value in self.dict
	
	def remove(self, value):
		del self.dict[value]
