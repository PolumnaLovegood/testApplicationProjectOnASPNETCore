using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationStudentd.Models
{
    public class Courses
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        [Remote(action: "CourseNameUnique", controller: "Courses")]
        public string Name { get; set; }
    }
}
