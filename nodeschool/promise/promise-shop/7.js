var Q = require('q');

Q.fcall(JSON.parse, process.argv[2]).then(null,console.log);
