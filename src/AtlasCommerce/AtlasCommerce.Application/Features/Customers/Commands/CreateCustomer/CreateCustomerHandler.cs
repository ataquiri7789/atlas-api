using AtlasCommerce.Application.Features.Customers.Commands.CreateCustomer;
using AtlasCommerce.Domain.Entities;
using AtlasCommerce.Domain.Repositories;
using MediatR;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _customerRepository.AddAsync(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}