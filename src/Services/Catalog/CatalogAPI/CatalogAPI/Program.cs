var builder = WebApplication.CreateBuilder(args);
// Add servies to the container.
builder.Services.AddCarter(); //Adding necessary services to DI of .net
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();
var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();
app.Run();
