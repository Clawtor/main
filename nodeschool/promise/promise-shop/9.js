var Q = require('q')
  , def1 = Q.defer()
  , def2 = Q.defer();

Q.all([def1.promise, def2.promise])
.spread(console.log)

setTimeout(function () {
  def1.resolve("PROMISES");
  def2.resolve("FTW");
}, 200);