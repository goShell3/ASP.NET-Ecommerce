using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

{
    builder.Services
        .AddInfrastructure();
        // .AddApplication();

    builder.Services.AddDbContext<EcommerceDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });


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

    app.MapControllers();
    app.Run();
}