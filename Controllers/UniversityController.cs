using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServLab7.Models;

namespace ServLab7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly ILogger<UniversityController> _logger;
        private readonly universityContext _universityContext;

        public UniversityController(ILogger<UniversityController> logger, universityContext universityContext)
        {
            _logger = logger;
            _universityContext = universityContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<University>>> Get()
        {
            return await _universityContext.Universities.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<University>> Get(int id)
        {
            //return await _universityContext.RankingSystems.ToListAsync();
            var res = await _universityContext.Universities.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
            {
                return NotFound();
            }
            else
                return new ObjectResult(res);
        }
    }
}
