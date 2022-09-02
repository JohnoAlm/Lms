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
using AutoMapper;
using Lms.Core.Dto;

namespace Lms.Api.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CoursesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            // Tar ut ALLA courses från databasen
            var courses = await _unitOfWork.CourseRepository.GetAllCourses();

            // Mappar från ALLA courses till en IEnumerable av CourseDto så att vi kan iterera på dem
            var dto = _mapper.Map<IEnumerable<CourseDto>>(courses);
            return Ok(dto);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            // Tar ut EN course från databasen
            var course = await _unitOfWork.CourseRepository.FindAsync(id);

            // Mappar från EN course till CourseDto
            var dto = _mapper.Map<CourseDto>(course);
            return Ok(dto);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseDto dto)
        {
            // Tar ut course från databasen
            var course = await _unitOfWork.CourseRepository.GetCourse(id);

            // Gör en null-check på course
            if (course == null) return NotFound();

            // Mappar från dto till course
            _mapper.Map(dto, course);

            // Sparar ändringarna i databasen
            await _unitOfWork.CompleteAsync();

            // Mappar tillbaka till CourseDto
            return Ok(_mapper.Map<CourseDto>(course));
        }


        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(CourseDto dto)
        {
            var course = _mapper.Map<Course>(dto);
            _unitOfWork.CourseRepository.Add(course);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, dto);
        }

        
        //[HttpDelete]
        //[Route("{id}")]
        //public async Task<IActionResult> DeleteCourse(int id)
        //{
            
        //}
    }
}
