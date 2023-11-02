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
    public class GamesController : ControllerBase
    {
        private readonly NbaDbContext _dbContext;

        public GamesController(NbaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("byDate")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByDate([FromQuery] string GameDate)
        {
            try
            {
                // Get games for the specified date from the database
                var games = await _dbContext.Games
                    .Where(game => game.GameDate == GameDate)
                    .ToListAsync();

                return Ok(games);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne du serveur : {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddGames([FromBody] List<Game> games)
        {
            try
            {
                // Check if games for this date already exist in database
                bool gamesExist = await _dbContext.Games
                    .AnyAsync(game => game.GameDate == games.First().GameDate);

                if (gamesExist)
                {
                    return Ok("Matchs déjà ajoutés pour cette date.");
                }

                // Add games to database
                await _dbContext.Games.AddRangeAsync(games);
                await _dbContext.SaveChangesAsync();
                return Ok("Matchs ajoutés avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne du serveur : {ex.Message}");
            }
        }

        [HttpPut]
        [Route("updateGameScore/{gameId}")]
        public async Task<ActionResult> UpdateGameScore(int gameId, [FromBody] GameScoreUpdate gameScoreUpdate)
        {
            try
            {
                var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.GameId == gameId);

                if (game != null)
                {
                    game.HomeTeamScore = gameScoreUpdate.HomeTeamScore;
                    game.VisitorTeamScore = gameScoreUpdate.VisitorTeamScore;
                    
                    await _dbContext.SaveChangesAsync();

                    return Ok("Score du match mis à jour avec succès.");
                }
                else
                {
                    return NotFound("Match non trouvé.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne du serveur : {ex.Message}");
            }
        }
    }
}
