using EscolaPro.Extensions;
using EscolaPro.Models;
using EscolaPro.Repositories;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EscolaPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanieController : ControllerBase
    {
        private readonly ICompanieRepository _companieRepository;

        public CompanieController(ICompanieRepository companieRepository)
        {
            _companieRepository = companieRepository;
        }

        [HttpGet]
        //[Authorize(policy: "admin")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Companies>>> GetAllAsync()
        {
            var response = await _companieRepository.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("{companieId:int}")]
        //[Authorize(policy: "admin")]
        [AllowAnonymous]
        public async Task<ActionResult<Companies>> GetByIdAsync(int companieId)
        {
            var user = await _companieRepository.GetByIdAsync(companieId);
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Companies>> CreateAsync(CreateCompanieViewModel request)
        {
            if (await _companieRepository.GetByCnpjAsync(request.Cnpj) != null)
            {
                return Conflict(new { message = "Esse CNPJ já foi cadastrado", type = "cnpj", code = 409 });
            }

            if (await _companieRepository.GetByNameAsync(request.Name) != null)
            {
                return Conflict(new { message = "Esse nome já foi cadastrado", type = "name", code = 409 });
            }

            var companie = new Companies
            {
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                CNPJ = request.Cnpj,
                ConnectionString = request.ConnectionString,
                Status = true
            };

            var response = await _companieRepository.CreateAsync(companie);
            return StatusCode(201, response);
        }

        [HttpPut]
        [Authorize(policy: "admin")]
        public async Task<IActionResult> UpdateAsync(UpdateCompanieViewModel request)
        {
            var companie = await _companieRepository.GetByIdAsync(request.Id.Value);
            var cnpjVerify = await _companieRepository.GetByCnpjAsync(request.Cnpj);
            var nameVerify = await _companieRepository.GetByNameAsync(request.Name);

            if (cnpjVerify != null && cnpjVerify.CNPJ != companie.CNPJ)
                return Conflict("Este CNPJ já está cadastrado, tente um CNPJ diferente!");

            if (nameVerify != null && nameVerify.Name != companie.Name)
                return Conflict("Este nome já está cadastrado, tente um nome diferente!");

            companie.Name = request.Name ?? companie.Name;
            companie.CNPJ = request.Cnpj ?? companie.CNPJ;
            companie.ConnectionString = request.ConnectionString ?? companie.ConnectionString;

            companie.LastUpdatedAt = DateTime.UtcNow;

            await _companieRepository.Update(companie);
            return StatusCode(204);
        }

        [HttpDelete("{companieId:int}")]
        //[Authorize(policy: "admin")]
        [AllowAnonymous]
        public async Task<IActionResult> DisableAsync(int companieId)
        {
            await _companieRepository.Disable(companieId);
            return StatusCode(204);
        }

        [HttpGet("search/{param}")]
        [Authorize(policy: "admin")]
        public async Task<ActionResult> Search(string param)
        {
            var companie = await _companieRepository.Search(param);
            return Ok(companie);
        }
    }
}
