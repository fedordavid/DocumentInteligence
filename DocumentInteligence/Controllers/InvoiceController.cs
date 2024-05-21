using DocumentInteligence.Application.Commands.Invoices;
using DocumentInteligence.Application.Queries.Invoice;
using DocumentInteligence.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocumentInteligence.Controllers;

[ApiController]
[Route("api/v1/invoices")]
public class InvoiceController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<InvoiceView>> GetInvoices() 
        => await sender.Send(new GetAllInvoicesQuery());
    
    [HttpPost]
    [Route("add-invoice")]
    public async Task<IActionResult> AddInvoice(AddInvoiceRequest request)
    {
        await sender.Send(new AddInvoiceCommand
        {
            InvoiceNumber = request.InvoiceNumber, 
            SupplierId = request.SupplierId, 
            InvoiceName = request.InvoiceName,
            InvoiceLocation = request.InvoiceLocation
        });
        
        return Ok();
    }
}

public class AddInvoiceRequest
{
    public string InvoiceNumber { get; set; }
    public int SupplierId { get; set; }
    public string InvoiceName { get; set; }
    public string InvoiceLocation { get; set; }
}