using MiddleApi.Models;
using MiddleApi.Services;
using MiddleApi.Services.Configurations;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers(options =>
    {
        // options.Filters.Add<ExceptionFilter>();
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Db Context
    builder.Services.AddDbContext<ApplicationDbContext>();

    // My Configurations
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
    builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

    // My Services
    builder.Services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
    builder.Services.AddTransient<IMailService, GmailService>();
}
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
// app.UseExceptionHandler("/error");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();