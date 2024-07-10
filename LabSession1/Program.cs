using LabSession1.Filters;
using LabSession1.Middlewares;
using LabSession1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(
    // opt =>
    // {
    //     opt.Filters.Add<LoggingActionFilter>();
    // }
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAppService, AppService>();
builder.Services.AddScoped<IObjectMapperService, ObjectMapperService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

// My custom middleware
// app.UseRequestLoggingMiddleware();

app.MapControllers();

app.Run();