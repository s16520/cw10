using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cw10.DTOs.Requests
{
    public class ModifyStudentRequest
    {
        [Required(ErrorMessage = "Podaj index !")]
        [MaxLength(100)]
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int IdEnrollment { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public string Salt { get; set; }
    }
}
