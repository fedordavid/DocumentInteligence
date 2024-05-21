using DocumentInteligence.Model;
using DocumentInteligence.Repository;
using MediatR;

namespace DocumentInteligence.Application.Commands.Suppliers;

public class AddSupplierCommand : IRequest<Unit>
{
    public string SupplierName { get; set; }
    public string Service { get; set; }
    public string CustomerNumber { get; set; }
}

public class AddSupplierCommandHandler(ISupplier supplierCommandRepository) : IRequestHandler<AddSupplierCommand, Unit>
{ 
    public Task<Unit> Handle(AddSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = new Supplier
        {
            Name = request.SupplierName,
            Service = request.Service,
            CustomerNumber = request.CustomerNumber
        };

        supplierCommandRepository.Add(supplier);
        return Unit.Task;
    }
}