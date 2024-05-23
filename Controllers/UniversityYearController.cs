using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIuni.Models;

namespace APIuni.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniversityYearController : ControllerBase
    {
        private readonly ILogger<UniversityYearController> _logger;
        private readonly universityContext _universityContext;

        public UniversityYearController(ILogger<UniversityYearController> logger, universityContext universityContext)
        {
            _logger = logger;
            _universityContext = universityContext;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UniversityYear>>> Get()
        {
            return await _universityContext.UniversityYears.ToListAsync();

        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UniversityYear>>> Get(int id)
        {
            var res = await _universityContext.UniversityYears.Where(x => x.UniversityId == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            else
                return new ObjectResult(res);

        }
        [Authorize]
        [HttpGet("{id}/{year}")]
        public async Task<ActionResult<UniversityYear>> Get(int id, int year)
        {
            var res = await _universityContext.UniversityYears.FirstOrDefaultAsync(x => x.UniversityId == id && x.Year == year);
            if (res == null)
            {
                return NotFound();
            }
            else
                return new ObjectResult(res);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<UniversityYear>> Post(UniversityYear item)
        {
            if (item == null)
                return BadRequest();
            try
            {
                _universityContext.UniversityYears.Add(item);
                await _universityContext.SaveChangesAsync();
                return Ok(item);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<UniversityYear>> Put(UniversityYear item)
        {
            if (item == null)
                return BadRequest();
            if (!_universityContext.UniversityYears.Any(x => x.UniversityId == item.UniversityId && x.Year == item.Year))
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
        [HttpDelete("{id}/{year}")]
        public async Task<ActionResult<UniversityYear>> Delete(int id, int year)
        {
            var item = _universityContext.UniversityYears.FirstOrDefault(x => x.UniversityId == id && x.Year == year);
            if (item == null)
                return NotFound();
            _universityContext.UniversityYears.Remove(item);
            await _universityContext.SaveChangesAsync();
            return Ok(item);
        }
    }
}
