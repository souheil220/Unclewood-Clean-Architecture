using UnclewoodCleanArchitectur.Presentation.Extention;
using UnclewoodCleanArchitecture.Application;
using UnclewoodCleanArchitecture.Infrastructure;
using UnclewoodCleanArchitecture.Infrastructure.Common.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName); // Use fully qualified names for schema IDs
});

var app = builder.Build();

app.AddInfrastructureMiddleware();
app.AddPresentationMiddleware();

app.Use(async (context, next) =>
{
    if (context.User?.Identity?.IsAuthenticated ?? false)
    {
        Console.WriteLine("User is authenticated");
        foreach (var claim in context.User.Claims)
        {
            Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
        }
    }
    else
    {  
        Console.WriteLine("User is not authenticated");
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        Console.WriteLine($"Token present: {!string.IsNullOrEmpty(token)}"); 
        Console.WriteLine(token); 
    }
    
    await next();
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();