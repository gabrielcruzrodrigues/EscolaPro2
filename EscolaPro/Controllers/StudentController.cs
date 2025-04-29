using EscolaPro.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ICompanieRepository _companieRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentController(ICompanieRepository companieRepository, IStudentRepository studentRepository)
        {
            _companieRepository = companieRepository;
            _studentRepository = studentRepository;
        }
    }
}
