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
        public async Task<IActionResult> CreatePost(POST_VIEW_MODEL VIEW_MODEL)
        {
            try
            {
                using (var session = await _mongoClient.StartSessionAsync())
                {
                    session.StartTransaction();
                    VIEW_MODEL.API_POST._id = ObjectId.GenerateNewId();
                    await _db.API_POST.AddAsync(VIEW_MODEL.API_POST);
                    //_db.SaveChanges();
                    foreach (var item in VIEW_MODEL.API_POST_TAGS)
                    {
                        item._id = ObjectId.GenerateNewId();
                        item.POST_ID = VIEW_MODEL.API_POST._id;
                    }
                    await _db.API_POST_TAGS.AddRangeAsync(VIEW_MODEL.API_POST_TAGS);
                    //_db.SaveChanges();
                    foreach (var item in VIEW_MODEL.API_POST_RESPONSES)
                    {
                        item._id = ObjectId.GenerateNewId();
                        item.POST_ID = VIEW_MODEL.API_POST._id;
                    }
                    await _db.API_POST_RESPONSES.AddRangeAsync(VIEW_MODEL.API_POST_RESPONSES);
                    //_db.SaveChanges();
                    foreach (var item in VIEW_MODEL.API_POST_PARAMS)
                    {
                        item._id = ObjectId.GenerateNewId();
                        item.POST_ID = VIEW_MODEL.API_POST._id;
                    }
                    await _db.API_POST_PARAMS.AddRangeAsync(VIEW_MODEL.API_POST_PARAMS);
                    //_db.SaveChangesAsync();
                    foreach (var item in VIEW_MODEL.API_POST_CODE_SAMPLES)
                    {
                        item._id = ObjectId.GenerateNewId();
                        item.POST_ID = VIEW_MODEL.API_POST._id;
                    }
                    await _db.API_POST_CODE_SAMPLES.AddRangeAsync(VIEW_MODEL.API_POST_CODE_SAMPLES);
                    await _db.SaveChangesAsync();
                    await session.CommitTransactionAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());   
            }
            return Ok("Ok");
        }
    }
}
