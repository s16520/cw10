using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

using cw10.DTOs.Requests;
using cw10.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using cw10.Models;

namespace cw10.Controllers
{
    [Authorize(Roles = "employee")]
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        private s16520Context db;

        public EnrollmentsController(s16520Context dbService)
        {
            db = new s16520Context();
        }

        [HttpGet]
        public IActionResult GetEnrollments()
        {
            return Ok(db.Enrollment.ToList());
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
           
            var st = new Student();
            st.FirstName = request.FirstName;
            st.BirthDate = request.Birthdate;
            st.LastName = request.LastName;
            st.IndexNumber = request.IndexNumber;

            var s = db.Student.Where(s => s.IndexNumber.Equals(request.IndexNumber)).ToList().First();
            if (s != null)
                return BadRequest("Student with that index already exists");

            var studies = db.Studies.Where(s => s.IdStudy.Equals(request.Studies)).ToList().First();
            if (studies == null)
                return NotFound("Studies does not exist");

            var enroll = db.Enrollment.Where(e => e.IdStudy.Equals(request.Studies))
                .Where(e => e.Semester.Equals(1)).ToList().First();

            if(enroll == null)
            {
                DateTime now = DateTime.Now;
                enroll = new Enrollment();
                enroll.IdStudy = request.Studies;
                enroll.Semester = 1;
                enroll.StartDate = now;

                db.Enrollment.Add(enroll);
                db.SaveChanges();
            }

            enroll = db.Enrollment.Where(e => e.IdStudy.Equals(request.Studies))
                .Where(e => e.Semester.Equals(1)).ToList().First();

            if (enroll == null)
                return BadRequest("SOMETHING WENT VERY VERY WRONG");

            st.IdEnrollment = enroll.IdEnrollment;
            db.Student.Add(st);

            db.SaveChanges();

            return Ok("Student added");
        }
    }
}