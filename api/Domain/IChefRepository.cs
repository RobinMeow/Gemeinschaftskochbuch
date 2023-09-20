using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Domain;

public interface IChefRepository
{
    Task AddAsync(Chef chef);
    Task<IEnumerable<Chef>> GetAllAsync();
}
