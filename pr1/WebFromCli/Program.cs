using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/who", () => "˳�������� �����");

app.MapGet("/time", () => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

app.Run();
