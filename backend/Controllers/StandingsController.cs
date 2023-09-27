using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StandingsController : ControllerBase
    {
        private readonly NbaDbContext _dbContext;

        public StandingsController(NbaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("bySeason")]
        public async Task<ActionResult<IEnumerable<Standings>>> GetStandingsBySeason([FromQuery] int season)
        {
            try
            {
                // Get standings for the specified season from the database
                var standings = await _dbContext.Standings
                    .Where(standing => standing.Season == season)
                    .ToListAsync();

                return Ok(standings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne du serveur : {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddStandings([FromBody] List<Standings> standings)
        {
            try
            {
                // Check if standings for this season already exist in database
                bool standingsExist = await _dbContext.Standings
                    .AnyAsync(standing => standing.Season == standings.First().Season);

                if (standingsExist)
                {
                    return Ok("Classement déjà ajouté pour cette saison.");
                }

                // Add standings to database
                await _dbContext.Standings.AddRangeAsync(standings);
                await _dbContext.SaveChangesAsync();
                return Ok("Classement ajouté avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne du serveur : {ex.Message}");
            }
        }
    }
}