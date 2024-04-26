package com.microsoft.hackathon.quarkus;

import io.quarkus.test.junit.QuarkusTest;
import io.restassured.RestAssured;
import org.junit.jupiter.api.Test;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.is;

@QuarkusTest
public class RestApiTests {

    @Test
    public void testHelloEndpoint() {
        given()
          .when().get("/hello?key=world")
          .then()
             .statusCode(200)
             .body(is("hello world"));
    }

    @Test
    public void testDiffDatesEndpoint() {
        given()
          .when().get("/diffdates?date1=01-01-2021&date2=01-02-2021")
          .then()
             .statusCode(200)
             .body(is("31"));
    }

    @Test
    public void testValidatePhoneEndpoint() {
        given()
          .when().get("/validatephone?phone=+34666666666")
          .then()
             .statusCode(200)
             .body(is("true"));
    }

    @Test
    public void testValidateDniEndpoint() {
        given()
          .when().get("/validatedni?dni=12345678Z")
          .then()
             .statusCode(200)
             .body(is("true"));
    }

    @Test
    public void testHexColorEndpoint() {
        given()
          .when().get("/hexcolor?name=red")
          .then()
             .statusCode(200)
             .body(is("#FF0000"));
    }

    @Test
    public void testChuckNorrisJokeEndpoint() {
        given()
          .when().get("/chucknorris")
          .then()
             .statusCode(200)
             .body(is(not(empty())));
    }
}
