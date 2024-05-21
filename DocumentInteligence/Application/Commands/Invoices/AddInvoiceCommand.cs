using DocumentInteligence.Application.Commands.Suppliers;
using DocumentInteligence.Model;
using DocumentInteligence.Repository;
using JetBrains.Annotations;
using MediatR;

namespace DocumentInteligence.Application.Commands.Invoices;

public class AddInvoiceCommand : IRequest<Unit>
{
    public int SupplierId { get; set; }
    public string InvoiceNumber { get; set; }
    public string InvoiceName { get; set; }
    
    public string InvoiceLocation { get; set; }
}

[UsedImplicitly]
public class AddInvoiceCommandHandler(IInvoice invoiceCommandRepository) : IRequestHandler<AddInvoiceCommand, Unit>
{ 
    public Task<Unit> Handle(AddInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = new Invoice
        {
          InvoiceName = request.InvoiceName,
          InvoiceNumber = request.InvoiceNumber,
          SupplierId = request.SupplierId,
          InvoiceLocation = request.InvoiceLocation
        };

        invoiceCommandRepository.Add(invoice);
        
        return Unit.Task;
    }
}