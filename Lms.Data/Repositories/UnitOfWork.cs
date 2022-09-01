using Lms.Core.Repositories;
using Lms.Data.Data;

namespace Lms.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LmsApiContext _context;
        public ICourseRepository CourseRepository { get; }
        public IModuleRepository ModuleRepository { get; }

        public UnitOfWork(LmsApiContext context)
        {
            _context = context;
            CourseRepository = new CourseRepository(context);
            ModuleRepository = new ModuleRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
