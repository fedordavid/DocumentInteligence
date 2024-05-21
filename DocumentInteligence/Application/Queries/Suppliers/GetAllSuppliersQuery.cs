using DocumentInteligence.Dto;
using DocumentInteligence.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentInteligence.Application.Queries.Suppliers;

public class GetAllSuppliersQuery : IRequest<SupplierView[]>
{
    
}

public class GetAllSuppliersQueryHandler(ISupplierViews supplierViews) : IRequestHandler<GetAllSuppliersQuery, SupplierView[]>
{
    public async Task<SupplierView[]> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
    {
        var suppliers = await supplierViews.Suppliers.ToListAsync(cancellationToken);

        return suppliers.Select(supplier => new SupplierView
        {
            Id = supplier.Id,
            Service = supplier.Service,
            Name = supplier.Name
        }).ToArray();
    }
}