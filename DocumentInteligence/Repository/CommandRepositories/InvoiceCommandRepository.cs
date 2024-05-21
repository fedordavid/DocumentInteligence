using DocumentInteligence.DbContext;
using DocumentInteligence.Model;
using MediatR;

namespace DocumentInteligence.Repository.CommandRepositories;

public class InvoiceCommandRepository(DatabaseContext databaseContext) : IInvoice
{
    public Task Add(Invoice invoice)
    {
        databaseContext.Invoices.Add(invoice);
        
        try
        {
            return databaseContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the error details
            Console.WriteLine(ex.InnerException?.Message);
        }

        return Unit.Task;
    }
}