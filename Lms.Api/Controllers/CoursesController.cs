using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Data.Data;
using Lms.Core.Entities;
using Lms.Core.Repositories;

namespace Lms.Api.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly LmsApiContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public CoursesController(LmsApiContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            var courses = await _unitOfWork.CourseRepository.GetAllCourses();
            return Ok(courses);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _unitOfWork.CourseRepository.FindAsync(id);
            return Ok(course);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            course = await _unitOfWork.CourseRepository.GetCourse(id);

            if (id != course.Id)
            {
                return BadRequest();
            }

            _unitOfWork.CourseRepository.Update(course);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CourseExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            if (await _unitOfWork.CourseRepository.GetCourse(course.Id) != null)
            {
                ModelState.AddModelError("Id", "Id is already occupied!");
                return BadRequest(ModelState);
            }

            _unitOfWork.CourseRepository.Add(course);
            await _unitOfWork.CompleteAsync();
            
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _unitOfWork.CourseRepository.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            _unitOfWork.CourseRepository.Remove(course);
            await _unitOfWork.CompleteAsync();  

            return NoContent();
        }

        //private async bool CourseExists(int id)
        //{
        //    return await _unitOfWork.CourseRepository?.AnyAsync(id);
        //}
    }
}
