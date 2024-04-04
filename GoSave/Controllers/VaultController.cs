using GoSave.Context;
using GoSave.Models;
using GoSave.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
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
        GoSaveDbContext _db;
        public VaultController(ILogger<VaultController> logger, GoSaveDbContext db)
        {
            _vaultRepo = new VaultRepo();
            _logger = logger;
            this._db = db;
        }

        //gets all vaults
        [Authorize]
        [HttpGet("GetVaults")]
        public async Task<IActionResult> GetVaults()
        {
            Guid userId = GetUserIdFromClaim();
            if(userId == Guid.Empty)
            {
                return Unauthorized("not logged in");
            }

            var vaults = await this._db.Vaults.Where(i => i.OwnerId == userId).ToListAsync();
            return Ok(vaults);
        }

        /// <summary>
        /// Gets info about a specific vault
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Object</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> One(Guid vaultId)
        {
            try
            {
                Guid userId = GetUserIdFromClaim();
                if (userId == Guid.Empty)
                {
                    return StatusCode(500, "Error getting details, try logging back into your account" +
                        "if the issue persists contact support >:)");
                }

                Vault vault = new Vault("", Guid.Empty, 0);

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

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetImage(Guid vaultId)
        {
            try
            {
                Guid userId = GetUserIdFromClaim();
                var vault = await _db.Vaults.Where(i => i.Id == vaultId).FirstOrDefaultAsync();
                if (vault.OwnerId != userId)
                {
                    return Unauthorized();
                }
                var vaultImage = await _db.VaultImages.Where(i=>i.VaultId == vault.Id).FirstOrDefaultAsync();
                return Ok(vaultImage.Base64Image);
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
        /// Creates a vault
        /// </summary>
        /// <param name="vault"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(string name, decimal amount, string base64image)
        {
            try
            {
                Vault newVault = new Vault(name, GetUserIdFromClaim(), amount);
                VaultImage vaultImage = new VaultImage(newVault.Id, base64image);
                await this._db.AddAsync(newVault);
                await this._db.AddAsync(vaultImage);
                await this._db.SaveChangesAsync();
                return Ok(new { message = "Vault created", createdVault = newVault });
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError("Failed to create vault reason: Empty field for name or empty goal");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>UserId contained from claim</returns>
        private Guid GetUserIdFromClaim()
        {
            try
            {
                Claim claim = ((ClaimsIdentity)User.Identity).FindFirst("userId");
                Guid userId = Guid.Parse(claim.Value);
                return userId;
            }
            catch (ArgumentNullException) { return Guid.Empty; }
            catch (FormatException) { return Guid.Empty; }
            catch (OverflowException) { return Guid.Empty; }
        }
    }
}
