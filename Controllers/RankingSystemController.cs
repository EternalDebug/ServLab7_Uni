using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServLab7.Models;

namespace ServLab7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RankingSystemController : ControllerBase
    {
        private readonly ILogger<RankingSystemController> _logger;
        private readonly universityContext _universityContext;

        public RankingSystemController(ILogger<RankingSystemController> logger, universityContext universityContext)
        {
            _logger = logger;
            _universityContext = universityContext;
        }

        [HttpGet("{mode}")]
        public async Task<ActionResult< IEnumerable<RankingSystem>>> Get(int mode)
        {
            if (mode == 1)
                return await _universityContext.RankingSystems.Include(x => x.RankingCriteria).ToListAsync();
            else
                return await _universityContext.RankingSystems.ToListAsync();
        }

        [HttpGet("{mode}/{id}")]
        public async Task<ActionResult<RankingSystem>> Get(int mode,int id)
        {
            RankingSystem? res = null;
            if (mode == 1)
                res = await _universityContext.RankingSystems.FirstOrDefaultAsync(x => x.Id == id);
            else
                res = await _universityContext.RankingSystems.Include(x => x.RankingCriteria).FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
            {
                return NotFound();
            }
            else
                return new ObjectResult(res);
        }

        [HttpPost]
        public async Task<ActionResult<RankingSystem>> Post(RankingSystem item)
        {
            if (item == null)
                return BadRequest();

            _universityContext.RankingSystems.Add(item);
            await _universityContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut]
        public async Task<ActionResult<RankingSystem>> Put(RankingSystem item)
        {
            if (item == null)
                return BadRequest();
            if (!_universityContext.RankingSystems.Any(x => x.Id == item.Id))
                return NotFound();

            _universityContext.Update(item);
            await _universityContext.SaveChangesAsync();
            return Ok(item);
        }
    }
}
