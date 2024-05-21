using DocumentInteligence.Model;

namespace DocumentInteligence.Repository;

public interface IInvoiceViews
{
    public IQueryable<Invoice> Invoices { get; }
}