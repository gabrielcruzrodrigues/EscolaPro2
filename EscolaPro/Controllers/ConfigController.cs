using EscolaPro.Models;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private readonly ICompanieRepository _companieRepository;

        public ConfigController(IDatabaseService databaseService, ICompanieRepository companieRepository)
        {
            _databaseService = databaseService;
            _companieRepository = companieRepository;
        }

        [HttpGet("Test")]
        [AllowAnonymous]
        public async Task<IActionResult> Test()
        {
            return Ok(new { message = "Ok" });
        }

        [HttpGet("Database-update")]
        [Authorize(policy: "admin")]
        public async Task<IActionResult> DatabaseUpdate()
        {
            var companies = await _companieRepository.GetAllAsync();

            foreach (Companie companie in companies)
            {
                await _databaseService.UpdateDatabase(companie.Name);
            }

            return Ok(new { message = "Update dos bancos internos Finalizado!" });
        }
    }
}
