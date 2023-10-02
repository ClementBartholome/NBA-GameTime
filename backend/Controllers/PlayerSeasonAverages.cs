using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerSeasonAveragesController : ControllerBase
    {
        private readonly NbaDbContext _dbContext;

        public PlayerSeasonAveragesController(NbaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("byId")]
        public async Task<ActionResult<PlayerSeasonAverages>> GetPlayerSeasonAverages([FromQuery] int playerId, [FromQuery] int season)
        {
            try
            {
                // Get the player's season averages for the specified playerId from the database
                var playerSeasonAverages = await _dbContext.PlayerSeasonAverages
                    .FirstOrDefaultAsync(averages => averages.PlayerId == playerId && averages.Season == season);

                if (playerSeasonAverages == null)
                {
                    return NotFound("Aucune statistique trouvée pour ce joueur");
                }

                return Ok(playerSeasonAverages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne du serveur : {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddPlayerSeasonAverages([FromBody] PlayerSeasonAverages playerSeasonAverages)
        {
            try
            {
                // Check if player's season averages for this playerId and season already exist in the database
                bool averagesExist = await _dbContext.PlayerSeasonAverages
                    .AnyAsync(averages => averages.PlayerId == playerSeasonAverages.PlayerId && averages.Season == playerSeasonAverages.Season);

                if (averagesExist)
                {
                    return Ok("Statistiques de la saison déjà ajoutées");
                }

                // Add player's season averages to the database
                _dbContext.PlayerSeasonAverages.Add(playerSeasonAverages);
                await _dbContext.SaveChangesAsync();
                return Ok("Statistiques de la saison ajoutées avec succès");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne du serveur : {ex.Message}");
            }
        }
    }
}
