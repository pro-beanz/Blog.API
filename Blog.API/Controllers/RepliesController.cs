using Blog.API.Models;
using Blog.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private readonly RepliesService _repliesService;

        public RepliesController(RepliesService repliesService)
        {
            _repliesService = repliesService;
        }

        // GET: api/Replies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReplyData>>> GetReplies()
        {
            return Ok(await _repliesService.GetAllAsync());
        }

        // GET: api/Replies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReplyData>> GetReply(long id)
        {
            var reply = await _repliesService.GetAsync(id);
            return reply == null ? NotFound() : Ok(reply);
        }

        // PUT: api/Replies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReply(long id, ReplyAPI reply)
        {
            var data = await _repliesService.UpdateAsync(id, reply);
            return data == null ? NotFound() : Ok(data);
        }

        // POST: api/Replies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReplyData>> PostReply(ReplyAPI reply)
        {
            var data = await _repliesService.CreateAsync(reply);
            return CreatedAtAction("GetReply", new { id = data.Id }, data);
        }

        // DELETE: api/Replies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReply(long id)
        {
            return await _repliesService.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}
