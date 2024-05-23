using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIuni.Models;

namespace APIuni.Controllers
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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UniversityRankingYear>>> Get()
        {
            return await _universityContext.UniversityRankingYears.ToListAsync();

        }
        [Authorize]
        [HttpGet("{Uid}")]
        public async Task<ActionResult<UniversityRankingYear>> Get(int Uid)
        {
            var res = await _universityContext.UniversityRankingYears.Where(x => x.UniversityId == Uid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            else
                return new ObjectResult(res);

        }
        [Authorize]
        [HttpGet("{Uid}/{Rid}")]
        public async Task<ActionResult<UniversityRankingYear>> Get(int Uid, int Rid)
        {
            var res = await _universityContext.UniversityRankingYears.Where(x => x.UniversityId == Uid && x.RankingCriteriaId == Rid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            else
                return new ObjectResult(res);

        }
        [Authorize]
        [HttpGet("{Uid}/{Rid}/{Year}")]
        public async Task<ActionResult<UniversityRankingYear>> Get(int Uid, int Rid, int Year)
        {
            var res = await _universityContext.UniversityRankingYears.FirstOrDefaultAsync(x => x.UniversityId == Uid && x.RankingCriteriaId == Rid && x.Year == Year);
            if (res == null)
            {
                return NotFound();
            }
            else
                return new ObjectResult(res);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<UniversityRankingYear>> Post(UniversityRankingYear item)
        {
            if (item == null)
                return BadRequest();
            try
            {
                _universityContext.UniversityRankingYears.Add(item);
                await _universityContext.SaveChangesAsync();
                return Ok(item);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<UniversityRankingYear>> Put(UniversityRankingYear item)
        {
            if (item == null)
                return BadRequest();
            if (!_universityContext.UniversityRankingYears.Any(x => x.UniversityId == item.UniversityId && x.RankingCriteriaId == item.RankingCriteriaId && item.Year == x.Year))
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
        [HttpDelete("{Uid}/{Rid}/{Year}")]
        public async Task<ActionResult<UniversityRankingYear>> Delete(int Uid,int Rid, int Year)
        {
            var item = _universityContext.UniversityRankingYears.FirstOrDefault(x => x.UniversityId == Uid && x.RankingCriteriaId == Rid && x.Year == Year);
            if (item == null)
                return NotFound();
            _universityContext.UniversityRankingYears.Remove(item);
            await _universityContext.SaveChangesAsync();
            return Ok(item);
        }
    }
}
