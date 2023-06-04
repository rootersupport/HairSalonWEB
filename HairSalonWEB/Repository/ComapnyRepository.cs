using HairSalonWEB.Interfaces;
using HairSalonWEB.Models;

namespace HairSalonWEB.Repository
{
    public class CompanyRepository : ICompany
    {
        private DataContext _context;

        public CompanyRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<company> GetAllCompanies()
        {
            return _context.Company;
        }

        public void AddCompany(company company)
        {
            _context.Company.Add(company);
            _context.SaveChanges();
        }

        public company GetCompany(int id)
        {
            return _context.Company.Find(id);
        }

        public void UpdateCompany(company company)
        {
            _context.Company.Update(company);
            _context.SaveChanges();
        }

        public void DeleteCompany(company company)
        {
            _context.Company.Remove(company);
            _context.SaveChanges();
        }
    }
}
