using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SkymeyAPIPost.Data;
using SkymeyLibs.Models.Tables.Posts;

namespace SkymeyAPIPost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private static MongoClient _mongoClient = new MongoClient("mongodb://127.0.0.1:27017");
        private static ApplicationContext _db = ApplicationContext.Create(_mongoClient.GetDatabase("skymey"));
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("CreatePost")]
        public async Task<string> CreatePost(API_POST POST)
        {
            POST.API_URL = "url";
            POST.Title = "title";
            POST.Description = "description";
            POST.Content = "content";
            await _db.AddAsync(POST);
            await _db.SaveChangesAsync();
            return "Ok";
        }
    }
}
