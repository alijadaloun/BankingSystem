using FinalLabTask1.Entities;
using FinalLabTask1.Middleware;
using FinalLabTask1.Services;
using FinalLabTask1.Services.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();//this gave an error if i did not register it, it is used in the globalexceptionhandler 
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddRazorPages();
builder.Services.AddDbContext<TransactionDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
//myservices-------------------------------------
builder.Services.AddScoped<ITransactionLogsService, TransactionLogService>();
builder.Services.AddScoped<ITransferService, TransferService>();
builder.Services.AddScoped<IAccountService, AccountService>();
//myservices-------------------------------------
builder.Services.AddOpenApi();
//ODATA---------------
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<TransactionLogs>("TransactionLogs");
builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
        "odata",
        modelBuilder.GetEdmModel()));
//ODATa----------------------
var app = builder.Build();
app.UseExceptionHandler();
var supportedCultures = new[] { "en", "fr", "es", "ar" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
localizationOptions.RequestCultureProviders.Insert(0, new AcceptLanguageHeaderRequestCultureProvider());
app.UseRequestLocalization(localizationOptions);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.Run();