using ApiVersioning;
using ApiVersioning.BusinessLogic;
using Asp.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerSetParametersValues>());

builder.Services.AddApiVersioning(options =>
{
    //Se il chiamante non specifica una versione, usiamo quella di default
    options.AssumeDefaultVersionWhenUnspecified = true;
    //Questa è la versione di Default. 
    options.DefaultApiVersion = new ApiVersion(1, 0);
    //Definiamo le strategie per stabilire quale versione il chiamante vuole utilizzare.
    //Ne possiamo definire più di una, in tal modo il chiamante potrà usare quella che desidera
    options.ApiVersionReader = ApiVersionReader.Combine(
        //sono disponibili anche UrlPath e MediaType che per semplicità non utilizziamo
        new HeaderApiVersionReader("api-version"),
        new QueryStringApiVersionReader("api-version"));
    //Permettiamo alla api di aggiungere due header che servono per stabilire quali versioni sono supportate e quali deprecate
    //ma ancora in servizio. 
    options.ReportApiVersions = true;
    //Diamo informazioni su una versione rilasciata
    options.Policies
        .Sunset(1.0)
        .Effective(new DateTimeOffset(2023, 9, 1, 0, 0, 0, TimeSpan.Zero))
        .Link("https://localhost:7124/WhatsNewVersion21alfa")
        .Language("it")
        .Title("Titolo")
        .Type("text/html");
})
.AddApiExplorer();

builder.Services.AddScoped<IApiVersion2Business, ApiVersion2Business>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            foreach (var description in app.DescribeApiVersions())
            {
                //Creiamo più json, uno per ogni versione.
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName);
            }
        });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
