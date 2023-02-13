using MissionReportingTool.Exceptions;
using MissionReportingTool.Repositories;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services;
using MissionReportingTool.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<BaseHttpResponseExceptionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SundownContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMissionReportRepository, MissionReportRepository>();
builder.Services.AddScoped<IMissionReportService, MissionReportService>();
builder.Services.AddScoped<IMissionImageRepository, MissionImageRepository>();
builder.Services.AddScoped<IMissionImageService, MissionImageService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
