using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Lms.Data.Repositories
{

#nullable disable

    public class CourseRepository : ICourseRepository
    {
        private readonly LmsApiContext _context;

        public CourseRepository(LmsApiContext context)
        {
            _context = context;
        }

        public void Add(Course course)
        {
            _context.AddAsync(course);
        }

        public async Task<bool> AnyAsync(int? id)
        {
            return await _context.Course.AnyAsync(c => c.Id == id);
        }

        public async Task<Course> FindAsync(int? id)
        {
            return await _context.Course.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _context.Course.ToListAsync();
        }

        public async Task<Course> GetCourse(int? id)
        {
            return await _context.Course.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Remove(Course course)
        {
            _context.Remove(course);
        }

        public void Update(Course course)
        {
            _context.Update(course);
            
        }
    }
}
