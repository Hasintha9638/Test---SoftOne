
global using web_api.Models;
using Microsoft.AspNetCore.Mvc;
using web_api.Dtos.Students;
using web_api.Services.StudentsService;

namespace web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {   
        private readonly IStudentsService _studentsService;
        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }
        
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetStudentsDto>>>> Get()
        {
           return Ok(await _studentsService.GetAllStudent());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetStudentsDto>>> Get(int id)
        {
            var data = await _studentsService.GetStudentById(id);
            if (data.Data == null)
            {
                return NotFound("Student not found");
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetStudentsDto>>>> Post(AddStudentsDto student)
        {   
            
            var data = await _studentsService.isCheckStudent(student.Email, student.NIC);
            if (data.Data != null)
            {
                return Ok("Email or NIC already exists");
            }
            return Ok(await _studentsService.CreateStudent(student));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetStudentsDto>>> Put(int id, UpdateStudentsDto student)
        {
            Console.WriteLine("Put method called" + id + student);
            var data = await _studentsService.UpdateStudent(id, student);
            if (data.Data == null)
            {
                return NotFound("Student not found");
            }
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _studentsService.DeleteStudent(id);
            if (data.Data == false)
            {
                return NotFound("Student not found");
            }
            return Ok(data);
        }

    }
}