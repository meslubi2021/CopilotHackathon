// write a nodejs server that will expose a method call "get" that will return the value of the key passed in the query string
// example: http://localhost:3000/get?key=hello
// if the key is not passed, return "key not passed"
// if the key is passed, return "hello" + key
// if the url has other methods, return "method not supported"
// when server is listening, log "server is listening on port 3000"

var http = require('http');
const { register } = require('module');
var url = require('url');

var server = http.createServer(function(req, res){
	var q = url.parse(req.url, true).query;
	var key = q.key;
	if (req.method === 'GET') {
		if (key) {
			res.write('hello ' + key);
		} else {
			res.write('key not passed');
		}
	} else {
		res.write('method not supported');
	}
	res.end();
});


// create a server with two endpoints
// endpoint 1: validatePhoneNumber
// endpoint 2: validateSpanishDNI
// both endpoints will receive a parameter called key
// validatePhoneNumber will return true if the key is a valid phone number
// validateSpanishDNI will return true if the key is a valid spanish DNI
server = http.createServer(function(req, res){
	var q = url.parse(req.url, true).query;
	var key = q.key;
	if (req.method === 'GET') {
		if (key) {
			//res.write('hello ' + key);
			var isPhoneNumber = /^\d{10}$/.test(key);
			var isSpanishDNI = /^[0-9XYZ][0-9]{7}[TRWAGMYFPDXBNJZSQVHLCKE]$/i.test(key);
			res.write(isPhoneNumber || isSpanishDNI);
		} else {
			res.write('key not passed');
		}
	} else {
		res.write('method not supported');
	}
	res.end();
});

// Create endpoints name DaysBetweenDates with two parameters date1 and date2
// return the number of dates between those two dates
server = http.createServer(function(req, res){
	var q = url.parse(req.url, true).query;
	var date1 = q.date1;
	var date2 = q.date2;
	if (req.method === 'GET') {
		if (date1 && date2) {
			//res.write('hello ' + key);
			var date1 = new Date(date1);
			var date2 = new Date(date2);
			var timeDiff = Math.abs(date2.getTime() - date1.getTime());
			var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)); 
			res.write(diffDays.toString());
		} else {
			res.write('key not passed');
		}
	} else {
		res.write('method not supported');
	}
	res.end();
});

server = http.createServer(function(req, res){
	var q = url.parse(req.url, true).query;
	var phoneNumber = q.phoneNumber;
	var dni = q.dni;
	
	if (req.method === 'GET') {
		if (phoneNumber) {
			// validate data is a phone number
			if (phoneNumber.match(/^\d{10}$/)) {
				res.write('true');
			} else {
				res.write('false');
			}
		} else if (dni) {
			// validate data is a spanish DNI
			if (dni.match(/^\d{8}[a-zA-Z]$/)) {
				res.write('true');
			} else {
				res.write('false');
			}
		} else {
			res.write('key not passed');
		}
	} else {
		res.write('method not supported');
	}
	res.end();
});


// create a server with two endpoints

server.listen(3000, function(){
	console.log('server is listening on port 3000');
});