using DocumentInteligence.Application.Commands.Suppliers;
using DocumentInteligence.Application.Queries.Suppliers;
using DocumentInteligence.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocumentInteligence.Controllers;

[ApiController]
[Route("api/v1/suppliers")]
public class SupplierController(ISender sender) : ControllerBase
{
    [HttpGet]
    [Route("get-suppliers")]
    public async Task<IEnumerable<SupplierView>> GetAllSuppliers() 
        => await sender.Send(new GetAllSuppliersQuery());

    [HttpPost]
    [Route("add-supplier")]
    public async Task<IActionResult> AddSupplier(AddSupplierRequest request)
    {
        await sender.Send(new AddSupplierCommand { SupplierName = request.Name, Service = request.Service, CustomerNumber = request.CustomerNumber});
        
        return Ok();
    }
}

public class AddSupplierRequest
{
    public string Name { get; set; }
    
    public string Service { get; set; }
    
    public string CustomerNumber { get; set; }
}