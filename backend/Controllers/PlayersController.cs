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
    public class PlayersController : ControllerBase
    {
        private readonly NbaDbContext _dbContext;

        public PlayersController(NbaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("byId")]
        public async Task<ActionResult<Player>> GetPlayerById([FromQuery] int PlayerId)
        {
            try
            {
                // Get a single player for the specified ID from the database
                var player = await _dbContext.Players
                    .FirstOrDefaultAsync(player => player.PlayerId == PlayerId);

                if (player == null)
                {
                    return NotFound("Joueur non trouvé");
                }

                return Ok(player);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne du serveur: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddPlayer([FromBody] Player player)
        {
            try
            {
                // Check if player for this id already exists in the database
                bool playerExist = await _dbContext.Players
                    .AnyAsync(p => p.PlayerId == player.PlayerId);

                if (playerExist)
                {
                    return Ok("Joueur déjà ajouté");
                }

                // Add player to the database
                _dbContext.Players.Add(player);
                await _dbContext.SaveChangesAsync();
                return Ok("Joueur ajouté avec succès");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne du serveur : {ex.Message}");
            }
        }
    }
}