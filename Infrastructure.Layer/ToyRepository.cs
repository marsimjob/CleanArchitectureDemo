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

    public async Task SoftDeleteAsync(int id, CancellationToken cancellationToken)
    {
        var toy = await _context.Toys.FindAsync(id);

        if (toy == null)
            throw new Exception("Toy not found");

        toy.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

}