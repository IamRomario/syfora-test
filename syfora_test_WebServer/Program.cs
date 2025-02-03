using syfora_test_WebServer;
using syfora_test_WebServer.Exceptions;

var app = WebApplication.CreateBuilder(args)
                        .InitializeServices()
                        .Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandlerMiddleware();

}

app.MapControllers();

app.Run();
