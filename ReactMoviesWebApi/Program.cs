using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using ReactMoviesWebApi;
using ReactMoviesWebApi.API_Behavior;
using ReactMoviesWebApi.Filters;
using ReactMoviesWebApi.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.OpenApi.Models;
using ReactMoviesWebApi.Repositories.IRepositories;
using ReactMoviesWebApi.Repositories;
using ReactMoviesWebApi.Profiles;

#region Add services to the container

var builder = WebApplication.CreateBuilder(args);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

#region DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.UseNetTopologySuite());
});
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton(provider => new MapperConfiguration(config =>
{
    var geometryFactory = provider.GetRequiredService<GeometryFactory>();
    config.AddProfile(new MovieTheatersProfile(geometryFactory));
    config.AddProfile(new ActorsProfile());
    config.AddProfile(new GenresProfile());
    config.AddProfile(new MoviesProfile());
    config.AddProfile(new UsersProfile());
}).CreateMapper());
builder.Services.AddSingleton(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));
#endregion

#region Dependancy Injection
builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IMovie_ActorsRepository, Movie_ActorsRepository>();
builder.Services.AddScoped<IMovie_GenresRepository, Movie_GenresRepository>();
builder.Services.AddScoped<IMovie_MovieTheatersRepository, Movie_MovieTheatersRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieTheaterRepository, MovieTheaterRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<ITokenHandler, ReactMoviesWebApi.Repositories.TokenHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region Azure File Storage
builder.Services.AddScoped<IFileStorageService, AzureStorageService>();
#endregion

#region Azure Insights
builder.Services.AddApplicationInsightsTelemetry(configuration["APPINSIGHTS_CONNECTIONSTRING"]);
#endregion

#region CORS
builder.Services.AddCors(options =>
{
    var frontendUrl = configuration.GetValue<string>("frontend_url");
    options.AddDefaultPolicy(policy =>
    {
        policy
        .WithOrigins(frontendUrl)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders(new string[] { "totalAmountOfRecords" });
    });
});
#endregion

#region Filters
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(MyExceptionFilter));
    options.Filters.Add(typeof(ParseBadRequest));
}).ConfigureApiBehaviorOptions(BadRequestBehavior.Parse);
#endregion

#region Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter a valid JWT bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] {} }
    });
});
#endregion

#region Identity, Authentication, Authorization
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "Admin"));
});

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        //ClockSkew = TimeSpan.Zero
    };
});
#endregion

#endregion

#region Configure the HTTP request pipeline

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion