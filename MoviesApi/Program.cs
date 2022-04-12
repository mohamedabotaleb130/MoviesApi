using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoviesApi.Models;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Defaultconnection")

    ));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//التعامل نع api خارجي ليك

builder.Services.AddCors();

builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new OpenApiInfo {
    Version = "v1",
    Title = "Test APi",
    Description = "My first api ",
    TermsOfService = new Uri("https://www.googel.com "),
    Contact = new OpenApiContact
    {
        Name = "abotaleb",
        Email = "mohemdabotaleb130@gmail.com",
        Url = new Uri("https://www.googel.com ")

    },
    License = new OpenApiLicense
    {
        Name = "My License",
        Url = new Uri("https://www.googel.com ")
    }


});
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
});
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
