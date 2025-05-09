﻿using EscolaPro.Enums;
using EscolaPro.Models;
using EscolaPro.Repositories.Interfaces.Educacional;
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
        [Authorize(policy: Policies.ADMIN_MASTER)]
        public async Task<IActionResult> DatabaseUpdate()
        {
            var companies = await _companieRepository.GetAllAsync();

            foreach (Company companie in companies)
            {
                await _databaseService.UpdateDatabase(companie.Name);
            }

            return Ok(new { message = "Update dos bancos internos Finalizado!" });
        }
    }
}
