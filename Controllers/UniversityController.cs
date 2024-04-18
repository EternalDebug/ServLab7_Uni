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
            return await _universityContext.Universities.Include(x => x.Country).ToListAsync();

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

        [HttpPost]
        public async Task<ActionResult<University>> Post(University item)
        {
            if (item == null)
                return BadRequest();

            _universityContext.Universities.Add(item);
            await _universityContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut]
        public async Task<ActionResult<University>> Put(University item)
        {
            if (item == null)
                return BadRequest();
            if (!_universityContext.Universities.Any(x => x.Id == item.Id))
                return NotFound();

            _universityContext.Update(item);
            await _universityContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<University>> Delete(int id)
        {
            var item = _universityContext.Universities.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();
            foreach (var i in _universityContext.UniversityYears.Where(x=>x.UniversityId == item.Id))
            {
                _universityContext.UniversityYears.Remove(i);
            }
            foreach (var i in _universityContext.UniversityRankingYears.Where(x => x.UniversityId == item.Id))
            {
                _universityContext.UniversityRankingYears.Remove(i);
            }
            //_universityContext.Countries.Where(x => x.Universities.Where(y => y.Id == item.Id))
            _universityContext.Universities.Remove(item);
            await _universityContext.SaveChangesAsync();
            return Ok(item);
        }
    }
}
