using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Domain;

public interface IRezeptRepository
{
    void Add(Rezept rezept);
    Task<IEnumerable<Rezept>> GetAll();
}