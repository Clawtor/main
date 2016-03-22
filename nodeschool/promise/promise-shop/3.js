var Q = require('q');
var def = Q.defer();
def.promise.then(console.log, console.log);
def.resolve("I FIRED");
def.resolve("I DID NOT FIRE");