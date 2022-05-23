using CRUD.WebAPI.AppServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddPersistence();

builder.AddAuthenticationJwt();

var app = builder.Build();

var environment = app.Environment;
app.UseExceptionHandling(environment)
    .UseSwaggerMiddleware()
    .UseAppCors();

app.MapControllers();

app.UseAuthentication();

app.UseAuthorization();

app.Run();
