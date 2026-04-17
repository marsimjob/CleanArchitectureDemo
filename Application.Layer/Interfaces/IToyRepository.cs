using Domain.Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Layer.Interfaces
{
    public interface IToyRepository
    {
        Task<List<Toy>> GetAllAsync(CancellationToken cancellationToken);
        Task<Toy?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(Toy newToy, CancellationToken cancellationToken);

        Task SoftDeleteAsync(int id, CancellationToken cancellationToken);
    }
}