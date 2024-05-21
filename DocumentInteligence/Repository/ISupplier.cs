using DocumentInteligence.Model;

namespace DocumentInteligence.Repository;

public interface ISupplier
{
    Task Add(Supplier supplier);
    // Task Delete(Guid productId);
    // Task Update(Product product);
    // Task<Product> Get(Guid productId);
}