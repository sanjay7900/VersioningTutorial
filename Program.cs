using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using VersioningTutorial.ApiVersioningModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiVersioning(option =>
{
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.DefaultApiVersion = new ApiVersion(1, 0);
    //  option.ReportApiVersions = true;    
   
    //option.ApiVersionReader= new HeaderApiVersionReader("x-api-version");
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "API V1", Version = "v1" });
    options.SwaggerDoc("v2",new OpenApiInfo() { Version = "v2",Title= "API V2" });
    options.ResolveConflictingActions(e => e.First());
    options.OperationFilter<RemoveVersionFromParameter>();
    options.DocumentFilter<ReplaceVersionWithExactInPath>();



    ////options.OperationFilter<SwaggerDefaultValues>();
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //options.IncludeXmlComments(xmlPath);
    //// Enable API versioning support in Swagger
    //options.DocInclusionPredicate((version, desc) =>
    //{
    //    var versions = desc.CustomAttributes().OfType<ApiVersionAttribute>()
    //                      .SelectMany(attr => attr.Versions);

    //    return versions.Any(v => $"v{v.ToString()}" == version);
    //});

    //options.TagActionsBy(api =>
    //{
    //    if (api.GroupName != null)
    //    {
    //        return new[] { api.GroupName };
    //    }

    //    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
    //    if (controllerActionDescriptor != null)
    //    {
    //        return new[] { controllerActionDescriptor.ControllerName };
    //    }

    //    throw new InvalidOperationException("Unable to determine tag for endpoint.");
    //});

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options=>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
