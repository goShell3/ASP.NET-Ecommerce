// using Ecommerce.Application;
using Ecommerce.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services
        .AddInfrastructure();

    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
}
var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Run();
}



