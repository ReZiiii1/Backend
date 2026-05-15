using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class MenuService(MenuContext context)
{
    public async Task<IReadOnlyList<Produkt>> GetProduktyAsync(CancellationToken cancellationToken = default)
    {
        return await context.Produkty
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToListAsync(cancellationToken);
    }
}