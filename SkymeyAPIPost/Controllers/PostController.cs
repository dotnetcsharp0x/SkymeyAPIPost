using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SkymeyLibs;
using SkymeyLibs.Data;
using SkymeyLibs.Interfaces.Data;
using SkymeyLibs.Models.Tables.Posts;

namespace SkymeyAPIPost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : Controller
    {
        private readonly IMongoRepository _db;
        private readonly ILogger<PostController> _logger;
        private readonly IOptions<MainSettings> _options;

        public PostController(ILogger<PostController> logger, IOptions<MainSettings> options)
        {
            _logger = logger;
            _options = options;
            _db = new MongoPostRepository(_options);
        }

        [HttpPost]
        [Route("CreatePost")]
        public async Task<IActionResult> CreatePost(POST_VIEW_MODEL VIEW_MODEL)
        {
            await _db.CreatePost(VIEW_MODEL);
            return Ok("Ok");
        }
    }
}
