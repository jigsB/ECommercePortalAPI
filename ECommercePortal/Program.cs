using ECommercePortal.API.GraphQL.Mutations;
using ECommercePortal.API.GraphQL.Queries;
using ECommercePortal.Application;
using ECommercePortal.Infrastructure;
using ECommercePortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
var policyName = builder.Configuration["CorsOrigins:PolicyName"];
var origins = builder.Configuration.GetSection("CorsOrigins:Origins").Get<string[]>();

// ASP.NET authorization
builder.Services.AddAuthorization();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

#region Database
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

#endregion

// Configure CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                      builder =>
                      {
                          builder.WithOrigins(origins)
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngular",
//        policy =>
//        {
//            policy
//                .WithOrigins("http://localhost:4200")
//                .AllowAnyHeader()
//                .AllowAnyMethod()
//                .AllowCredentials();
//        });
//});
#region GraphQL
builder.Services
    .AddGraphQLServer()
    .AddAuthorization().
     AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<UserMutation>()
    .AddTypeExtension<AuthMutation>()
    .AddFiltering()
    .AddSorting();
#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
}); builder.Services.AddHttpContextAccessor();

var app = builder.Build();

#region Middleware
app.UseHttpsRedirection();
app.UseCors("AllowAngular"); 
app.UseAuthorization();
//app.UseAuthentication();

app.MapGraphQL("/graphql");

app.Run();
#endregion
