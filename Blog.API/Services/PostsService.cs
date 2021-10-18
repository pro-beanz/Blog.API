using Blog.API.Data;
using Blog.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Services
{
    public class PostsService
    {
        private readonly BlogDbContext _context;

        public PostsService(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostData>> GetAllAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<PostData> GetAsync(long id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<PostData> UpdateAsync(long id, PostAPI post)
        {
            var data = ApiToData(id, post);
            _context.Entry(data).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id)) return null;
                throw;
            }
            return data;
        }

        public async Task<PostData> CreateAsync(PostAPI post)
        {
            var data = ApiToData(post);
            _context.Posts.Add(data);

            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return false;

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool PostExists(long id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

        private PostData ApiToData(PostAPI post)
        {
            return new PostData()
            {
                Author = post.Author,
                Content = post.Content,
                Score = post.Score
            };
        }

        private PostData ApiToData(long id, PostAPI post)
        {
            return new PostData()
            {
                Id = id,
                Author = post.Author,
                Content = post.Content,
                Score = post.Score
            };
        }
    }
}
