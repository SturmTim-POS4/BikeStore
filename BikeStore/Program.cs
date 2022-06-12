//----------------------------------------
// .Net Core WebApi project create script 
//           v6.2.0 from 2022-04-06
//   (C)Robert Grueneis/HTL Grieskirchen 
//----------------------------------------

using BikeStore.Services;
using GrueneisR.RestClientGenerator;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;

string corsKey = "_myCorsKey";
string swaggerVersion = "v1";
string swaggerTitle = "BikeStore";

var builder = WebApplication.CreateBuilder(args);

#region -------------------------------------------- ConfigureServices
builder.Services.AddControllers();
builder.Services.AddScoped<BikeStoreService>();
builder.Services
  .AddEndpointsApiExplorer()
  .AddSwaggerGen(x => x.SwaggerDoc(
    swaggerVersion,
    new OpenApiInfo { Title = swaggerTitle, Version = swaggerVersion }
  ))
  .AddCors(options => options.AddPolicy(
    corsKey,
    x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
  ))
  .AddRestClientGenerator(options => options
	  .SetFolder(@"C:\Users\timst\Desktop\Schule\4.Klasse\POS1\Sturm")
	  .SetFilename("_requests.http")
	  .SetAction($"swagger/{swaggerVersion}/swagger.json")
	  //.EnableLogging()
  );

string connectionString = builder.Configuration.GetConnectionString("BikeStore");
Console.WriteLine($"******** ConnectionString: {connectionString}");
builder.Services.AddDbContext<BikeStoreContext>(options => options.UseSqlServer(connectionString));
#endregion

var app = builder.Build();

#region -------------------------------------------- Middleware pipeline
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	Console.WriteLine("******** Swagger enabled: http://localhost:5000/swagger (to set as default route: see launchsettings.json)");
	app.UseSwagger();
	app.UseRestClientGenerator(); //Note: must be used after UseSwagger
	app.UseSwaggerUI(x => x.SwaggerEndpoint( $"/swagger/{swaggerVersion}/swagger.json", swaggerTitle));
}

app.UseCors(corsKey);

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

//app.UseExceptionHandler(config =>
//{
//  config.Run(async context =>
//  {
//    context.Response.StatusCode = 500;
//    context.Response.ContentType = "application/json";
//    var error = context.Features.Get<IExceptionHandlerFeature>();
//    if (error != null)
//    {
//      await context.Response.WriteAsync(
//        $"Exception: {error.Error?.Message} {error.Error?.InnerException?.Message}");
//    }
//  });
//});

app.MapControllers();
#endregion

app.Run();
