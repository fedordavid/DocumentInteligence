using System.Globalization;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using DocumentInteligence.DbContext;
using DocumentInteligence.Model;
using DocumentInteligence.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

builder.Configuration.AddConfiguration(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddDbContext<DatabaseContext>(options 
    => options.UseSqlServer(configuration.GetConnectionString("DatabaseContext")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/vvs", async ([FromServices]IServiceProvider services, CancellationToken cancellationToken) =>
    {
        //TODO:
        var repository = services.GetRequiredService<IRepository<InvoiceItem>>();
            
        //TODO: configurations
        const string endpoint = "https://vvs.cognitiveservices.azure.com/";
        const string apiKey = "";
        var credential = new AzureKeyCredential(apiKey);
        var client = new DocumentAnalysisClient(new Uri(endpoint), credential);
        
        //TODO: access to sample pdf file
        const string filePath = @"C:\Users\Tímea Fedorová\Downloads\Document Inteligence\vvs\Invoice_2500956800.pdf";
        
        await using var stream = new FileStream(filePath, FileMode.Open);
        // var fileUri = new Uri("<fileUri>");
        
        //TODO: can be used a stream, or URI
        var operation = await client.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-layout", stream, null, cancellationToken);
        var result = operation.Value;
        
        //TODO: extract table data
        var table = result.Tables[2];
        var invoiceItem = new InvoiceItem
        {
            ItemName = table.Cells[5].Content,
            TaxBase =  decimal.TryParse(table.Cells[6].Content.Trim().Replace(" ", "").Replace(",", "."), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var taxBaseOut) ? Math.Round(taxBaseOut, 2) : 0,
            TaxRate = decimal.TryParse(table.Cells[7].Content.Trim().Replace(" ", "").Replace(",", "."), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var taxRateOut) ? Math.Round(taxRateOut, 2) : 0,
            TaxAmount = decimal.TryParse(table.Cells[8].Content.Trim().Replace(" ", "").Replace(",", "."), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var taxAmountOut) ? Math.Round(taxAmountOut, 2) : 0,
            Total = decimal.TryParse(table.Cells[9].Content.Trim().Replace(" ", "").Replace(",", "."), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var totalOut) ? Math.Round(totalOut, 2) : 0
        };
        
        repository.Add(invoiceItem);

        return invoiceItem;
    })
    .WithName("vvs")
    .WithOpenApi();

app.Run();