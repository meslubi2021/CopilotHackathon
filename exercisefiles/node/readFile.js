// read the file "color.json" and print it on console
var fs = require('fs');
 
fs.readFile('colors.json', 'utf8', function(err, contents) {
    // contents is a json string
    // extract all the category field from colors.json file and print them on console
    var color = JSON.parse(contents);
    for(var i=0;i<color.length;i++){
        console.log(color[i].category);
    }

});