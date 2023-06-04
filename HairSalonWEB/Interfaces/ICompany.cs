using HairSalonWEB.Models;
namespace HairSalonWEB.Interfaces
{
    public interface ICompany
    {
        IEnumerable<company> GetAllCompanies();
        void AddCompany(company company);
        company GetCompany(int id);
        void UpdateCompany(company company);
        void DeleteCompany(company company);
    }
}