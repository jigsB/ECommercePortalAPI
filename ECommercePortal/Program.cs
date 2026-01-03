using ECommercePortal.API.GraphQL.Mutations;
using ECommercePortal.API.GraphQL.Queries;
using ECommercePortal.Application;
using ECommercePortal.Infrastructure;
using ECommercePortal.Infrastructure.Persistence;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// ASP.NET authorization
builder.Services.AddAuthorization();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

#region Database
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

#endregion



#region GraphQL
builder.Services
    .AddGraphQLServer()
    .AddAuthorization().
     AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<UserMutation>()
    .AddFiltering()
    .AddSorting();
#endregion

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

#region Middleware
app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseAuthentication();

app.MapGraphQL("/graphql");

app.Run();
#endregion
