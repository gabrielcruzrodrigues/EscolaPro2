using EscolaPro.Enums;
using EscolaPro.Models.Educacional;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.Repositories.Interfaces.Educacional;
using EscolaPro.ViewModels.Educacional;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EscolaPro.Controllers.Educacional
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergieController : ControllerBase
    {
        private readonly IAllergieRepository _allergieRepository;
        private readonly ICompanieRepository _companieRepository;
        private readonly IUsersGeneralRepository _userGeneralRepository;

        public AllergieController(
            IAllergieRepository allergieRepository,
            ICompanieRepository companieRepository,
            IUsersGeneralRepository userGeneralRepository
        )
        {
            _allergieRepository = allergieRepository;
            _companieRepository = companieRepository;
            _userGeneralRepository = userGeneralRepository;
        }

        [HttpGet]
        [Authorize(policy: Policies.ADMINISTRACAO)]
        public async Task<ActionResult<IEnumerable<Allergy>>> GetAllAsync()
        {
            // ============= Início validação de empresa e adquirimento do nome da empresa =============

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

            if (!await CompanieValidation(userId, userCompanieId))
            {
                return BadRequest("Você não tem acesso a essa empresa!");
            }
            var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

            // ============= Fim validação de empresa e adquirimento do nome da empresa =============

            var response = await _allergieRepository.GetAllAsync(userCompanie.Name);
            return Ok(response);
        }

        [HttpGet("{allergieId:int}")]
        [Authorize(policy: Policies.ADMINISTRACAO)]
        public async Task<ActionResult<Allergy>> GetByIdAsync(int allergieId)
        {
            // ============= Início validação de empresa e adquirimento do nome da empresa =============

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

            if (!await CompanieValidation(userId, userCompanieId))
            {
                return BadRequest("Você não tem acesso a essa empresa!");
            }
            var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

            // ============= Fim validação de empresa e adquirimento do nome da empresa =============

            var user = await _allergieRepository.GetByIdAsync(userCompanie.Name, allergieId);
            return Ok(user);
        }

        [HttpPost]
        [Authorize(policy: Policies.ADMINISTRACAO)]
        public async Task<ActionResult<Allergy>> CreateAsync(CreateAllergieViewModel request)
        {
            // ============= Início validação de empresa e adquirimento do nome da empresa =============

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

            if (!await CompanieValidation(userId, userCompanieId))
            {
                return BadRequest("Você não tem acesso a essa empresa!");
            }
            var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

            // ============= Fim validação de empresa e adquirimento do nome da empresa =============

            if (await _allergieRepository.GetByNameAsync(userCompanie.Name, request.Name) != null)
            {
                return BadRequest("Alergia já cadastrada no banco de dados!");
            }

            var allergie = new Allergy
            {
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = true
            };

            var response = await _allergieRepository.CreateAsync(userCompanie.Name, allergie);
            return StatusCode(201, response);
        }

        [HttpDelete("{allergieId:int}")]
        [Authorize(policy: Policies.ADMINISTRACAO)]
        public async Task<IActionResult> DisableAsync(int allergieId)
        {
            // ============= Início validação de empresa e adquirimento do nome da empresa =============

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

            if (!await CompanieValidation(userId, userCompanieId))
            {
                return BadRequest("Você não tem acesso a essa empresa!");
            }
            var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

            // ============= Fim validação de empresa e adquirimento do nome da empresa =============

            await _allergieRepository.Disable(userCompanie.Name, allergieId);
            return StatusCode(204);
        }

        [HttpGet("search/{param}")]
        [Authorize(policy: Policies.ADMINISTRACAO)]
        public async Task<ActionResult> Search(string param)
        {
            // ============= Início validação de empresa e adquirimento do nome da empresa =============

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

            if (!await CompanieValidation(userId, userCompanieId))
            {
                return BadRequest("Você não tem acesso a essa empresa!");
            }
            var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

            // ============= Fim validação de empresa e adquirimento do nome da empresa =============

            var companie = await _allergieRepository.Search(userCompanie.Name, param);
            return Ok(companie);
        }

        [HttpPut]
        [Authorize(policy: Policies.ADMINISTRACAO)]
        public async Task<ActionResult> Update(UpdateAllergieViewModel request)
        {
            // ============= Início validação de empresa e adquirimento do nome da empresa =============

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userCompanieId = int.Parse(User.FindFirst("CompanieId")?.Value);

            if (!await CompanieValidation(userId, userCompanieId))
            {
                return BadRequest("Você não tem acesso a essa empresa!");
            }
            var userCompanie = await _companieRepository.GetByIdAsync(userCompanieId);

            // ============= Fim validação de empresa e adquirimento do nome da empresa =============

            Allergy allergie = await _allergieRepository.GetByIdAsync(userCompanie.Name, request.Id);

            if (request.Name.IsNullOrEmpty())
            {
                allergie.Name = request.Name ?? allergie.Name;
            }

            await _allergieRepository.Update(userCompanie.Name, allergie);
            return NoContent();
        }

        private async Task<bool> CompanieValidation(long userId, int companieId)
        {
            var userGeneral = await _userGeneralRepository.GetByIdAsync(userId);

            if (userGeneral.CompanieId == companieId)
            {
                return true;
            }

            return false;
        }
    }
}
