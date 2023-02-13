using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MissionReportingTool.Configuration;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Handlers;
using MissionReportingTool.Jobs;
using MissionReportingTool.Repositories;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services;
using MissionReportingTool.Services.Interfaces;
using Quartz;
using System.Text;

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
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<LandHandler>();
builder.Services.AddHttpClient<ICommandsService, CommandsService>();
builder.Services.AddHttpClient<LandingJob>();
builder.Services.Configure<JwtTokenConfiguration>(builder.Configuration.GetSection(nameof(JwtTokenConfiguration)));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtTokenConfiguration>>().Value);
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
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    var jwtTokenConfiguration = new JwtTokenConfiguration();
    builder.Configuration.GetSection("JwtTokenConfiguration").Bind(jwtTokenConfiguration);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtTokenConfiguration.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtTokenConfiguration.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenConfiguration.SigningKey))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
