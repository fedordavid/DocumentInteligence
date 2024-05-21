using DocumentInteligence.Model;

namespace DocumentInteligence.Repository;

public interface ISupplierViews
{
    public IQueryable<Supplier> Suppliers { get; }
}