using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServLab7.Models;

namespace ServLab7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniversityRankingYearController : ControllerBase
    {
        private readonly ILogger<UniversityRankingYearController> _logger;
        private readonly universityContext _universityContext;

        public UniversityRankingYearController(ILogger<UniversityRankingYearController> logger, universityContext universityContext)
        {
            _logger = logger;
            _universityContext = universityContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UniversityRankingYear>>> Get()
        {
            return await _universityContext.UniversityRankingYears.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UniversityRankingYear>> Get(int id)
        {
            var res = await _universityContext.UniversityRankingYears.FirstOrDefaultAsync(x => x.UniversityId == id);
            if (res == null)
            {
                return NotFound();
            }
            else
                return new ObjectResult(res);

        }
    }
}
