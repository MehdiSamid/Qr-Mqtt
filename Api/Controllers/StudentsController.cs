using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class StudentsController(PointageContext context) : ControllerBase
    {
        private readonly PointageContext _context = context;

        [HttpGet("{id}")]
        public ActionResult<List<Student>> Get(int id) {
            var students = _context.Students
                .Where(s => s.Filiere.FiliereId == id)
                .ToList();

            if (students == null || students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }
    }
}
