using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServLab7.Models;

namespace ServLab7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RankingCriteriaController : ControllerBase
    {
        private readonly ILogger<RankingCriteriaController> _logger;
        private readonly universityContext _universityContext;

        public RankingCriteriaController(ILogger<RankingCriteriaController> logger, universityContext universityContext)
        {
            _logger = logger;
            _universityContext = universityContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RankingCriterion>>> Get()
        {
            return await _universityContext.RankingCriteria.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RankingCriterion>> Get(int id)
        {
            //return await _universityContext.Countries.ToListAsync();
            var res = await _universityContext.RankingCriteria.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
            {
                return NotFound();
            }
            else
                return new ObjectResult(res);
        }
    }
}
