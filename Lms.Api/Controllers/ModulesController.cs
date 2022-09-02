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
    [Route("api/modules")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ModulesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModule()
        {
            // Tar ut ALLA moduler från databasen
            var modules = await _unitOfWork.ModuleRepository.GetAllModules();

            // Mappar från ALLA moduler till en IEnumerable av ModuleDto så att vi kan iterera på dem
            var dto = _mapper.Map<IEnumerable<ModuleDto>>(modules);

            return Ok(dto);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Module>> GetModule(int id)
        {
            // Tar ut EN module från databasen
            var module = await _unitOfWork.ModuleRepository.GetModule(id);

            // Mappar från EN module till ModuleDto
            var dto = _mapper.Map<ModuleDto>(module);

            return Ok(dto);
        }


        [HttpPut]
        [Route("id")]
        public async Task<IActionResult> PutModule(int id, ModuleDto dto)
        {
            // Tar ut modulen från databasen
            var module = await _unitOfWork.ModuleRepository.GetModule(id);

            // Gör en null-check på modulen
            if (module == null) return NotFound();

            // Mappar från dto till module
            _mapper.Map(dto, module);

            // Sparar ändringarna i databasen
            await _unitOfWork.CompleteAsync();

            // Mappar tillbaka till ModuleDto
            return Ok(_mapper.Map<ModuleDto>(module));

        }


        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(ModuleDto dto)
        {
            var module = _mapper.Map<Module>(dto);
            _unitOfWork.ModuleRepository.Add(module);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetModule), new { id = module.Id }, dto);
        }

        //// DELETE: api/Modules/5
        //[HttpDelete]
        //[Route("{id}")]
        //public async Task<IActionResult> DeleteModule(int id)
        //{
            
        //}

       
    }
}
