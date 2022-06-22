using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WebApplicationStudentd.Models
{
    public class StudentCourses
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [ValidateNever]
        public Students Students { get; set; }
        [Required]
        public int CoursesId { get; set; }
        [ForeignKey("CoursesId")]
        [ValidateNever]
        public Courses Courses { get; set; }
    }
}
