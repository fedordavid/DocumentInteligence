using DocumentInteligence.DbContext;
using DocumentInteligence.Model;
using Microsoft.EntityFrameworkCore;

namespace DocumentInteligence.Repository.QueryRepositories;

public class InvoiceQueryRepository(DatabaseContext context) : IInvoiceViews
{
    public IQueryable<Invoice> Invoices => context.Invoices.AsNoTracking();
}