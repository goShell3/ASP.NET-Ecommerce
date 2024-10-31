using Ecommerce.Application;
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

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
    app.Run();
}



