﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<ActionResult<RankingCriterion>> Post(RankingCriterion item)
        {
            if (item == null)
                return BadRequest();

            _universityContext.RankingCriteria.Add(item);
            await _universityContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut]
        public async Task<ActionResult<RankingCriterion>> Put(RankingCriterion item)
        {
            if (item == null)
                return BadRequest();
            if (!_universityContext.RankingCriteria.Any(x => x.Id == item.Id))
                return NotFound();

            _universityContext.Update(item);
            await _universityContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RankingCriterion>> Delete(int id)
        {
            var item = _universityContext.RankingCriteria.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();
            foreach (var i in _universityContext.UniversityRankingYears.Where(x => x.RankingCriteriaId == item.Id))
            {
                _universityContext.UniversityRankingYears.Remove(i);
            }
            _universityContext.RankingCriteria.Remove(item);
            await _universityContext.SaveChangesAsync();
            return Ok(item);
        }
    }
}
