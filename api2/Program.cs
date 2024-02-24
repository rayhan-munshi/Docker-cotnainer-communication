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


app.MapGet("/weatherforecastlocal", async Task<string> () =>
{
    HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("http://host.docker.internal:32775/")
    };

    var response = await _httpClient.GetAsync("/weatherforecast");
    response.EnsureSuccessStatusCode();

    return await response.Content.ReadAsStringAsync();
})
.WithName("GetWeatherForecastLocal")
.WithOpenApi();


app.MapGet("/ger-remote-data", async Task<string> () =>
{
    HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
    };

    var response = await _httpClient.GetAsync("/posts");
    response.EnsureSuccessStatusCode();

    return await response.Content.ReadAsStringAsync();
})
.WithName("GetRemoteData")
.WithOpenApi();


app.Run();


