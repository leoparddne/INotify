using Common.Notify.WebAPI.Config;
using Common.Notify.WebAPI.IService;
using Common.Notify.WebAPI.Model;
using Common.Notify.WebAPI.Service;
using Common.Notify.WebAPI.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var apiName = builder.Configuration["APIName"] ?? "Notify";
var swaggerFile = builder.Configuration["SwaggerFile"];

builder.Services.Configure<NotifyOption>(builder.Configuration.GetSection("Notify"));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(delegate (Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions c)
{
    c.SwaggerDoc(apiName, new OpenApiInfo { Title = apiName + "接口" });

    if (!string.IsNullOrWhiteSpace(swaggerFile))
    {
        foreach (var path in swaggerFile.Split(','))
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path.Trim());
            if (File.Exists(filePath))
            {
                c.IncludeXmlComments(filePath, includeControllerXmlComments: true);
            }
        }
    }
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var error = context.ModelState.Values.FirstOrDefault(f => f.ValidationState != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid);
        if (error != null && error.Errors != null && error.Errors.Count > 0)
        {
            return new JsonResult(new APIResponseModel<object> { Code = 1, Message = error.Errors.First().ErrorMessage });
        }

        return new JsonResult(new APIResponseModel<object> { Code = 1, Message = "ModelError" });
    };
});

builder.Services.AddScoped<IMessageService, MessageService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger(c => c.RouteTemplate = "{documentName}/swagger.json");
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
    c.SwaggerEndpoint($"/{apiName}/swagger.json", apiName);
});

app.MapControllers();

app.Run();
