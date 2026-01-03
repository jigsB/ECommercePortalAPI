using ECommercePortal.API.GraphQL.Mutations;
using ECommercePortal.API.GraphQL.Product;
using ECommercePortal.API.GraphQL.Queries;
using ECommercePortal.API.GraphQL.Types;
using ECommercePortal.Infrastructure.Persistence;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

#region Database
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
#endregion

#region Authentication (JWT)
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();
#endregion

#region GraphQL
builder.Services
    .AddGraphQLServer()
    .AddAuthorizationCore()
    .AddQueryType<ProductQueries>()
    .AddMutationType<ProductMutations>()
    .AddType<ProductType>()
    .AddQueryType<UserQuery>()
    .AddMutationType<UserMutation>()
    .AddType<UserType>()
    .AddFiltering()
    .AddSorting();
#endregion

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

#region Middleware
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL("/graphql");

app.Run();
#endregion
