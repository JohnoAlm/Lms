using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{

#nullable disable

    internal class ModuleRepository : IModuleRepository
    {
        private readonly LmsApiContext _context;

        public ModuleRepository(LmsApiContext context)
        {
            _context = context;
        }

        public void Add(Module module)
        {
            _context.AddAsync(module);
        }

        public async Task<bool> AnyAsync(int? id)
        {
            return await _context.Module.AnyAsync(c => c. Id == id);
        }

        public async Task<Module> FindAsync(int? id)
        {
            return await _context.Module.FindAsync(id);
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await _context.Module.ToListAsync();
        }

        public async Task<Module> GetModule(int? id)
        {
            return await _context.Module.FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Remove(Module module)
        {
            _context.Remove(module);
        }

        public void Update(Module module)
        {
            _context.Update(module);
        }
    }
}
