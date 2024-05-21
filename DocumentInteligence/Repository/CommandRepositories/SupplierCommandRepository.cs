using DocumentInteligence.DbContext;
using DocumentInteligence.Model;

namespace DocumentInteligence.Repository.CommandRepositories;

public class SupplierCommandRepository(DatabaseContext databaseContext) : ISupplier
{
    public Task Add(Supplier supplier)
    {
        databaseContext.Suppliers.Add(supplier);
        return databaseContext.SaveChangesAsync();
    }
}