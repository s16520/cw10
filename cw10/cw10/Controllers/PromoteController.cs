using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

using cw10.Models;
using cw10.DTOs.Requests;
using cw10.DTOs.Responses;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace cw10.Controllers
{
    [Route("api/enrollments/promotions")]
    [Authorize(Roles = "employee")]
    [ApiController]
    
    public class PromoteControlle : ControllerBase
    {

        private s16520Context db;

        public PromoteControlle(s16520Context dbService)
        {
            db = new s16520Context();
        }

        [HttpPost]
        public IActionResult PromoteStudents(PromotionRequest request)
        {

            var studies = db.Studies.Where(s => s.Name.Equals(request.Studies)).ToList().First();

            if (studies == null)
                return NotFound("Studies not found");

            var enroll = db.Enrollment.Where(e => e.Semester.Equals(request.Semester))
                .Where(e => e.IdStudy.Equals(studies.IdStudy)).ToList().First();

            if (enroll == null)
                return NotFound("Enrollment does not exist");

            enroll.Semester++;

            db.Enrollment.Update(enroll);
            db.SaveChanges();

            return Ok("Semester updated");
            
            }

        }



    }
}
