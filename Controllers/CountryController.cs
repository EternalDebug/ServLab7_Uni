using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServLab7.Models;

namespace ServLab7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly universityContext _universityContext;

        public CountryController(ILogger<CountryController> logger, universityContext universityContext)
        {
            _logger = logger;
            _universityContext = universityContext;
        }

        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> Get()
        {
            return await _universityContext.Countries.ToListAsync();

        }*/

        [Authorize]
        [HttpGet("{mode}")]
        public async Task<ActionResult<IEnumerable<Country>>> Get(string mode = "0")
        {
            if (mode == "1")
                return await _universityContext.Countries.Include(b => b.Universities).ToListAsync();
            else
                return await _universityContext.Countries.ToListAsync();
        }

        [Authorize]
        [HttpGet("{mode}/{id}")]
        public async Task<ActionResult<Country>> Get(string mode , int id)
        {
            //return await _universityContext.Countries.ToListAsync();
            Country? res = null;
            if (mode == "1")
                res = await _universityContext.Countries.Include(b => b.Universities).FirstOrDefaultAsync(x => x.Id == id);
            else
                res = await _universityContext.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
            {
                return NotFound();
            }
            else
                return new ObjectResult(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Country>> Post(Country item)
        {
            if (item == null)
                return BadRequest();

            _universityContext.Countries.Add(item);
            await _universityContext.SaveChangesAsync();
            return Ok(item);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<Country>> Put(Country item)
        {
            if (item == null)
                return BadRequest();
            if (!_universityContext.Countries.Any(x => x.Id == item.Id))
                return NotFound();

            _universityContext.Update(item);
            await _universityContext.SaveChangesAsync();
            return Ok(item);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> Delete(int id)
        {
            var item = _universityContext.Countries.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();
            foreach (var i in _universityContext.Universities.Where(x => x.CountryId == item.Id))
            {
                if (i == null) continue; 
                foreach (var j in _universityContext.UniversityYears.Where(x => x.UniversityId == i.Id))
                {
                    _universityContext.UniversityYears.Remove(j);
                }
                foreach (var j in _universityContext.UniversityRankingYears.Where(x => x.UniversityId == i.Id))
                {
                    _universityContext.UniversityRankingYears.Remove(j);
                }
                _universityContext.Universities.Remove(i);
            }
            _universityContext.Countries.Remove(item);
            await _universityContext.SaveChangesAsync();
            return Ok(item);
        }
    }
}
