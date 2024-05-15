var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ADD NEW ENDPOINTS HERE

// Endpoint: Hello World
// Description: Adds a new Hello World endpoint at / that returns a Hello World! string.
app.MapGet("/", () => "Hello World!");

// Endpoint: DaysBetweenDates
// Description: Calculates the number of days between two dates.
// Parameters:
// - date1: The first date.
// - date2: The second date.
app.MapGet("/DaysBetweenDates", (DateTime date1, DateTime date2) => (date2 - date1).TotalDays);

// Endpoint: validatephonenumber
// Description: Validates a phone number with Spanish format. Spanish format must begin with +34 and has 9 digits, not letters or special characters.
// Parameters:
// - phoneNumber: The phone number to validate.
// Returns: True if the phone number is valid, false otherwise.
app.MapGet("/validatephonenumber", (string phoneNumber) => {
    var regex = new Regex(@"^34(?:6[0-9]|7[1-9])[0-9]{7}$");
    return regex.IsMatch(phoneNumber);
});


// Endpoint: validatespanishdni
// Description: Validates a Spanish DNI (Documento Nacional de Identidad).
// Parameters:
// - dni: The DNI to validate.
// Returns: "valid" if the DNI is valid, "invalid" otherwise.
app.MapGet("/validatespanishdni", (string dni) => {
    var regex = new Regex(@"^\d{8}[A-Z]$");
    if (!regex.IsMatch(dni))
    {
        return "invalid";
    }

    var letters = "TRWAGMYFPDXBNJZSQVHLCKE";
    var number = int.Parse(dni.Substring(0, 8));
    var letter = dni.Substring(8, 1);
    var validLetter = letters[number % 23].ToString();
    return letter == validLetter ? "valid" : "invalid";
});


// Endpoint: returncolorcode
// Description: Returns the color code (hex) for a given color.
// Parameters:
// - color: The color to retrieve the code for.
// Returns: The color code (hex) if found, null otherwise.
app.MapGet("/returncolorcode", (string color) => {
    var colors = new List<ColorX>
    {
        new ColorX { color = "red", code = new Code { code = new Hex { hex = "#FF0000" } } },
        new ColorX { color = "green", code = new Code { code = new Hex { hex = "#00FF00" } } },
        new ColorX { color = "blue", code = new Code { code = new Hex { hex = "#0000FF" } } }
    };

    var colorX = colors.FirstOrDefault(c => c.color == color);
    return colorX?.code.code.hex;
});


// Endpoint: tellmeajoke
// Description: Makes a call to the joke API and returns a random joke.
// Returns: A random joke.
app.MapGet("/tellmeajoke", async () => {
    var client = new HttpClient();
    var response = await client.GetAsync("https://official-joke-api.appspot.com/jokes/programming/random");
    var jokes = JsonSerializer.Deserialize<Joke[]>(await response.Content.ReadAsStringAsync());
    return jokes[0].setup + " " + jokes[0].punchline;
});

// Endpoint: moviesbydirector
// Description: Makes a call to the movie API and returns a list of movies by a specific director.
// Parameters:
// - director: The director's name.
// Returns: A list of movies by the specified director.
// Note: This endpoint requires an API key from https://www.omdbapi.com/apikey.aspx.
app.MapGet("/moviesbydirector", async (string director) => {
    var client = new HttpClient();
    var response = await client.GetAsync($"http://www.omdbapi.com/?apikey=e0dd69c8&s={director}");
    var movies = JsonSerializer.Deserialize<Movies>(await response.Content.ReadAsStringAsync());
    return movies.Search;
});

// Endpoint: parseurl
// Description: Parses a URL and returns its components.
// Parameters:
// - someurl: The URL to parse.
// Returns: An object containing the parsed components of the URL.
app.MapGet("/parseurl", (string someurl) => {
    var uri = new Uri(someurl);
    return new
    {
        uri.Scheme,
        uri.Host,
        uri.Port,
        uri.AbsolutePath,
        uri.Query,
        uri.Fragment
    };
});

app.Run();

// Needed to be able to access this type from the MinimalAPI.Tests project.
public partial class Program
{ }

public class Movie
{
    public string Title { get; set; }
    public string Year { get; set; }
    public string imdbID { get; set; }
    public string Type { get; set; }
    public string Poster { get; set; }
}
public class Movies
{
    public List<Movie> Search { get; set; }
}

public class Joke
{
    public string setup { get; set; }
    public string punchline { get; set; }
}

public class ColorX
{
    public string color { get; set; }
    public Code code { get; set; }
}
public class Hex
{
    public string hex { get; set; }
}

public class Code
{
    public Hex code { get; set; }
}