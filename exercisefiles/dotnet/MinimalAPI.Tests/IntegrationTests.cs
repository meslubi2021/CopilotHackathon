namespace IntegrationTests;

public class IntegrationTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public IntegrationTests(TestWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsHelloWorld()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Hello World!", content);
    }

    [Fact]
    public async Task TestHelloWorldEndpoint()
    {
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Hello World!", content);
    }

    [Fact]
    public async Task TestDaysBetweenDatesEndpoint()
    {
        var date1 = new DateTime(2022, 1, 1);
        var date2 = new DateTime(2022, 1, 10);
        var response = await _client.GetAsync($"/DaysBetweenDates?date1={date1}&date2={date2}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("9", content);
    }

    [Theory]    
    [InlineData("34636789587", true)]
    [InlineData("3412345", false)]
    [InlineData("123456", false)]
    [InlineData("341234567", false)]
    public async Task ValidatePhoneNumberTest(string phoneNumber, bool expected)
    {
        // Arrange
        var request = $"/validatephonenumber?phoneNumber={phoneNumber}";

        // Act
        var response = await _client.GetAsync(request);
        response.EnsureSuccessStatusCode();
        var isValid = bool.Parse(await response.Content.ReadAsStringAsync());

        // Assert
        Assert.Equal(expected, isValid);
    }

    [Fact]
    public async Task TestValidateSpanishDNIEndpoint()
    {
        var dni = "82957475V";
        var response = await _client.GetAsync($"/validatespanishdni?dni={dni}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("valid", content);
    }

    [Theory]
    [InlineData("90280967X", "valid")]
    [InlineData("26454332P", "valid")]
    [InlineData("1234567Z", "invalid")]
    [InlineData("123456789Z", "invalid")]
    public async Task ValidateSpanishDniTest(string dni, string expected)
    {
        // Arrange
        var request = $"/validatespanishdni?dni={dni}";

        // Act
        var response = await _client.GetAsync(request);
        response.EnsureSuccessStatusCode();
        var isValid = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(expected, isValid);
    }

    [Fact]
    public async Task TestReturnColorCodeEndpoint()
    {
        var color = "red";
        var response = await _client.GetAsync($"/returncolorcode?color={color}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("#FF0000", content);
    }

    [Fact]
    public async Task TestTellMeAJokeEndpoint()
    {
        var response = await _client.GetAsync("/tellmeajoke");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);
    }

    [Fact]
    public async Task TestMoviesByDirectorEndpoint()
    {
        var director = "Christopher Nolan";
        var response = await _client.GetAsync($"/moviesbydirector?director={director}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);
    }

    [Fact]
    public async Task TestParseURLEndpoint()
    {
        var someurl = "https://www.example.com/path?param1=value1&param2=value2#fragment";
        var response = await _client.GetAsync($"/parseurl?someurl={someurl}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);
    }
}

