using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repositories
{
    public class PostRepository
    {
        private readonly BlogDbContext _dbContext;

        public PostRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Получить все посты
        public async Task<IEnumerable<PostEntity>> GetAllPostsAsync()
        {
            return await _dbContext.Posts
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .ToListAsync();
        }

        // Получить пост по ID
        public async Task<PostEntity?> GetPostByIdAsync(Guid id)
        {
            return await _dbContext.Posts
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Добавить новый пост
        public async Task AddPostAsync(PostEntity post)
        {
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
        }

        // Обновить существующий пост
        public async Task UpdatePostAsync(PostEntity post)
        {
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }

        // Удалить пост
        public async Task DeletePostAsync(Guid id)
        {
            var post = await GetPostByIdAsync(id);
            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
