using securityApp.Data;
using securityApp.Interfaces;
using securityApp.Models;

namespace securityApp.Repositories
{
    public class ScanRepository : IScanRepository
    {
        private readonly DataContext _context;
        public ScanRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public bool AddScan(Scan scan)
        {
            _context.Add(scan);
            return save();
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
