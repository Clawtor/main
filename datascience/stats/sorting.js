function insertionSort(data){
	var array = data;
	var j=0;
	for(var i=0;i< array.length;i++){
		j = i;
		while(j > 0 && array[j-1] > array[j]){
			swap(array, j, j-1);
			j--;
		}
	}
	return array;
}