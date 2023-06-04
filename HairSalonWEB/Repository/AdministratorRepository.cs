using HairSalonWEB.Interfaces;
using HairSalonWEB.Models;

namespace HairSalonWEB.Repository
{
    public class AdministratorRepository : IAdministrator
    {
        private DataContext _context;

        public AdministratorRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<administrator> GetAllAdmins()
        {
            return _context.Administrator;
        }

        public void AddAdmin(administrator administrator)
        {
            _context.Administrator.Add(administrator);
            _context.SaveChanges();
        }

        public administrator GetAdmin(int id)
        {
            return _context.Administrator.Find(id);
        }
        public administrator GetAdminByLogin(string login)
        {
            return _context.Administrator.SingleOrDefault(admin => admin.admin_login == login);
        }
        public administrator GetAdminRecords(string login)
        {
            return _context.Administrator.Find(login);
        }
        public void UpdateAdmin(administrator administrator)
        {
            _context.Administrator.Update(administrator);
            _context.SaveChanges();
        }

        public void DeleteAdmin(administrator administrator)
        {
            _context.Administrator.Remove(administrator);
            _context.SaveChanges();
        }
    }
}
