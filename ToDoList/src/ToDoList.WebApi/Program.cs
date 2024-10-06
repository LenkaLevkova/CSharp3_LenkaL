var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Ahoj!");
app.MapGet("/nazdarSvete", () => "Nazdar svete!");

app.Run();
