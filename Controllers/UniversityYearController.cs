using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServLab7.Models;

namespace ServLab7.Controllers
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UniversityYear>>> Get()
        {
            return await _universityContext.UniversityYears.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UniversityYear>> Get(int id)
        {
            var res = await _universityContext.UniversityYears.FirstOrDefaultAsync(x => x.UniversityId == id);
            if (res == null)
            {
                return NotFound();
            }
            else
                return new ObjectResult(res);

        }
    }
}
