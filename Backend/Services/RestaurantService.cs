using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class RestaurantService(ManticoreContext context)
{
    public async Task<IReadOnlyList<Restauracja>> GetRestauracjeAsync(CancellationToken cancellationToken = default)
    {
        return await context.Restauracje
            .AsNoTracking()
            .OrderBy(p => p.Nr_restauracji)
            .ToListAsync(cancellationToken);
    }
}