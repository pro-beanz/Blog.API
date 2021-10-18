using Blog.API.Data;
using Blog.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Services
{
    public class RepliesService
    {
        private readonly BlogDbContext _context;

        public RepliesService(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReplyData>> GetAllAsync()
        {
            return await _context.Replies.ToListAsync();
        }

        public async Task<ReplyData> GetAsync(long id)
        {
            return await _context.Replies.FindAsync(id);
        }

        public async Task<ReplyData> UpdateAsync(long id, ReplyAPI reply)
        {
            var data = ApiToData(reply);
            _context.Entry(data).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReplyExists(id)) return null;
                throw;
            }
            return data;
        }

        public async Task<ReplyData> CreateAsync(ReplyAPI reply)
        {
            var data = ApiToData(reply);
            _context.Replies.Add(data);

            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var reply = await _context.Replies.FindAsync(id);
            if (reply == null) return false;

            _context.Replies.Remove(reply);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool ReplyExists(long id)
        {
            return _context.Replies.Any(e => e.Id == id);
        }

        private ReplyData ApiToData(ReplyAPI reply)
        {
            return new ReplyData()
            {
                Author = reply.Author,
                Content = reply.Content,
                Score = reply.Score,
                ParentId = reply.ParentId
            };
        }
        
        private ReplyData ApiToData(long id, ReplyAPI reply)
        {
            return new ReplyData()
            {
                Id = id,
                Author = reply.Author,
                Content = reply.Content,
                Score = reply.Score,
                ParentId = reply.ParentId
            };
        }
    }
}
