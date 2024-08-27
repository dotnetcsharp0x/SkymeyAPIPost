using Microsoft.AspNetCore.Mvc;

namespace SkymeyAPIPost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Test")]
        public string Get()
        {
            return "Ok";
        }
    }
}
