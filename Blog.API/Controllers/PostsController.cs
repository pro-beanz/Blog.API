using Blog.API.Models;
using Blog.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostsService _postsService;

        public PostsController(PostsService postsService)
        {
            _postsService = postsService;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostData>>> GetPosts()
        {
            return Ok(await _postsService.GetAllAsync());
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostData>> GetPost(long id)
        {
            var post = await _postsService.GetAsync(id);
            return post == null ? NotFound() : Ok(post);
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(long id, PostAPI post)
        {
            var data = await _postsService.UpdateAsync(id, post);
            return data == null ? NotFound() : Ok(data);
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostData>> PostPost(PostAPI post)
        {
            var data = await _postsService.CreateAsync(post);
            return CreatedAtAction("GetPost", new { id = data.Id }, data);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(long id)
        {
            return await _postsService.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}
