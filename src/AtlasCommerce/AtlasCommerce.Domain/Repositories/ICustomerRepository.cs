using AtlasCommerce.Domain.Entities;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);

    Task<IEnumerable<Customer>> GetAllAsync();
}