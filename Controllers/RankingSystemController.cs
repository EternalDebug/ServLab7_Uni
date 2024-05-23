using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIuni.Models;

namespace APIuni.Controllers
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
        [Authorize]
        [HttpGet("{mode}")]
        public async Task<ActionResult< IEnumerable<RankingSystem>>> Get(int mode)
        {
            if (mode == 1)
                return await _universityContext.RankingSystems.Include(x => x.RankingCriteria).ToListAsync();
            else
                return await _universityContext.RankingSystems.ToListAsync();
        }
        [Authorize]
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<RankingSystem>> Post(RankingSystem item)
        {
            if (item == null)
                return BadRequest();
            try
            {
                _universityContext.RankingSystems.Add(item);
                await _universityContext.SaveChangesAsync();
                return Ok(item);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<RankingSystem>> Put(RankingSystem item)
        {
            if (item == null)
                return BadRequest();
            if (!_universityContext.RankingSystems.Any(x => x.Id == item.Id))
                return NotFound();

            try
            {
                _universityContext.Update(item);
                await _universityContext.SaveChangesAsync();
                return Ok(item);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<RankingSystem>> Delete(int id)
        {
            var item = _universityContext.RankingSystems.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();
            foreach (var i in _universityContext.RankingCriteria.Where(x => x.RankingSystemId == item.Id))
            {
                if (i == null) continue;
                foreach (var j in _universityContext.UniversityRankingYears.Where(x => x.RankingCriteriaId == i.Id))
                {
                    _universityContext.UniversityRankingYears.Remove(j);
                }
                _universityContext.RankingCriteria.Remove(i);
            }
            _universityContext.RankingSystems.Remove(item);
            await _universityContext.SaveChangesAsync();
            return Ok(item);
        }
    }
}
