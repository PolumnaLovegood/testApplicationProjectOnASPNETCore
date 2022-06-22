using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationStudentd.Models
{
    public class Students
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [Remote(action: "StudentIdUnique", controller: "Students")]
        public int StudentId { get; set; }
        [Required]
        [MaxLength(25)]
        public string Group { get; set; }
    }
}
