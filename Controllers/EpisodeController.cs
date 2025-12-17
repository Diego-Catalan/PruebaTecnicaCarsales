using Microsoft.AspNetCore.Mvc;
using RickAndMortyBackend.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RickAndMortyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly IEpisodeRepository _service;

        public EpisodeController(IEpisodeRepository service)
        {
            _service = service;
        }

        [HttpGet]//GetAll
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllEpisodesAsync();
            return Ok(result);
        }
    }
}
