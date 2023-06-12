using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Domain;

public interface IRezeptRepository
{
    void AddAsync(Rezept rezept);
    Task<IEnumerable<Rezept>> GetAllAsync();
}