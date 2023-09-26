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
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            try
            {
                // Get all games from database
                var games = await _dbContext.Games.ToListAsync();
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
    }
}
