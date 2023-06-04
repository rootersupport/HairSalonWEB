using HairSalonWEB.Interfaces;
using HairSalonWEB.Models;

namespace HairSalonWEB.Repository
{
    public class MasterRepository: IMaster
    {
        private DataContext _context;

        public MasterRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<master> GetAllMasters()
        {
            return _context.Master;
        }
        public master GetMasterByLogin(string login)
        {
            return _context.Master.SingleOrDefault(master => master.master_login == login);
        }
        public void AddMaster(master master)
        {
            _context.Master.Add(master);
            _context.SaveChanges();
        }

        public master GetMaster(int id)
        {
            return _context.Master.Find(id);
        }

        public void UpdateMaster(master master)
        {
            _context.Master.Update(master);
            _context.SaveChanges();
        }

        public void DeleteMaster(master master)
        {
            _context.Master.Remove(master);
            _context.SaveChanges();
        }
    }
}
