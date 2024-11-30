using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repositories
{
    
    public class TagRepository
    {
        private readonly BlogDbContext _dbContext;

        public TagRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TagEntity>> GetAllTagsAsync()
        {
            return await _dbContext.Tags
                .Include(t => t.PostTags)
                .ThenInclude(pt => pt.Post)
                .ToListAsync();
        }

        public async Task<TagEntity?> GetTagByIdAsync(Guid id)
        {
            return await _dbContext.Tags
                .Include(t => t.PostTags)
                .ThenInclude(pt => pt.Post)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTagAsync(TagEntity tag)
        {
            _dbContext.Tags.Add(tag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTagAsync(TagEntity tag)
        {
            _dbContext.Tags.Update(tag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(Guid id)
        {
            var tag = await GetTagByIdAsync(id);
            if (tag != null)
            {
                _dbContext.Tags.Remove(tag);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
