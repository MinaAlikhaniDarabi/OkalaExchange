using OkalaExchange.ApplicationService.Common.Middleware;
using OkalaExchange.ApplicationService.Common.Extensions;
using OkalaExchange.Api.Exchange;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

builder.Services.AddServices(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapExchangeEndpoint();


app.Run();
