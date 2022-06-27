using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoviesApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options=>
options.UseSqlServer(connectionstring));
builder.Services.AddControllers();
builder.Services.AddCors(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(name: "v1", info: new OpenApiInfo
    {

        Title = "Test My Api",
        Version = "v1",
        Description ="my First api",
        TermsOfService =    new Uri ("https://www.google.com"),
        Contact =new OpenApiContact {
            Name ="Gomaa",
            Email ="mohamedgomaa211@yahoo.com",
            Url = new Uri("https://www.google.com"),


        },
        License = new OpenApiLicense { 
            Name = "my License",
            Url  = new Uri("https://www.google.com"),
        }


    });
    //options.AddSecurityDefinition(name: "Bearer", new OpenApiSecurityScheme
    //{
    //    Name ="Authoriztion",
    //    Type =SecuritySchemeType.ApiKey,
    //    Scheme= "Bearer",
    //    BearerFormat ="jwt",
    //    In=ParameterLocation.Header,
    //    Description ="enter  your jwt key"
    //});
    //options.AddSecurityRequirement(securityRequirement: new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference= new OpenApiReference
    //            {

    //                Type=ReferenceType.SecurityScheme,
    //                Id="Bearer"
    //            },
    //            Name="Bearer",
    //            In=ParameterLocation.Header

    //        },
    //        new List<string>()

    //    }

    //});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
