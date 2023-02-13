using Microsoft.Extensions.Hosting;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Handlers;
using MissionReportingTool.Jobs;
using MissionReportingTool.Repositories;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services;
using MissionReportingTool.Services.Interfaces;
using Quartz;

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
builder.Services.AddScoped<ILandingRepository, LandingRepository>();
builder.Services.AddScoped<ICommandsService, CommandsService>();
builder.Services.AddSingleton<LandHandler>();
builder.Services.AddQuartzServer(options =>
{
    options.WaitForJobsToComplete = true;
});
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    q.UseSimpleTypeLoader();
    q.UseInMemoryStore();
    q.UseDefaultThreadPool(tp =>
    {
        tp.MaxConcurrency = 10;
    });

    q.ScheduleJob<LandingJob>(
        trigger => 
            trigger
                .WithIdentity("CronTrigger")
                .WithCronSchedule(builder.Configuration.GetValue<string>("LandingJobCron")),
        job =>
            job.WithIdentity("LandingJob")
    );
});
builder.Services.AddHttpClient<LandingJob>();

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
