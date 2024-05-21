using DocumentInteligence.Model;

namespace DocumentInteligence.Repository;

public interface IInvoice
{
    Task Add(Invoice supplier);
}