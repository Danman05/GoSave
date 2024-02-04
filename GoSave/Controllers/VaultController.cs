using GoSave.Models;
using GoSave.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoSave.Controllers
{
    /* Vault Controller
     * 
     * Authors: Daniel Spurrell
     * 
     * This class is a controller for the vault context
     * 
     * 
     * Methods:
     * One: Returns a vault from a given Id
     * To retrieve a vault, the user must match its userId
     * With the vaults given ownerId. // TODO Add admin role that can fetch any vault
     * 
     * All:
     * Returns collecton of vaults that matches
     * vault.ownerId == user.Id. // TODO Admin role can fetch vault collection from any userId
     * 
     * Create:
     * Lets the user create a new vault
     */

    [ApiController]
    [Authorize] // Requires authorization on this class, this will add authorization on all actions
    [Route("[controller]")]
    public class VaultController : Controller
    {
        ILogger<VaultController> _logger;
        VaultRepo _vaultRepo;
        public VaultController(ILogger<VaultController> logger)
        {
            _vaultRepo = new VaultRepo();
            _logger = logger;
        }


        /// <summary>
        /// Get one vault
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Object</returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult One(int id)
        {
            try
            {
                int userId = GetUserIdFromClaim();
                if (userId < 0)
                {
                    return StatusCode(500, "Error getting details, try logging back into your account" +
                        "if the issue persists contact support >:)");
                }

                Vault vault = _vaultRepo.GetVault(id);

                if (vault == null)
                    return NotFound("Vault not found");

                return Ok(new { message = vault, userIdClaim = userId });
            }
            catch (ArgumentNullException)
            {
                return NotFound("Vault not found");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error occoured while getting vault");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All owned vaults</returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult All()
        {
            int userId = GetUserIdFromClaim();
            if (userId < 0)
            {
                return StatusCode(500, "Error getting details, try logging back into your account" +
                    "if the issue persists contact support >:)");
            }

            return Ok(new
            {
                message = $"Hello {User?.Identity?.Name} Here is a list of your owned vaults",
                ownedVaults = _vaultRepo.GetVaults(userId)
            });
        }
        /// <summary>
        /// Creates a vault
        /// </summary>
        /// <param name="vault"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult Create([FromBody] Vault vault)
        {
            try
            {
                vault.OwnerId = GetUserIdFromClaim();
                if (vault.OwnerId < 0)
                    return StatusCode(500, "Error getting details, try logging back into your account" +
                    "if the issue persists contact support >:)");

                _vaultRepo.AddVault(vault);
                _logger.LogInformation("Vault has been created with Id: {vaultId}", vault.Id);

                return Ok(new { message = "Vault created", createdVault = vault });
            }
            catch (ArgumentNullException)
            {
                _logger.LogError("Failed to create vault reason: Empty field for name or empty goal");
                return BadRequest("Vault needs an valid name & goal");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>UserId contained from claim</returns>
        private int GetUserIdFromClaim()
        {
            try
            {
                Claim claim = ((ClaimsIdentity)User.Identity).FindFirst("userId");
                int userId = Int32.Parse(claim.Value);
                return userId;
            }
            catch (ArgumentNullException) { return -1; }
            catch (FormatException) { return -1; }
            catch (OverflowException) { return -1; }
        }
    }
}
