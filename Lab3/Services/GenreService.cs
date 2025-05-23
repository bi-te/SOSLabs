using GamesApi.Data;
using GamesApi.DTOs;
using GamesApi.Entities;
using GamesApi.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.Services;

public class GenreService(GamesDbContext dbContext)
{
    private GamesDbContext _dbContext = dbContext;

    public async Task<IEnumerable<GenreDto>> GetGenresAsync()
    {
        return await _dbContext.Genres.AsNoTracking().Select(genre => genre.ToDto()).ToListAsync();
    }

    public async Task<GenreDto?> GetGenreAsync(int id)
    {
        return (await _dbContext.Genres.FindAsync(id))?.ToDto();
    }

    public async Task<GenreDto> AddGenreAsync(CreateGenreDto addDto)
    {
        var existing =  await GetGenreByNameAsync(addDto.Name);
        if (existing is not null)
        {
            if (existing.IsDeleted)
            {
                existing.IsDeleted = false;
                await _dbContext.SaveChangesAsync();
            }
            return existing.ToDto();
        }
            
            
        Genre newGenre = new Genre() { Name = addDto.Name };
        
        var addedGenre = await _dbContext.Genres.AddAsync(newGenre);
        await _dbContext.SaveChangesAsync();

        return addedGenre.Entity.ToDto();
    }

    public async Task EditGenre(int id, CreateGenreDto editDto)
    {
        var existing = await GetGenreByIdAsync(id);
        if (existing is null)
        {
            await AddGenreAsync(editDto);
            return;
        }

        existing.Name = editDto.Name;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteGenre(int id)
    {
        await _dbContext.Genres.Where(genre => genre.GenreId == id)
            .ExecuteUpdateAsync(setters => setters.SetProperty(g => g.IsDeleted, true));
        //await _dbContext.Genres.Where(genre => genre.GenreId == id).ExecuteDeleteAsync();
    }

    private async Task<Genre?> GetGenreByNameAsync(string name)
    {
        return await _dbContext.Genres.IgnoreQueryFilters().SingleOrDefaultAsync(genre => 
                                                    genre.Name.ToLower().Equals(name.ToLower()));
    }

    private async Task<Genre?> GetGenreByIdAsync(int id)
    {
        return await _dbContext.Genres.FindAsync(id);
    }
    
}