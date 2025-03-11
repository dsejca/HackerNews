using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Stories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsController : ControllerBase
    {
        private readonly IHackerStories _hackerStories;

        public HackerNewsController(IHackerStories hackerStories)
        {
            _hackerStories = hackerStories;     
        }

        [HttpGet]
        [Route("beststories")]
        public async Task<IActionResult> HackerStories(int numberOfStories) 
        {
            if (numberOfStories > 0)
            {
                return Ok(await _hackerStories.ListOfHackerStories(numberOfStories));
            }
            else 
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("beststories/{id}")]
        public async Task<IActionResult> HackerStoriesById(long id)
        {
            if (id > 0)
            {
                return Ok(await _hackerStories.HackerStoriesById(id));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
