using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SkymeyAPIPost.Data;
using SkymeyLibs;
using SkymeyLibs.Models.Tables.Posts;

namespace SkymeyAPIPost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private static MongoClient _mongoClient;
        private static ApplicationContext _db;
        private readonly ILogger<PostController> _logger;
        private readonly IOptions<MainSettings> _options;

        public PostController(ILogger<PostController> logger, IOptions<MainSettings> options)
        {
            _logger = logger;
            _options = options;
            _mongoClient = new MongoClient(_options.Value.MongoDatabase.DBServer);
            _db = ApplicationContext.Create(_mongoClient.GetDatabase(_options.Value.MongoDatabase.DBName));
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
