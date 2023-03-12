using System;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

string urltoadd = "http://" + Environment.MachineName;

var restClient = new RestClient("http://load-balancer");
restClient.Post(new RestRequest("LoadBalancer", Method.Post)
    .AddJsonBody(new
    {
        Url = urltoadd
    }));

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCrossOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost/9001",
                    "http://load-balancer")
                .AllowAnyHeader()
                .AllowCredentials()
                .WithMethods("GET");
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();