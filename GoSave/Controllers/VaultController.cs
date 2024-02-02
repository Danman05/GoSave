using GoSave.Models;
using GoSave.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoSave.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class VaultController : Controller
    {
        VaultRepo _vaultRepo;
        public VaultController()
        {
            _vaultRepo = new VaultRepo();
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            Vault vault = _vaultRepo.GetVault(id);
            
            if (vault == null)
                return NotFound("Vault not found");

            return Ok(vault);
        }
    }
}
