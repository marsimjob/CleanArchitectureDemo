using Application.Layer.Interfaces;
using Domain.Layer.Models;
using Infrastructure.Layer;
using Microsoft.EntityFrameworkCore;

public class ToyRepository : IToyRepository
{
    private readonly AppDbContext _context;

    public ToyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Toy newToy, CancellationToken cancellationToken)
    {
        await _context.Toys.AddAsync(newToy, cancellationToken); 
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Toy>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Toys.ToListAsync();
    }

    public async Task<Toy?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Toys.FindAsync(id);
    }
}