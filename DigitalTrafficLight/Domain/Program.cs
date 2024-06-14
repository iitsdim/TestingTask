using Domain.Services.Sequence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<ISequenceService, SequenceService>();
}
var app = builder.Build();
{

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

