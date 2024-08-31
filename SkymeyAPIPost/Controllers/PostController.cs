using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SkymeyAPIPost.Data;
using SkymeyLibs;
using SkymeyLibs.Models.Tables.Posts;

namespace SkymeyAPIPost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : Controller
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
        public IActionResult CreatePost([FromBody]POST_VIEW_MODEL VIEW_MODEL)
        {
            VIEW_MODEL.API_POST._id = ObjectId.GenerateNewId();
            _db.API_POST.Add(VIEW_MODEL.API_POST);
            _db.SaveChanges();
            foreach(var item in VIEW_MODEL.API_POST_TAGS)
            {
                item._id = ObjectId.GenerateNewId();
                item.POST_ID = VIEW_MODEL.API_POST._id;
            }
            _db.API_POST_TAGS.AddRange(VIEW_MODEL.API_POST_TAGS);
            _db.SaveChanges();
            foreach (var item in VIEW_MODEL.API_POST_RESPONSES)
            {
                item._id = ObjectId.GenerateNewId();
                item.POST_ID = VIEW_MODEL.API_POST._id;
            }
            _db.API_POST_RESPONSES.AddRange(VIEW_MODEL.API_POST_RESPONSES);
            _db.SaveChanges();
            foreach (var item in VIEW_MODEL.API_POST_PARAMS)
            {
                item._id = ObjectId.GenerateNewId();
                item.POST_ID = VIEW_MODEL.API_POST._id;
            }
            _db.API_POST_PARAMS.AddRange(VIEW_MODEL.API_POST_PARAMS);
            _db.SaveChanges();
            foreach (var item in VIEW_MODEL.API_POST_CODE_SAMPLES)
            {
                item._id = ObjectId.GenerateNewId();
                item.POST_ID = VIEW_MODEL.API_POST._id;
            }
            _db.API_POST_CODE_SAMPLES.AddRange(VIEW_MODEL.API_POST_CODE_SAMPLES);
            _db.SaveChanges();
            return Ok("Ok");
        }
    }
}
