using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw10.Models;
using cw10.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace cw10.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : Controller
    {

        private s16520Context db;

        public StudentController(s16520Context dbService)
        {
            db = new s16520Context();
        }


        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(db.Student.ToList());
        }

        [HttpPost("modify")]
        public IActionResult ModifyStudent(ModifyStudentRequest request)
        {
            if (request == null || request.IndexNumber == null)
                return BadRequest("Brak danych wejściowych");

            var student = db.Student.Where(s => s.IndexNumber.Equals(request.IndexNumber)).ToList().First();
            if (student == null)
                return NotFound("Student with index: @index not found");

            if(request.FirstName != null)
                student.FirstName = request.FirstName;
            if(request.LastName != null)
                student.LastName = request.LastName;
            if(request.BirthDate != null)
                student.BirthDate = request.BirthDate;
            if (request.IdEnrollment != 0)
                student.IdEnrollment = request.IdEnrollment;
                        
            db.Student.Update(student);

            db.SaveChanges();
            return Ok();
        }

        [HttpDelete("del/{index}")]
        public IActionResult RemoveStudent(String index)
        {
            if (index == null)
                return BadRequest("Brak danych wejściowych");

            var student = db.Student.Where(s => s.IndexNumber.Equals(index)).ToList().First();
            if (student == null)
                return NotFound("Student with index: @index not found");

            db.Student
                .Remove(student);

            db.SaveChanges();
            return Ok("Usunięto pomyślnie: " + student );
        }
    }
}