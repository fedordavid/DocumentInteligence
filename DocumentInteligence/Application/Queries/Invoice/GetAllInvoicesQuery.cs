using DocumentInteligence.Dto;
using DocumentInteligence.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentInteligence.Application.Queries.Invoice;

public class GetAllInvoicesQuery : IRequest<InvoiceView[]>
{
    
}

public class GetAllInvoicesQueryHandler(IInvoiceViews invoiceViews) : IRequestHandler<GetAllInvoicesQuery, InvoiceView[]>
{
    public async Task<InvoiceView[]> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
    {
        var invoices = await invoiceViews.Invoices.Include(x => x.Supplier).ToListAsync(cancellationToken);

        return invoices.Select(invoice => new InvoiceView
        {
            Id = invoice.Id,
            InvoiceNumber = invoice.InvoiceNumber,
            SupplierId = invoice.SupplierId,
            SupplierName = invoice.Supplier.Name,
        }).ToArray();
    }
}