var length = 8;
			//testSort(selectionSort);
			//testSort(insertionSort);
			//testSort(bubblesort);
			testFunction(merge_sort);
			
			
			 
			function testFunction(fn){
				console.log(sort);
				var before = new Date(); 
				var data = generateData(length);
				testSorted(data);
				testSorted(fn(data));
				dateDifference(before);
			}
			
			function testSorted(array){
				var last = array[0];
				for(var i=0;i<array.length;i++){
					if(array[i] < last){
						console.log("Not sorted.");
						return;
					}
				}
				console.log("Sorted.");
			}
			
			function selectionSort(data){
				var array = data;
				for(var i=0;i<array.length;i++){
					var smallest = i;
					for(var j=i;j<array.length;j++){
						if(array[j] < array[smallest]){
							smallest = j;
						}
					}
					swap(array,i, smallest);
				}
				return array;
			}
			
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
			
			function mergeSort(arr, p, q){
				//	split
				if(p-q == 1){
					return; 
				}
				q = Math.floor((p+r)/2);
				console.log("P: " + p);
				console.log("R: " + r);
				console.log("Q: " + q);
				mergeSort(arr, p, q);
				mergeSort(arr, q+1, r);
				merge(arr, p, q, r);
			}
			
			function merge(arr, p, q, r){
				var n1 = q-p+1;
				var n2 = r-q;
				var L = [];
				var R = [];
				for(var i=0;i<n1;i++){
					L[i] = A[p+i-1];
				}
			}
			function bubblesort(data){
				var array = data;
				do{
					var swapped = false;
					for(var i=1;i<array.length;i++){
						if(array[i-1] > array[i]){
							swapped = true;
							swap(array,i-1, i);
						}
					}
				}while(swapped);
				return array;
			}
			
			function dateDifference(before){
				console.log((new Date() - before)/1000 + " seconds.");
			}
			 
			function swap(array, a, b){
				var temp  = array[a];
				array[a] = array[b];
				array[b] = temp;
			} 