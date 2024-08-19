using AuthResourceEX.Bootstrapper;
using AuthResourceEX.Enums;
using AuthResourceEX.Services.Classes;
using AuthResourceEX.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AuthResourceEX.Models;
using AuthResourceEX.Data;
using AuthResourceEX.Repository.Interfaces;
using System;
using AuthResourceEX.Extensions;
using AuthResourceEX.Dtos;
using AuthResourceEX.Bootstrapper;
using AuthResourceEX.Dtos;
using AuthResourceEX.Repository.Interfaces;
using AuthResourceEX.Services.Interfaces;
using AuthResourceEX.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Bootstrapper

// Add scopes
builder.Services.AddScopes();
// Add JWT Authentication services
builder.Services.AddJwtAuthentication(builder.Configuration);
#endregion

// to access context 
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure authentication and authorization middleware is used
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Define API endpoints
app.MapPost("/login", (UserRequest loginRequest, IJwtTokenService tokenService) =>
{
    var users = Users.UsersList;
    var user = users.FirstOrDefault(x => x.UserName == loginRequest.UserName &&
                                         x.Password == loginRequest.Password);
    if (user is null)
    {
        return Results.Unauthorized();
    }

    // Generate JWT token
    var token = tokenService.GenerateToken(user.UserName, user.Role.ToString());
    return Results.Ok(new { Token = token });
});

// User Info Endpoint
// test with postman and do not forget to use headers for jwt bearer
app.MapGet("/userinfo", [Authorize] (HttpContext context) =>
{
    var user = context.User;
    var userName = user.Identity?.Name;
    var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

    return new
    {
        UserName = userName,
        Roles = roles
    };
});


#region ResourceEndpoints
app.MapGet("/resources", (IResourceRepository resourceRepo) =>
{
    var resource = resourceRepo.GetAll();
    if (resource is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(resource);
});
// get by id
app.MapGet("/resourceById/{id:int}", (int id, IResourceRepository resourceRepo) =>
{
    var resource = resourceRepo.GetById(id);
    if (resource is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(resource);
});
// UPDATE user

app.MapPost("/createResource", [Authorize] (ResourceDto resourceDto, IResourceRepository resourceRepo, HttpContext context) =>
{
    var user = context.User;
    var userName = user.Identity?.Name;
    var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
    var loggedInUser = new User()
    {
        UserName = userName,
        Role = role.GetRole()
    };

    (Resource? Resource, string Message) resource = resourceRepo.Create(loggedInUser, resourceDto);
    if (resource.Resource == null)
    {
        return Results.BadRequest(resource.Message);
    }
    return Results.Ok(resource.Resource);
});


// UPDATE user

app.MapPut("/updateResource/{id:int}", [Authorize] (int id, ResourceDto resourceDto, IResourceRepository resourceRepo, HttpContext context) =>
{
    var user = context.User;
    var userName = user.Identity?.Name;
    var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
    var loggedInUser = new User()
    {
        UserName = userName,
        Role = role.GetRole()
    };

    (Resource Resource, string Message) resource = resourceRepo.Update(loggedInUser, id, resourceDto);
    if (resource.Resource == null)
    {
        return Results.BadRequest(resource.Message);
    }
    return Results.Ok(resource.Resource);
});

// delete user

app.MapDelete("/deleteResource/{id:int}", [Authorize] (int id, IResourceRepository resourceRepo, HttpContext context) =>
{
    var user = context.User;
    var userName = user.Identity?.Name;
    var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
    var userModel = new User()
    {
        UserName = userName,
        Role = role.GetRole()
    };

    (bool IsSuccess, string Message) resource = resourceRepo.Delete(userModel, id);
    if (resource.IsSuccess is false)
    {
        return Results.BadRequest(resource.Message);
    }
    return Results.Ok(resource.Message);
});
#endregion

app.Run();