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
    public class TeamsController : ControllerBase
    {
        private readonly NbaDbContext _dbContext;

        public TeamsController(NbaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{TeamName}")]
        public async Task<ActionResult<Team>> GetTeamInfo(string TeamName)
        {
            try
            {
                // Try to find the team info in the database by TeamName
                var team = await _dbContext.Teams
                    .FirstOrDefaultAsync(t => t.TeamName == TeamName);

                if (team != null)
                {
                    // If found, return the team info
                    return Ok(team);
                }

                // If not found, return a default value or handle as needed
                return NotFound("Team info not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddTeamInfo([FromBody] Team team)
        {
            try
            {
                // Check if team info already exists in the database
                var existingTeam = await _dbContext.Teams
                    .FirstOrDefaultAsync(t => t.TeamName == team.TeamName);

                if (existingTeam != null)
                {
                    // If team info already exists, return a message or handle as needed
                    return Ok("Team info already exists in the database");
                }

                // If team info doesn't exist, add it to the database
                _dbContext.Teams.Add(team);
                await _dbContext.SaveChangesAsync();

                return Ok("Team info added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
