using securityApp.Models;

namespace securityApp.Interfaces
{
    public interface IScanRepository
    {
        public bool AddScan(Scan scan);
        public bool save();
   
    }
}
