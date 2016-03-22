var Q = require('q');
var deferred = Q.defer();
deferred.promise.then(console.log);
deferred.resolve("SECOND");
console.log("FIRST");