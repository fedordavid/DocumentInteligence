using DocumentInteligence.DbContext;
using DocumentInteligence.Model;
using Microsoft.EntityFrameworkCore;

namespace DocumentInteligence.Repository.QueryRepositories;

public class SupplierQueryRepository(DatabaseContext context) : ISupplierViews
{
    public IQueryable<Supplier> Suppliers => context.Suppliers.AsNoTracking();
}