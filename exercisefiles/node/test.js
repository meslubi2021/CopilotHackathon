//write npm command line to install mocha
//npm install --global mocha

//command to run this test file
//mocha test.js

const assert = require('assert');
const http = require('http');
const server = require('./nodeserver');

describe('Node Server', 



() => {
    it('should return "key not passed" if key is not passed', (done) => {
        http
        .get('http://localhost:3000/get' , (res) => {
            let data = '';
            res.on('data', (chunk) => {
                data += chunk;
            });
            res.on('end', () => {
                assert.equal(data, 'key not passed');
                done();
            });
        });
    });
    //add test to check get when key is equal to world
    it('should return "key not passed 2" if key is not passed', (done) => {
        http
        .get('http://localhost:3000/get?key=world' , (res) => {
            let data = '';
            res.on('data', (chunk) => {
                data += chunk;
            });
            res.on('end', () => {
                assert.equal(data, 'hello world');
                done();
            });
        });
    });
    
    

    //add test to check validatephoneNumber

    //add test to validate y key parameter is a valid phone number
    it('validate key is a phone number', (done) => {
        http
        .get('http://localhost:3000/get?key=1234567890' , (res) => {
            let data = '';
            res.on('data', (chunk) => {
                data += chunk;
            });
            res.on('end', () => {
                // validate data is a phone number
                assert.equal(data, 'hello 1234567890');
                done();
            });
        });
    }

    //write test to validate validateSpanishDNI
   

    //write test for returnColorCode red should return code #FF0000


   //write test for daysBetweenDates



});
