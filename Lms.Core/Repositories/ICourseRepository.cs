﻿using Lms.Core.Entities;

namespace Lms.Core.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course> GetCourse(int? id); 
        Task<Course> FindAsync(int? id); 
        Task<bool> AnyAsync(int? id); 
        void Add(Course course); 
        void Update(Course course); 
        void Remove(Course course);
    }
}