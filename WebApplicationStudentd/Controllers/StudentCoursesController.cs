using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationStudentd.Data;
using WebApplicationStudentd.Models;

namespace WebApplicationStudentd.Controllers
{
    public class StudentCoursesController : Controller
    {
        private readonly WebApplicationStudentdContext _context;

        public StudentCoursesController(WebApplicationStudentdContext context)
        {
            _context = context;
        }

        // GET: StudentCourses
        public async Task<IActionResult> Index()
        {
            var webApplicationStudentdContext = _context.StudentCourses.Include(s => s.Courses).Include(s => s.Students);
            return View(await webApplicationStudentdContext.ToListAsync());
        }

        // GET: StudentCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentCourses == null)
            {
                return NotFound();
            }

            var studentCourses = await _context.StudentCourses
                .Include(s => s.Courses)
                .Include(s => s.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentCourses == null)
            {
                return NotFound();
            }

            return View(studentCourses);
        }

        // GET: StudentCourses/Create
        public IActionResult Create()
        {
            ViewData["CoursesId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
            return View();
        }

        // POST: StudentCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CoursesId")] StudentCourses studentCourses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentCourses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetStudentsByCourseId), new { id = studentCourses.CoursesId });
            }
            ViewData["CoursesId"] = new SelectList(_context.Courses, "Id", "Name", studentCourses.CoursesId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", studentCourses.StudentId);
            return View(studentCourses);
        }

        // GET: StudentCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentCourses == null)
            {
                return NotFound();
            }

            var studentCourses = await _context.StudentCourses.FindAsync(id);
            if (studentCourses == null)
            {
                return NotFound();
            }
            ViewData["CoursesId"] = new SelectList(_context.Courses, "Id", "Name", studentCourses.CoursesId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", studentCourses.StudentId);
            return View(studentCourses);
        }

        // POST: StudentCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CoursesId")] StudentCourses studentCourses)
        {
            if (id != studentCourses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentCourses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentCoursesExists(studentCourses.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GetStudentsByCourseId), new { id = studentCourses.CoursesId });
            }
            ViewData["CoursesId"] = new SelectList(_context.Courses, "Id", "Name", studentCourses.CoursesId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", studentCourses.StudentId);
            return View(studentCourses);
        }

        // GET: StudentCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentCourses == null)
            {
                return NotFound();
            }

            var studentCourses = await _context.StudentCourses
                .Include(s => s.Courses)
                .Include(s => s.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentCourses == null)
            {
                return NotFound();
            }

            return View(studentCourses);
        }

        // POST: StudentCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentCourses == null)
            {
                return Problem("Entity set 'WebApplicationStudentdContext.StudentCourses'  is null.");
            }
            var studentCourses = await _context.StudentCourses.FindAsync(id);
            if (studentCourses != null)
            {
                _context.StudentCourses.Remove(studentCourses);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetStudentsByCourseId), new {id = studentCourses.CoursesId});
        }

        public async Task<IActionResult> GetStudentsByCourseId(int id)
        {
            var webApplicationStudentdContext = _context.StudentCourses
                .Include(s => s.Students).Where(s => s.CoursesId == id);

            var Course = _context.Courses.FirstOrDefault(s => s.Id == id);
            ViewBag.CourseName = Course.Name;
            ViewBag.CourseId = id;
            return View(await webApplicationStudentdContext.ToListAsync());
        }

        public IActionResult AddStudent(int id)
        {
            var Students = _context.StudentCourses.Where(s => s.CoursesId == id).Select(s => s.StudentId);
            var Course = _context.Courses.FirstOrDefault(s => s.Id == id);
            ViewBag.CourseName = Course.Name;
            ViewBag.CourseId = id;
            ViewData["StudentId"] = new SelectList(_context.Students.Where(x => !Students.Contains(x.Id)), "Id", "Name");
            return View();
        }

        private bool StudentCoursesExists(int id)
        {
          return (_context.StudentCourses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
