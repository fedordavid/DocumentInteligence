using DocumentInteligence.DbContext;
using DocumentInteligence.Repository;
using DocumentInteligence.Repository.CommandRepositories;
using DocumentInteligence.Repository.QueryRepositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
var db = configuration.GetConnectionString("DatabaseContext");
builder.Configuration.AddConfiguration(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<DatabaseContext>(options 
    => options.UseSqlServer(configuration.GetConnectionString("DatabaseContext")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddScoped<IInvoiceViews, InvoiceQueryRepository>();
builder.Services.AddScoped<ISupplierViews, SupplierQueryRepository>();
builder.Services.AddScoped<ISupplier, SupplierCommandRepository>();
builder.Services.AddScoped<IInvoice, InvoiceCommandRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyUI",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.MapGet("/vvs", async ([FromServices]IServiceProvider services, CancellationToken cancellationToken) =>
//     {
//         //TODO:
//         var repository = services.GetRequiredService<IRepository<InvoiceItem>>();
//             
//         //TODO: configurations
//         const string endpoint = "https://vvs.cognitiveservices.azure.com/";
//         const string apiKey = "b69f7781766a439987dd1fad01b7682d";
//         var credential = new AzureKeyCredential(apiKey);
//         var client = new DocumentAnalysisClient(new Uri(endpoint), credential);
//         
//         //TODO: access to sample pdf file
//         const string filePath = @"C:\Users\ZZ020T693\Downloads\DocumentIntelligence\Invoice_2500956800.pdf";
//         
//         await using var stream = new FileStream(filePath, FileMode.Open);
//         // var fileUri = new Uri("<fileUri>");
//         
//         //TODO: can be used a stream, or URI
//         var operation = await client.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-invoice", stream, null, cancellationToken);
//         var result = operation.Value;
//         
//         //TODO: extract table data
//         var table = result.Tables[2];
//         var invoiceItem = new InvoiceItem
//         {
//             ItemName = table.Cells[5].Content,
//             TaxBase =  decimal.TryParse(table.Cells[6].Content.Trim().Replace(" ", "").Replace(",", "."), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var taxBaseOut) ? Math.Round(taxBaseOut, 2) : 0,
//             TaxRate = decimal.TryParse(table.Cells[7].Content.Trim().Replace(" ", "").Replace(",", "."), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var taxRateOut) ? Math.Round(taxRateOut, 2) : 0,
//             TaxAmount = decimal.TryParse(table.Cells[8].Content.Trim().Replace(" ", "").Replace(",", "."), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var taxAmountOut) ? Math.Round(taxAmountOut, 2) : 0,
//             Total = decimal.TryParse(table.Cells[9].Content.Trim().Replace(" ", "").Replace(",", "."), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var totalOut) ? Math.Round(totalOut, 2) : 0
//         };
//         
//         repository.Add(invoiceItem);
//
//         return invoiceItem;
//     })
//     .WithName("vvs")
//     .WithOpenApi();
app.MapControllers();
app.UseCors("AllowMyUI");

app.Run();